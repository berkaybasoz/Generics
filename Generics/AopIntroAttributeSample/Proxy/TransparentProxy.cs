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

        //Attribute olarak eklenen metot çağırıldığında çalışacak olan metotdur.
        public override IMessage Invoke(IMessage msg)
        {
            var methodCallMessage = msg as IMethodCallMessage;
            ReturnMessage returnMessage = null;
            object[] interceptions = null;
            InterceptArgs e = CreateEventArgs(methodCallMessage);
            try
            {
                interceptions = GetInterceptions(methodCallMessage);

                // PreInterceptionlarımız çalıştırıyoruz, cache tarzı geriye donen attribute varsa bunlardan donen sonucu alıyoruz 
                PreInterceptArgs preArg = new PreInterceptArgs(e);
                object result = OnPreIntercept(interceptions, preArg);

                // OverrideReturnValue true ise esas metodu çalıştırmamıza gerek yok
                if (preArg.OverrideReturnValue)
                {
                    returnMessage = new ReturnMessage(result, null, 0, methodCallMessage.LogicalCallContext, methodCallMessage);
                }
                else
                {
                    // boş resultımız olduğuna göre ilgili metodu çalıştırabiliriz
                    result = methodCallMessage.MethodBase.Invoke(new T(), methodCallMessage.InArgs);
                    returnMessage = new ReturnMessage(result, null, 0, methodCallMessage.LogicalCallContext, methodCallMessage);
                }

                OnPostIntercept(interceptions, new PostInterceptArgs(e, result));

                // PostInterceptionlarımız çalıştırdıktan sonra artık geriye çıktıyı dönebiliriz.
                return returnMessage;
            }
            catch (Exception ex)
            {
                var exArg = new ExceptionInterceptArgs(e, ex);
                OnErrorIntercept(interceptions, exArg);
                return new ReturnMessage(exArg.Ex, methodCallMessage);
            }
        }

        private object[] GetInterceptions(IMethodCallMessage methodCallMessage)
        {
            // tipimiz üzerinden metot infoya erişerek ilgili attribute olarak eklenmiş
            // Interception'larımızı buluyoruz.
            var realType = typeof(T);
            MethodInfo mInfo = realType.GetMethod(methodCallMessage.MethodName);
            return mInfo.GetCustomAttributes(typeof(IInterception), true);
        }

        private InterceptArgs CreateEventArgs(IMethodCallMessage methodCallMessage)
        {
            InterceptArgs e = new InterceptArgs(methodCallMessage.MethodName, methodCallMessage.InArgs);
            return e;
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

    }
}
