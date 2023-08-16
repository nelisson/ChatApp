using ChatApp.Server.Services;
using ChatApp.Shared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("list/{chatroomId}")]
        public ActionResult<IEnumerable<Message>> List(int chatroomId)
        {
            var messages = _messageService.GetMessages(chatroomId);

            return Ok(messages);
        }
    }
}
