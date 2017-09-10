using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Extensions;

namespace GenericUnitOfWorkSample.Model
{
    public abstract class BaseModel
    {
        public DateTime? CreateDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [Timestamp]
        public byte[] TimeStamp { get; set; }

        [NotMapped]
        public long TimeStampValue
        {
            get { return TimeStamp.GetTimeStampValue(); }
        } 
    }
}
