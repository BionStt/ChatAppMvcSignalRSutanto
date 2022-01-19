using ChatAppMvcSignalRSutanto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppMvcSignalRSutanto.Dto
{
    public class ChatByIdResponse
    {
        public int ChatId { get; set; }
        public string Name { get; set; }
        public int ChatType { get; set; }
        public ICollection<Message> Messages { get; set; }

    }
}
