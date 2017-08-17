using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.Args
{
    public class BaseArgs<T> : ItemEventArgs<T>
    {
        public BaseArgs(T item) : base(item)
        {

        }
    }
}
