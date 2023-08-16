using ChatApp.Shared.Model;
using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
    DbSet<Chatroom> Chatrooms { get; set; }
    DbSet<Message> Messages { get; set; }
    Task<int> SaveChangesAsync();
}
