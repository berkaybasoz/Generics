using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class ContextFactory<T> where T : new()
    {
        public static T Create()
        {
            T tmp = new T();
            return tmp;
        }
    }
}
