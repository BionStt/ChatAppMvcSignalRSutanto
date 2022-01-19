using ChatAppMvcSignalRSutanto.Domain.Enum;
using ChatAppMvcSignalRSutanto.Domain;
using ChatAppMvcSignalRSutanto.Infrastructure.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppMvcSignalRSutanto.ServiceApplication.Commands
{
    public class CreatePrivateRoomCommandHandler : IRequestHandler<CreatePrivateRoomCommand,int>
    {
        private readonly AppDbContext _dbContext;

        public CreatePrivateRoomCommandHandler(AppDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public async Task<int> Handle(CreatePrivateRoomCommand request, CancellationToken cancellationToken)
        {
            var chat = new Chat
            {
                Name = request.NameRoom,
                Type = ChatType.Private
            };
            chat.Users.Add(new ChatUser
            {
                UserId = request.userId
            });
            //chat.Users.Add(new ChatUser
            //{
            //    UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            //});
            _dbContext.Chats.Add(chat);
            await _dbContext.SaveChangesAsync();

            return chat.Id;

        }
    }
}
