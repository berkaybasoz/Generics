using AopIntroSample.Model;
using AopIntroSample.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AopIntroSample
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateFlexibleDynamicProxy();
            //CreateFilteredDynamicProxy();
            //AuthenticationProxyLogging();
            //DynamicProxyLogging();
            //SimpleMethodLogging();
            //NoMethodLogging();
        }
        private static void CreateFlexibleDynamicProxy()
        {
            Console.WriteLine("***\r\n CreateFlexibleDynamicProxy Loglama program başladı \r\n");
            IRepository<Customer> customerRepository =
              RepositoryFactory.CreateFlexibleDynamicProxy<Customer>();
            var customer = new Customer
            {
                Id = 1,
                Name = "Müşteri 1",
                Address = "Adres 1"
            };
            customerRepository.Add(customer);
            customerRepository.Update(customer);
            customerRepository.Delete(customer);
            customerRepository.GetAll();
            customerRepository.GetById(1);
            Console.WriteLine("\r\n CreateFlexibleDynamicProxy Loglama program bitti \r\n***");
            Console.ReadLine();
        }
        private static void CreateFilteredDynamicProxy()
        {
            Console.WriteLine("***\r\n CreateFilteredDynamicProxy Loglama program başladı \r\n");
            IRepository<Customer> customerRepository =
              RepositoryFactory.CreateFilteredDynamicProxy<Customer>();
            var customer = new Customer
            {
                Id = 1,
                Name = "Müşteri 1",
                Address = "Adres 1"
            };
            customerRepository.Add(customer);
            customerRepository.Update(customer);
            customerRepository.Delete(customer);
            customerRepository.GetAll();
            customerRepository.GetById(1);
            Console.WriteLine("\r\n CreateFilteredDynamicProxy Loglama program bitti \r\n***");
            Console.ReadLine();
        }

        private static void AuthenticationProxyLogging()
        {
            Console.WriteLine("***\r\n AuthenticationProxyLogging Loglama program başladı \r\n");
            Console.WriteLine("\r\nadmin olarak çalışıyor");
            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("Administrator"), new[] { "ADMIN" });
            IRepository<Customer> customerRepository =
              RepositoryFactory.CreateAuthenticationProxy<Customer>();
            var customer = new Customer
            {
                Id = 1,
                Name = "Müşteri 1",
                Address = "Adres 1"
            };
            customerRepository.Add(customer);
            customerRepository.Update(customer);
            customerRepository.Delete(customer);
            Console.WriteLine("\r\nnormal kullanıcı olarak çalışıyor");
            Thread.CurrentPrincipal =
              new GenericPrincipal(new GenericIdentity("NormalUser"),
              new string[] { });
            customerRepository.Add(customer);
            customerRepository.Update(customer);
            customerRepository.Delete(customer);
            Console.WriteLine("\r\n AuthenticationProxyLogging Loglama program bitti \r\n***");
            Console.ReadLine();
        }

        private static void DynamicProxyLogging()
        {
            Console.WriteLine("***\r\n DynamicProxyLogging Loglama program başladı \r\n");
            IRepository<Customer> customerRepository =
              RepositoryFactory.CreateDynamicProxy<Customer>();
            var customer = new Customer
            {
                Id = 1,
                Name = "Müşteri 1",
                Address = "Adres 1"
            };
            customerRepository.Add(customer);
            customerRepository.Update(customer);
            customerRepository.Delete(customer);
            Console.WriteLine("\r\n DynamicProxyLogging Loglama program bitti \r\n***");
            Console.ReadLine();
        }

        private static void SimpleMethodLogging()
        {
            Console.WriteLine("***\r\n SimpleMethodLogging Loglama program başladı \r\n");
            IRepository<Customer> customerRepository =
              new LoggerRepository<Customer>(new Repository<Customer>());
            var customer = new Customer
            {
                Id = 1,
                Name = "Müşteri 1",
                Address = "Adres 1"
            };
            customerRepository.Add(customer);
            customerRepository.Update(customer);
            customerRepository.Delete(customer);
            Console.WriteLine("\r\n SimpleMethodLogging Loglama program bitti \r\n***");
            Console.ReadLine();
        }

        private static void NoMethodLogging()
        {
            Console.WriteLine("***\r\n NoMethodLogging Loglama program başladı \r\n");
            IRepository<Customer> customerRepository =
              new Repository<Customer>();
            var customer = new Customer
            {
                Id = 1,
                Name = "Müşteri 1",
                Address = "Adres 1"
            };
            customerRepository.Add(customer);
            customerRepository.Update(customer);
            customerRepository.Delete(customer);
            Console.WriteLine("\r\n NoMethodLogging Loglama program bitti \r\n***");
            Console.ReadLine();
        }
    }
}
