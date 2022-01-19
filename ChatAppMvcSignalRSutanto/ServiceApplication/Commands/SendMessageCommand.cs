using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppMvcSignalRSutanto.ServiceApplication.Commands
{
    public class SendMessageCommand : IRequest
    {
        public int Id { get;  set; }
        public string Name { get;  set; }
        public DateTime TimeStamp { get;  set; }
        public int ChatId { get;  set; }   
        public string Text { get; set; }
   
    }
}
