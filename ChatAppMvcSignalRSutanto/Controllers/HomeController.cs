using ChatAppMvcSignalRSutanto.Domain.Enum;
using ChatAppMvcSignalRSutanto.Infrastructure.Context;
using ChatAppMvcSignalRSutanto.Models;
using ChatAppMvcSignalRSutanto.ServiceApplication.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace ChatAppMvcSignalRSutanto.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMediator _mediator;
        private readonly AppDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, IMediator mediator, AppDbContext dbContext)
        {
            _logger = logger;
            _mediator=mediator;
            _dbContext=dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var chats = await _dbContext.Chats
            .Include(c => c.Users)
            //.Where(x => !x.Users.Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
            .ToListAsync();
            return View(chats);
        }
        public async Task<IActionResult> Find()
        {
            var users = await _dbContext.Users
            .Where(x => x.Id != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            .ToListAsync();
            return View(users);
        }
        public async Task<IActionResult> Private()
        {
            var chats = _dbContext.Chats
            .Include(x => x.Users)
                .ThenInclude(x => x.User)
            .Where(x => x.Type == ChatType.Private
             && x.Users
             .Any(y => y.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value))
             .ToList();
            return View(chats);
        }
        public IActionResult CreatePrivateRoom()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePrivateRoom(string name)
        {
            var chatId = await _mediator.Send(new CreatePrivateRoomCommand {NameRoom = name,userId = User.FindFirst(ClaimTypes.NameIdentifier).Value });

          return RedirectToAction("Chat", new { id = chatId });
        }

        [HttpGet("{id}")]
        public IActionResult Chat(int id)
        {
            var chat = _dbContext.Chats
                .Include(x => x.Messages)
                .FirstOrDefault(x => x.Id == id);
            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int roomId, string message)
        {

            var Message = Domain.Message.CreateMessage(User.Identity.Name, message, DateTime.Now, roomId);
            _dbContext.Messages.Add(Message);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Chat", new { id = roomId });
        }
        public IActionResult CreateRoom()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            var chat = new Domain.Chat
            {
                Name = name,
                Type = ChatType.Room
            };
            chat.Users.Add(new Domain.ChatUser
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Role = UserRole.Admin
            });
            await _dbContext.Chats.AddAsync(chat);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> JoinRoom(int id)
        {
            //   var xx = _dbContext.ChatUsers.Where(chatUser => chatUser.UserId ==User.FindFirst(ClaimTypes.NameIdentifier).Value).SingleOrDefaultAsync();
            var xx = await _dbContext.ChatUsers.AnyAsync(x=>x.UserId ==User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (!xx)
            {
                var chatUser = new Domain.ChatUser
                {
                    ChatId = id,
                    UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                    Role = UserRole.Member
                };
                await _dbContext.ChatUsers.AddAsync(chatUser);
                await _dbContext.SaveChangesAsync();
            }
         
            return RedirectToAction("Chat", "Home", new { id = id });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}