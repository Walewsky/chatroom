using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatRoom.Domain.Interfaces;
using ChatRoom.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ChatRoomDbContext _context;

        public ChatRepository(ChatRoomDbContext context)
        {
            _context = context;
        }

        public Task<List<Message>> GetMessagesAsync()
        {
            return _context.Messages.OrderBy(x => x.Timestamp).Take(50).ToListAsync();
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
        }
    }
}
