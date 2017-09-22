using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AopIntroAttributeSample.Model;
using AopIntroAttributeSample.Attr;
using System.Threading;
using System.Data.SqlClient;

namespace AopIntroAttributeSample.Service
{
    public class DummyBankAccountService : IBankAccountService
    {
        private static BankAccountCollection bankAccounts = new DummyBankAccountCollection();

        [Log]
        [Cache(DurationInMinute = 10)]
        public BankAccountCollection GetBankAccounts(int branchCode)
        {
            Thread.Sleep(1000);
            return new BankAccountCollection(bankAccounts.Where(w => w.BranchCode == branchCode));
        }

        [Exception]
        public void Deposit(int accountNumber, decimal money)
        { 
            // Exception attribute içerisinde burda fırlattığımız hatayı loglayıp friendly name ile değiştireceğiz
            throw new NotImplementedException();
        }

    }
}
