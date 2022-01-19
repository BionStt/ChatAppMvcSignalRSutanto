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

        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var chats = _dbContext.ChatUsers
                .Include(x => x.Chat)
                .Where(x => x.UserId == userId
                && x.Chat.Type == ChatType.Room)
                .Select(x => x.Chat)
                .ToList();
            return View(chats);

        }

    }
}
