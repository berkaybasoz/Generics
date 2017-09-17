using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AopIntroSample.Proxy
{
    class AuthenticationProxy<T> : RealProxy
    {
        private readonly T _decorated;
        public AuthenticationProxy(T decorated)
          : base(typeof(T))
        {
            _decorated = decorated;
        }
        private void Log(string msg, object arg = null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg, arg);
            Console.ResetColor();
        }
        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as MethodInfo;
            if (Thread.CurrentPrincipal.IsInRole("ADMIN"))
            {
                try
                {
                    Log("Kullanıcı yetkilendirildi - '{0}' metodunu çalıştırabilirsiniz",
                      methodCall.MethodName);
                    var result = methodInfo.Invoke(_decorated, methodCall.InArgs);
                    return new ReturnMessage(result, null, 0,
                      methodCall.LogicalCallContext, methodCall);
                }
                catch (Exception e)
                {
                    Log(string.Format(
                      "Kullanıcı yetkilendirme hatası - {0} metodu çalışırken hata oluştu '{1}'",
                      methodCall.MethodName, e));
                    return new ReturnMessage(e, methodCall);
                }
            }
            Log("Kullanıcı yetkilendirmesi başarısız - '{0}' metodu çalıştırılamaz",
              methodCall.MethodName);
            return new ReturnMessage(null, null, 0,
              methodCall.LogicalCallContext, methodCall);
        }
    }
}
