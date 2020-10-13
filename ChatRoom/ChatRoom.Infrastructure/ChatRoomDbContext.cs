using ChatRoom.Domain.Models;
using ChatRoom.Infrastructure.EfTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Infrastructure
{
    public class ChatRoomDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public ChatRoomDbContext(DbContextOptions<ChatRoomDbContext> options) : base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MessageEntityConfiguration());
        }
    }
}
