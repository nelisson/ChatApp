using ChatApp.Server.Data;
using ChatApp.Shared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public MessageController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("list/{chatroomId}")]
        public async Task<ActionResult<IEnumerable<Message>>> List(int chatroomId)
        {
            var messages = await _dbContext.Messages
                .Where(m => m.ChatroomId == chatroomId)
                .OrderByDescending(m => m.Timestamp)     
                .Take(50)
                .ToListAsync();

            return Ok(messages);
        }
    }
}
