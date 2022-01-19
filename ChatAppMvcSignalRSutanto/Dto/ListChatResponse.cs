using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppMvcSignalRSutanto.Dto
{
    public class ListChatResponse
    {
        public int ChatId { get; set; }
        public string Name { get; set; }
        public int ChatType { get; set; }
        public string UserId { get; set; }
        public int MyProperty { get; set; }
    }
}
