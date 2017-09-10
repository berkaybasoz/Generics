using DataAccess.Context;
using DataAccess.Initializers;
using DataAccess.Repository;
using DataAccess.UnitOfWork;
using GenericUnitOfWorkSample.Context;
using GenericUnitOfWorkSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericUnitOfWorkSample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DataBaseInitializer<MessageContext>.InitializedDatabase();
                using (MessageContext context = ContextFactory<MessageContext>.Create())
                {
                    IUnitOfWork uow = new EFUnitOfWork(context);

                    IRepository<User> userRepository = uow.GetRepository<User>();
                    User user1 = new User() { Name = "Berkay" };
                    User user2 = new User() { Name = "Arda" };
                    userRepository.Add(user1);
                    userRepository.Add(user2);

                    uow.SaveChanges();

                    IRepository<Message> msgRepository = uow.GetRepository<Message>(); 
                    msgRepository.Add(new Message() { FromUser = user1, ToUser = user2, Text = $"Selam. Nasılsın?", CreateDate = DateTime.Now });
                    msgRepository.Add(new Message() { FromUser = user2, ToUser = user1, Text = $"İyiyim", CreateDate = DateTime.Now });
                    uow.SaveChanges();

                    Console.WriteLine("Tüm mesajlar cekiliyor...");
                    foreach (var msg in msgRepository.GetAll())
                    {
                        Console.WriteLine(msg.ToString());
                    }

                    Console.WriteLine("Berkay'ın mesajları çekiliyor");
                    foreach (var msg in msgRepository.GetAll((m) => m.FromUser.Name == "Berkay"))
                    {
                        Console.WriteLine(msg.ToString());
                    }

                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}
