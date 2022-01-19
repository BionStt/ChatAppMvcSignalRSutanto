using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppMvcSignalRSutanto.ServiceApplication.Commands
{
    public class CreatePrivateRoomCommand:IRequest<int>
    {
        public string userId { get; set; }
        public string NameRoom { get; set; }

    }
}
