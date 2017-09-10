using DataAccess.Context;
using GenericUnitOfWorkSample.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericUnitOfWorkSample.Context
{
    public class MessageContext : BaseContext
    {
        public MessageContext()
            : base("name=MessageContext")
        { 
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //Relation bağlantısı kurma örneği 
            // İlişkileri kuruyoruz one-to-many olarak.
            modelBuilder.Entity<Message>()
               .HasRequired<User>(x => x.FromUser)
               .WithMany(x => x.FromUserMessages)
               .HasForeignKey(x => x.FromUserId)
               .WillCascadeOnDelete(false); ;

            modelBuilder.Entity<Message>()
               .HasRequired<User>(x => x.ToUser)
                .WithMany(x => x.ToUserMessages)
               .HasForeignKey(x => x.ToUserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
