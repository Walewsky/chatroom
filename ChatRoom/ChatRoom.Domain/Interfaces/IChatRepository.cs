using System.Collections.Generic;
using System.Threading.Tasks;
using ChatRoom.Domain.Models;

namespace ChatRoom.Domain.Interfaces
{
    public interface IChatRepository
    {
        Task<List<Message>> GetMessagesAsync();
        void AddMessage(Message message);
    }
}
