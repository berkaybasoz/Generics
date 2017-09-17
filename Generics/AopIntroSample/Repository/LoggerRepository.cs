using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroSample.Repository
{
    public class LoggerRepository<T> : IRepository<T>
    {
        private readonly IRepository<T> _decorated;
        public LoggerRepository(IRepository<T> decorated)
        {
            _decorated = decorated;
        }
        private void Log(string msg, object arg = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg, arg);
            Console.ResetColor();
        }
        public void Add(T entity)
        {
            Log("In decorator - Eklenmeden önce {0}", entity);
            _decorated.Add(entity);
            Log("In decorator - Eklenmeden sonra {0}", entity);
        }
        public void Delete(T entity)
        {
            Log("In decorator - Silinmeden önce {0}", entity);
            _decorated.Delete(entity);
            Log("In decorator - Silindikten sonra {0}", entity);
        }
        public void Update(T entity)
        {
            Log("In decorator - Güncellemeden önce {0}", entity);
            _decorated.Update(entity);
            Log("In decorator - Güncellendikten sonra {0}", entity);
        }
        public IEnumerable<T> GetAll()
        {
            Log("In decorator - Veriler çekilmeden önce");
            var result = _decorated.GetAll();
            Log("In decorator - Veriler çekildikten sonra");
            return result;
        }
        public T GetById(int id)
        {
            Log("In decorator - Veriler çekilmeden önce {0}", id);
            var result = _decorated.GetById(id);
            Log("In decorator - Veriler çekildikten sonra {0}", id);
            return result;
        }
    }
}
