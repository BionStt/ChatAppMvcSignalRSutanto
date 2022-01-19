using ChatAppMvcSignalRSutanto.Infrastructure.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppMvcSignalRSutanto.ServiceApplication.Commands
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand>
    {
        private readonly AppDbContext _dbContext;

        public SendMessageCommandHandler(AppDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var dtMessage = Domain.Message.CreateMessage( request.Name, request.Text, request.TimeStamp, request.ChatId);
           
            await _dbContext.Messages.AddAsync(dtMessage);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
