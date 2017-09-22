using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroAttributeSample.Cache
{
    public interface ICacheProvider
    {
        bool TryGetData(string cacheKey, out object data);
        void SetData(string cacheKey, object data);
    }
}
