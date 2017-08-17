using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.Args
{  
    public delegate void ItemEventHandler<T>(object sender, ItemEventArgs<T> e);
      
    public class ItemEventArgs<T> : EventArgs
    {  
        public ItemEventArgs(T item)
        {
            Item = item;
        } 
        public T Item { get; protected set; } 

        public static implicit operator ItemEventArgs<T>(T item) => new ItemEventArgs<T>(item);
    }

}
