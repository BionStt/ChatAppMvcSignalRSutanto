using ChatAppMvcSignalRSutanto.Hubs;
using ChatAppMvcSignalRSutanto.Infrastructure.Context;
using ChatAppMvcSignalRSutanto.ServiceApplication.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppMvcSignalRSutanto.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private IHubContext<ChatHub> _chat;
        private readonly IMediator _mediator;

        public ChatController(IHubContext<ChatHub> chat, IMediator mediator)
        {
            _chat=chat;
            _mediator=mediator;
        }

        [HttpPost("[action]/{connectionId}/{roomId}")]
        public async Task<IActionResult> JoinRoom(string connectionId, string roomId)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, roomId);
            return Ok();
        }
        [HttpPost("[action]/{connectionId}/{roomId}")]
        public async Task<IActionResult> LeaveRoom(string connectionId, string roomId)
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, roomId);
            return Ok();
        }
        [HttpPost("/Chat/SendMessage")]
        public async Task<IActionResult> SendMesage(
            int roomId,
            string message)
        {
            await _mediator.Send(new SendMessageCommand{
                ChatId =roomId,
                Text = message,
                Name=User.Identity.Name,
                TimeStamp=DateTime.Now 
            });

            await _chat.Clients.Group(roomId.ToString())
                .SendAsync("ReceiveMessage", new
                {
                    Text = message,
                    Name = User.Identity.Name,
                    Timestamp = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")
                });
            return Ok();
        }
    }
}
