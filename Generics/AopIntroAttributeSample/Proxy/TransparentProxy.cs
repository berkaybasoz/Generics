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
        public event EventHandler<IMethodCallMessage> OnExecuting;
        public event EventHandler<IMethodCallMessage> OnExecuted;
        public event EventHandler<IMethodCallMessage> OnError;

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
            InterceptArgs e = null;
            try
            {
                RaiseOnExecuting(methodCallMessage);

                // tipimiz üzerinden metot infoya erişerek ilgili attribute olarak eklenmiş
                // aspect'lerimizi buluyoruz.
                var realType = typeof(T);
                mInfo = realType.GetMethod(methodCallMessage.MethodName);
                aspects = mInfo.GetCustomAttributes(typeof(IInterception), true);

                e = CreateEventArgs(methodCallMessage);

                // Before aspectlerimizi çalıştırıyoruz önce ve geriye değer dönen varsa respons'a eşitliyoruz.
                object response = InvokePreIntercept(aspects, new PreInterceptArgs(e));

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

                InvokePostIntercept(aspects, new PostInterceptArgs(e, result));
                RaiseOnExecuted(methodCallMessage);
                // After aspectlerimizi'de çalıştırdıktan sonra artık geriye çıktıyı dönebiliriz.
                return returnMessage;
            }
            catch (Exception ex)
            {
                RaiseOnError(methodCallMessage);

                var exArg = new ExceptionInterceptArgs(e, ex);
                InvokeErrorIntercept(aspects, exArg);
                if (!exArg.IsHandled)
                {
                    return new ReturnMessage(ex, methodCallMessage);
                }
                else
                {
                    return new ReturnMessage(null, null, 0, methodCallMessage.LogicalCallContext, methodCallMessage);
                }
            }
        }
        private object InvokePreIntercept(object[] aspects, PreInterceptArgs e)
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
        private void InvokePostIntercept(object[] aspects, PostInterceptArgs e)
        {
            foreach (IInterception loopAttribute in aspects)
            {
                if (loopAttribute is IPostVoidInterception)
                {
                    ((IPostVoidInterception)loopAttribute).OnPost(e);
                }
            }
        }
        private void InvokeErrorIntercept(object[] aspects, ExceptionInterceptArgs e)
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
        private void RaiseOnExecuting(IMethodCallMessage methodCall)
        {
            if (OnExecuting != null)
            {
                var methodInfo = methodCall.MethodBase as MethodInfo;
                if (IsRaisable(methodInfo))
                    OnExecuting(this, methodCall);
            }
        }
        private void RaiseOnExecuted(IMethodCallMessage methodCall)
        {
            if (OnExecuted != null)
            {
                var methodInfo = methodCall.MethodBase as MethodInfo;
                if (IsRaisable(methodInfo))
                    OnExecuted(this, methodCall);
            }
        }
        private void RaiseOnError(IMethodCallMessage methodCall)
        {
            if (OnError != null)
            {
                var methodInfo = methodCall.MethodBase as MethodInfo;
                if (IsRaisable(methodInfo))
                    OnError(this, methodCall);
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
