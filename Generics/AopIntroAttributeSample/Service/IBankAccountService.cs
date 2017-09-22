using AopIntroAttributeSample.Model;
using Generics.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroAttributeSample.Service
{
    public interface IBankAccountService
    {
        BankAccountCollection GetBankAccounts(int branchCode); 
        void WithDraw(int accountNumber, decimal money);
        void Deposit(int accountNumber, decimal money);
    }
}
