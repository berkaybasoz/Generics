using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Extensions
{
    public static class ObjectQueryExtensions
    {
        public static ObjectQuery<T> DisablePlanCaching<T>(this ObjectQuery<T> query)
        {
            query.EnablePlanCaching = false;
            return query;
        }
        public static ObjectQuery DisablePlanCaching(this ObjectQuery query)
        {
            query.EnablePlanCaching = false;
            return query;
        }
    }
}
