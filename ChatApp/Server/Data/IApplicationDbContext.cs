using Microsoft.EntityFrameworkCore;
using ChatApp.Shared.Model;

public interface IApplicationDbContext
{
    DbSet<Chatroom> Chatrooms { get; set; }
    DbSet<Message> Messages { get; set; }
    Task<int> SaveChangesAsync();
}
