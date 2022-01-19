using ChatAppMvcSignalRSutanto.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppMvcSignalRSutanto.Domain
{
    public class ChatUser
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int ChatId { get; set; }//ChatRoomId
        public Chat Chat { get; set; }
        public UserRole Role { get; set; }
    }
}
