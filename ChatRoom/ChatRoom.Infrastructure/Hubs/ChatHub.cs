using System.Threading.Tasks;
using ChatRoom.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Infrastructure.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        public Task SendMessage(Message message) => Clients.All.SendAsync(ChatHubMethods.ReceiveMessage, message);

    }
}
