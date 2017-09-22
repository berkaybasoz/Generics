using AopIntroAttributeSample.Interception;
using AopIntroAttributeSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroAttributeSample.Attr
{
    public class CacheAttribute : InterceptAttribute, IPreInterception, IPostVoidInterception
    {
        public int DurationInMinute { get; set; }

        public object OnPre(PreInterceptArgs e)
        {
            string cacheKey = string.Format("{0}_{1}", e.MethodName, string.Join("_", e.Arguments));
             
            // gerekli cache key ile kontrol ederek varsa cache'de çağırım öncesi metot'u execute
            // etmeden cache üzerinden ilgili veriyi geri dön.

            Console.WriteLine("{0} isimli cache key ile cache üzerinden geliyorum!", cacheKey);
            return new BankAccountCollection(new List<BankAccount>() { new BankAccount() { AccountNumber = 1000, BranchCode = 1000, Money = -1000 } });

        }

        public void OnPost(PostInterceptArgs e)
        {
            string cacheKey = string.Format("{0}_{1}", e.MethodName, string.Join("_", e.Arguments));

            // cache key ile ilgili veriyi DurationInMinute kullanarak Cache'e ekle veya güncelle.
        }
    }
}
