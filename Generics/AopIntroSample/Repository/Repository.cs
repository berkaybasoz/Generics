using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroSample.Repository
{
    public class Repository<T> : IRepository<T>
    {
        public void Add(T entity)
        {
            Console.WriteLine("Ekleniyor {0}", entity);
        }
        public void Delete(T entity)
        {
            Console.WriteLine("Siliniyor {0}", entity);
        }
        public void Update(T entity)
        {
            Console.WriteLine("Güncelleniyor {0}", entity);
        }
        public IEnumerable<T> GetAll()
        {
            Console.WriteLine("Veriler çekiliyor");
            return null;
        }
        public T GetById(int id)
        {
            Console.WriteLine("Veri çekiliyor {0}", id);
            return default(T);
        }
    }
}
