using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Shared.Model
{
    public class Message
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
        public int ChatroomId { get; set; }
        public virtual Chatroom Chatroom { get; set; }
    }
}
