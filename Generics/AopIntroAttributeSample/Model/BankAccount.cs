using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroAttributeSample.Model
{
    public class BankAccount
    {
        public decimal Money { get; set; }
        public int BranchCode { get; set; }
        public int AccountNumber { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"{nameof(AccountNumber)} : {AccountNumber}, ");
            builder.Append($"{nameof(BranchCode)} : {BranchCode}, ");
            builder.Append($"{nameof(Money)} : {Money}");
            return builder.ToString();
        }
    }
}
