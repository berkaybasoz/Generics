using AopIntroAttributeSample.Container;
using AopIntroAttributeSample.Model;
using AopIntroAttributeSample.Service;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroAttributeSample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ContainerContext.Load();

                IBankAccountService productService = BankAccountServiceFactory.CreateTransparentProxy();

                //servis üzerinden normal şekilde veriler gelecek
                BankAccountCollection bankAccounts = productService.GetBankAccounts(1);
                foreach (var bankAccount in bankAccounts)
                {
                    Console.WriteLine(bankAccount.ToString());
                }

                //cache üzerinden gelecek
                bankAccounts = productService.GetBankAccounts(1);
                foreach (var bankAccount in bankAccounts)
                {
                    Console.WriteLine(bankAccount.ToString());
                }
                 
                //hata verdirmeye calisiyoruz
                productService.Deposit(1000, 200000);

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Program main ex" + ex.ToString());
            }

            Console.ReadLine();
        }

    }
}
