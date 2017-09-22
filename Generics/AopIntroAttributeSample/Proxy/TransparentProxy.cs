using AopIntroAttributeSample.Interception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroAttributeSample.Proxy
{
    public class TransparentProxy<T, K> : RealProxy where T : K, new()
    {
        private Predicate<MethodInfo> filter;

        public TransparentProxy()
            : base(typeof(K))
        {

        }
        public static K CreateNew(Predicate<MethodInfo> filter = null)
        {
            var instance = new TransparentProxy<T, K>();
            instance.filter = filter;
            return (K)instance.GetTransparentProxy();
        }
        public Predicate<MethodInfo> Filter
        {
            get { return filter; }
            set
            {
                if (value == null)
                    filter = m => true;
                else
                    filter = value;
            }
        }
        // İlgili metot çağırıldığında çalışacak olan metotdur.
        public override IMessage Invoke(IMessage msg)
        {
            var methodCallMessage = msg as IMethodCallMessage;
            ReturnMessage returnMessage = null;
            MethodInfo mInfo = null;
            object[] aspects = null;
            InterceptArgs e = CreateEventArgs(methodCallMessage);
            try
            {
                // tipimiz üzerinden metot infoya erişerek ilgili attribute olarak eklenmiş
                // aspect'lerimizi buluyoruz.
                var realType = typeof(T);
                mInfo = realType.GetMethod(methodCallMessage.MethodName);
                aspects = mInfo.GetCustomAttributes(typeof(IInterception), true);

                // Before aspectlerimizi çalıştırıyoruz önce ve geriye değer dönen varsa respons'a eşitliyoruz.
                object response = OnPreIntercept(aspects, new PreInterceptArgs(e));

                object result = null;

                // Response boş değilse, buradaki veri cache üzerinden de geliyor olabilir ve tekrardan invoke etmeye
                // gerek yok, direkt olarak geriye response dönebiliriz bu durumda.
                if (response != null)
                {
                    returnMessage = new ReturnMessage(response, null, 0, methodCallMessage.LogicalCallContext, methodCallMessage);
                }
                else
                {
                    // Response boş ise ilgili metot'u artık invoke ederek çalıştırıyor ve sonucunu alıyoruz.
                    result = methodCallMessage.MethodBase.Invoke(new T(), methodCallMessage.InArgs);
                    returnMessage = new ReturnMessage(result, null, 0, methodCallMessage.LogicalCallContext, methodCallMessage);
                }

                OnPostIntercept(aspects, new PostInterceptArgs(e, result));

                // After aspectlerimizi'de çalıştırdıktan sonra artık geriye çıktıyı dönebiliriz.
                return returnMessage;
            }
            catch (Exception ex)
            {
                var exArg = new ExceptionInterceptArgs(e, ex);
                OnErrorIntercept(aspects, exArg);
                return new ReturnMessage(exArg.Ex, methodCallMessage);
            }
        }
        private object OnPreIntercept(object[] aspects, PreInterceptArgs e)
        {
            object response = null;
            foreach (IInterception loopAttribute in aspects)
            {
                if (loopAttribute is IPreVoidInterception)
                {
                    ((IPreVoidInterception)loopAttribute).OnPre(e);
                }
                else if (loopAttribute is IPreInterception)
                {
                    response = ((IPreInterception)loopAttribute).OnPre(e);
                }
            }
            return response;
        }
        private void OnPostIntercept(object[] aspects, PostInterceptArgs e)
        {
            foreach (IInterception loopAttribute in aspects)
            {
                if (loopAttribute is IPostVoidInterception)
                {
                    ((IPostVoidInterception)loopAttribute).OnPost(e);
                }
            }
        }
        private void OnErrorIntercept(object[] aspects, ExceptionInterceptArgs e)
        {
            if (aspects == null)
                return;

            foreach (IInterception loopAttribute in aspects)
            {
                if (loopAttribute is IExceptionInterception)
                {
                    ((IExceptionInterception)loopAttribute).OnException(e);
                }
            }
        }
        private InterceptArgs CreateEventArgs(IMethodCallMessage methodCallMessage)
        {
            InterceptArgs e = new InterceptArgs(methodCallMessage.MethodName, methodCallMessage.InArgs);
            return e;
        }
        private bool IsRaisable(MethodInfo methodInfo)
        {
            return (filter == null || filter(methodInfo));
        }
    }
}
