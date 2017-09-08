using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyncBlockingQueueSample.Message
{
    public interface IMessage
    {
        [JsonProperty("id")]
        int Id { get; set; }

        //MessageData Data { get; }
        [JsonProperty("message")]
        string Message { get; set; }  
    }
}
