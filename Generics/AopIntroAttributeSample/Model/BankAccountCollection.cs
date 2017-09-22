using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroAttributeSample.Model
{
    public class BankAccountCollection : List<BankAccount>
    {
        public BankAccountCollection()
        {

        }

        public BankAccountCollection(IEnumerable<BankAccount> accounts)
        {
            AddRange(accounts);
        } 
    }
}
