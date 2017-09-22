using AopIntroAttributeSample.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroAttributeSample.Service
{
    public class BankAccountServiceFactory
    {
        public static IBankAccountService CreateTransparentProxy()
        {           
            IBankAccountService bankAccountService = TransparentProxy<DummyBankAccountService, IBankAccountService>.CreateNew();
            return bankAccountService;
        }

        public static IBankAccountService CreateFilteredTransparentProxy()
        {
            var dynamicProxy = new TransparentProxy<DummyBankAccountService, IBankAccountService>(); 
            dynamicProxy.Filter = (m => !m.Name.StartsWith("Get"));
            dynamicProxy.OnExecuting += (s, e) => Log(
             "'{0}' çalıştırılmadan önce", e.MethodName);
            dynamicProxy.OnExecuted += (s, e) => Log(
              "'{0}' çalıştırıldıktan sonra", e.MethodName);
            dynamicProxy.OnError += (s, e) => Log(
              "'{0}' çalıştırılırken hata alındı", e.MethodName); 
            IBankAccountService bankAccountService = (IBankAccountService)dynamicProxy.GetTransparentProxy();
            return bankAccountService;
        }

        private static void Log(string msg, object arg = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg, arg);
            Console.ResetColor();
        }
    }
}
