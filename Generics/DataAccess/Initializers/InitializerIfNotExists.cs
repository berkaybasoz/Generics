using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Initializers
{
    public class InitializerIfNotExists <T> : CreateDatabaseIfNotExists<T> where T:DbContext 
    {
        protected override void Seed(T context)
        {
            base.Seed(context);
        }
    }
}
