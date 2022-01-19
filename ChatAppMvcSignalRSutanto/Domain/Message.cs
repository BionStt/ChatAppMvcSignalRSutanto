using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppMvcSignalRSutanto.Domain
{
    public class Message
    {
        public Message( string name, string text, DateTime timestamp, int chatID)
        {
          
            Name=name;
            Text=text;
            Timestamp=timestamp;
            ChatID=chatID;
          
        }
        public static Message CreateMessage( string name, string text, DateTime timestamp, int chatID)
        {
            return new Message(name,text,timestamp,chatID);
        }
        public int Id { get; set; } // IdRoom
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public int ChatID { get; set; }
        public Chat Chat { get; set; }
    }
}
