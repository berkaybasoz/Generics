using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroSample.Proxy
{
    public class FilteredDynamicProxy<T> : RealProxy
    {
        private readonly T _decorated;
        private Predicate<MethodInfo> _filter;
        public FilteredDynamicProxy(T decorated)
            : base(typeof(T))
        {
            _decorated = decorated;
            _filter = m => true;
        }
        public Predicate<MethodInfo> Filter
        {
            get { return _filter; }
            set
            {
                if (value == null)
                    _filter = m => true;
                else
                    _filter = value;
            }
        }
        private void Log(string msg, object arg = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg, arg);
            Console.ResetColor();
        }
        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as MethodInfo;
            if (_filter(methodInfo))
                Log("In FilteredDynamic Proxy - çalıştırılıyor '{0}'",
                  methodCall.MethodName);
            try
            {
                var result = methodInfo.Invoke(_decorated, methodCall.InArgs);
                if (_filter(methodInfo))
                    Log("In FilteredDynamic Proxy - çalıştırıldı '{0}' ",
                      methodCall.MethodName);
                return new ReturnMessage(result, null, 0,
                  methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception e)
            {
                if (_filter(methodInfo))
                    Log(string.Format(
                      "In FilteredDynamic Proxy- çalışıtırılırken {0} hata alındı '{1}'",
                      methodCall.MethodName, e));
                return new ReturnMessage(e, methodCall);
            }
        }
    }
}
