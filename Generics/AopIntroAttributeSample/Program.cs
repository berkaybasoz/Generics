﻿using AopIntroAttributeSample.Container;
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

            ContainerContext.Load();

            IBankAccountService productService = BankAccountServiceFactory.CreateTransparentProxy();

            int branchCode = 1, accountNumber = 1000;

            //servis üzerinden normal şekilde veriler gelecek
            BankAccountCollection bankAccounts = productService.GetBankAccounts(branchCode);
            foreach (var bankAccount in bankAccounts)
            {
                Console.WriteLine(bankAccount.ToString());
            }

            //cache üzerinden gelecek
            bankAccounts = productService.GetBankAccounts(branchCode);
            foreach (var bankAccount in bankAccounts)
            {
                Console.WriteLine(bankAccount.ToString());
            }

            Run(() =>
            {
                //[Authorize] içerinde Admin userı çalıştırabilir sadece
                productService.WithDraw(accountNumber, 200000);
            });

            Run(() =>
            {
                //[Exception] attribute için hata fırlatıyoruz içeride
                productService.Deposit(accountNumber, 200000);
            });

            Console.ReadLine();
        }


        public static void Run(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Program main ex" + ex.ToString());
                Console.ResetColor();
            }
        }
    }
}
