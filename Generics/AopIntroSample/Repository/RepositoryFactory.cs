using AopIntroSample.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopIntroSample.Repository
{
    public class RepositoryFactory
    {
        public static IRepository<T> CreateDynamicProxy<T>()
        {
            var repository = new Repository<T>();
            var dynamicProxy = new DynamicProxy<IRepository<T>>(repository);
            return dynamicProxy.GetTransparentProxy() as IRepository<T>;
        }

        public static IRepository<T> CreateAuthenticationProxy<T>()
        {
            var repository = new Repository<T>();
            var decoratedRepository =
              (IRepository<T>)new DynamicProxy<IRepository<T>>(
              repository).GetTransparentProxy();

            decoratedRepository =
              (IRepository<T>)new AuthenticationProxy<IRepository<T>>(
              decoratedRepository).GetTransparentProxy();
            return decoratedRepository;
        }

        public static IRepository<T> CreateFilteredDynamicProxy<T>()
        {
            var repository = new Repository<T>();
            var dynamicProxy = new FilteredDynamicProxy<IRepository<T>>(repository)
            {
                Filter = m => !m.Name.StartsWith("Get")
            };
            return dynamicProxy.GetTransparentProxy() as IRepository<T>;
        }

        public static IRepository<T> CreateFlexibleDynamicProxy<T>()
        {
            var repository = new Repository<T>();
            var dynamicProxy = new FlexibleDynamicProxy<IRepository<T>>(repository);
            dynamicProxy.BeforeExecute += (s, e) => Log(
              "'{0}' çalıştırılmadan önce", e.MethodName);
            dynamicProxy.AfterExecute += (s, e) => Log(
              "'{0}' çalıştırıldıktan sonra", e.MethodName);
            dynamicProxy.ErrorExecuting += (s, e) => Log(
              "'{0}' çalıştırılırken hata alındı", e.MethodName);
            dynamicProxy.Filter = m => !m.Name.StartsWith("Get");
            return dynamicProxy.GetTransparentProxy() as IRepository<T>;
        }
         
        private static void Log(string msg, object arg = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg, arg);
            Console.ResetColor();
        }
    }
}
