using ChatApp.Server.Data;
using ChatApp.Shared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ChatroomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatroomController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Chatroom
        [HttpGet]
        public ActionResult<IEnumerable<Chatroom>> GetChatrooms()
        {
            return _context.Chatrooms.ToList();
        }

        // POST: api/Chatroom
        [HttpPost]
        public ActionResult<Chatroom> CreateChatroom()
        {
            // Unique chatroom name using timestamp
            var chatroomName = $"Chatroom_{DateTime.UtcNow:yyyyMMddHHmmssfff}";
            var chatroom = new Chatroom { Name = chatroomName };

            _context.Chatrooms.Add(chatroom);
            _context.SaveChanges();

            return CreatedAtAction("GetChatrooms", new { id = chatroom.Id }, chatroom);
        }
    }
}
