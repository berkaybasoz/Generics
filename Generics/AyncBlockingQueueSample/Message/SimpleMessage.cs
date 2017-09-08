using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyncBlockingQueueSample.Message
{
    public class SimpleMessage : IMessage
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public SimpleMessage(int id, string msg = "")
        {
            Id = id;
            Message = msg;
        }

        public override string ToString()
        {
            return String.Format("{0} => {1}, {2} => {3}", nameof(Id), Id, nameof(Message), Message);
        }
    }
}
