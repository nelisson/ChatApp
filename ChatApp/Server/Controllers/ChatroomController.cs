using ChatApp.Server.Data;
using ChatApp.Server.Services;
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
        private readonly IChatroomService _chatroomService;

        public ChatroomController(IChatroomService chatroomService)
        {
            _chatroomService = chatroomService;
        }

        // GET: api/Chatroom
        [HttpGet]
        public ActionResult<IEnumerable<Chatroom>> GetChatrooms()
        {
            return _chatroomService.GetChatrooms().ToList();
        }

        // POST: api/Chatroom
        [HttpPost]
        public async Task<ActionResult<Chatroom>> CreateChatroom()
        {
            var chatroom = await _chatroomService.CreateChatroom();
            return CreatedAtAction("GetChatrooms", new { id = chatroom.Id }, chatroom);
        }
    }
}
