using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericUnitOfWorkSample.Model
{
    public class User : BaseModel
    {
        public User()
        {
            FromUserMessages = new List<Message>();
            ToUserMessages = new List<Message>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string Name { get; set; }
        //User ve Message arasında ki relational ilişkimizi bu şekilde kuruyoruz
        public virtual ICollection<Message> FromUserMessages { get; set; }
        public virtual ICollection<Message> ToUserMessages { get; set; }
    }
}
