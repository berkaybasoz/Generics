using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System;

namespace GenericUnitOfWorkSample.Model
{
    public class Message : BaseModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }

        [Required]
        [StringLength(140)]
        public string Text { get; set; }

        //User ve Message arasında ki relational ilişkimizi bu şekilde kuruyoruz
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }

        public virtual User FromUser { get; set; }
        public virtual User ToUser { get; set; }


        public override string ToString()
        {
            return String.Format($"{nameof(MessageId)}:{MessageId}, {nameof(Text)}:{Text}, {nameof(FromUser)}:{FromUser.Name}, {nameof(ToUser)}:{ToUser.Name}, {nameof(TimeStampValue)}:{TimeStampValue}");
        }
    }
}