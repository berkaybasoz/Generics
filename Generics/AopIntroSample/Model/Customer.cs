using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroSample.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return string.Format($"Müşteri {nameof(Id)}:{Id}, {nameof(Name)}:{Name}, {nameof(Address)}:{Address}");
        }
    }
}
