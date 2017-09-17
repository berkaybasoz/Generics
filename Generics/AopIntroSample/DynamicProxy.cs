using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroSample
{
    public class DynamicProxy<T> : RealProxy
    {
        private readonly T _decorated;
        public DynamicProxy(T decorated)
            : base(typeof(T))
        {
            _decorated = decorated;
        }

        private void Log(string msg, object arg = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg, arg);
            Console.ResetColor();
        }

        public override IMessage Invoke(IMessage msg)
        {
            //return InvokeNormal(msg);
            return InvokeWithGetFilter(msg);
        }

        private IMessage InvokeNormal(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as MethodInfo;
            Log("In Dynamic Proxy - Çalışmadan önce '{0}'",
                 methodCall.MethodName);
            try
            {
                var result = methodInfo.Invoke(_decorated, methodCall.InArgs);
                Log("In Dynamic Proxy - Çalışmadan sonra '{0}' ",
                        methodCall.MethodName);
                return new ReturnMessage(result, null, 0,
                  methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception e)
            {
                Log(string.Format(
                     "In Dynamic Proxy- Çalışırken hata alındı {0} '{1}'", e),
                     methodCall.MethodName);
                return new ReturnMessage(e, methodCall);
            }
        }

        /// <summary>
        /// Tüm metodları loglamak istemezsek (GetAll and GetById). Bunu başarmanın yollarından biri method adına göre filtreleme yapmak
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private IMessage InvokeWithGetFilter(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as MethodInfo;
            if (IsValidMethod(methodInfo))
                Log("In Dynamic Proxy - Çalışmadan önce '{0}'",
                  methodCall.MethodName);
            try
            {
                var result = methodInfo.Invoke(_decorated, methodCall.InArgs);
                if (IsValidMethod(methodInfo))
                    Log("In Dynamic Proxy - Çalışmadan sonra '{0}' ",
                      methodCall.MethodName);
                return new ReturnMessage(result, null, 0,
                 methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception e)
            {
                if (IsValidMethod(methodInfo))
                    Log(string.Format(
                      "In Dynamic Proxy- Çalışırken hata alındı {0} '{1}'", e),
                      methodCall.MethodName);
                return new ReturnMessage(e, methodCall);
            }
        }

        private static bool IsValidMethod(MethodInfo methodInfo)
        {
            return !methodInfo.Name.StartsWith("Get");
        }
    }
}
