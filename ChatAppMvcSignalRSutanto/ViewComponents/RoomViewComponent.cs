using ChatAppMvcSignalRSutanto.Domain.Enum;
using ChatAppMvcSignalRSutanto.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppMvcSignalRSutanto.ViewComponents
{
    public class RoomViewComponent:ViewComponent
    {
        private AppDbContext _dbContext;

        public RoomViewComponent(AppDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var chats = _dbContext.ChatUsers
            //    .Include(x => x.Chat)
            //    .Where(x => x.UserId == userId
            //    && x.Chat.Type == ChatType.Room)
            //    .Select(x => x.Chat)
            //    .ToList();


            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var chats = await _dbContext.Chats
          .Include(c => c.Users)
         //  .Where(x => !x.Users.Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
            .Where(c=>c.Users.Any(y=>y.UserId != userId ))
          .ToListAsync();

           // return View(chats);
            return await Task.FromResult((IViewComponentResult)View("Room",chats));


        }

    }
}
