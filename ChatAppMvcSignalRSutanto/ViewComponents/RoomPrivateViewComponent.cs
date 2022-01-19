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
    public class RoomPrivateViewComponent : ViewComponent
    {
        private AppDbContext _dbContext;

        public RoomPrivateViewComponent(AppDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var chats = _dbContext.ChatUsers
                .Include(x => x.Chat)
                .Where(x => x.UserId == userId
                && x.Chat.Type == ChatType.Private)
                .Select(x => x.Chat)
                .ToList();
            //  return View(chats);
            return await Task.FromResult((IViewComponentResult)View("RoomPrivate", chats));


        }
    }
}
