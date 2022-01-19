using ChatAppMvcSignalRSutanto.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppMvcSignalRSutanto.Domain
{
    public class Chat//ChatRoom
    {
        public Chat()
        {
            Messages = new List<Message>();
            Users = new List<ChatUser>();
        }
        public int Id { get; set; }
        public string Name { get; set; }//RoomName
        public ChatType Type { get; set; }//Room type
        public ICollection<Message> Messages { get; set; }
        public ICollection<ChatUser> Users { get; set; }
    }
}
