using System;
using System.Threading.Tasks;
using ChatRoom.Domain.Interfaces;
using ChatRoom.Domain.Models;
using ChatRoom.Infrastructure.Hubs;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IChatRepository _chatRepository;

        public ChatController(IHubContext<ChatHub> hubContext, IPublishEndpoint publishEndpoint, IChatRepository chatRepository)
        {
            _hubContext = hubContext;
            _publishEndpoint = publishEndpoint;
            _chatRepository = chatRepository;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Send([FromBody]Message message)
        {
            if (!ModelState.IsValid) 
                return BadRequest();

            try
            {
                if (message.Content.StartsWith("/"))
                {
                    await _publishEndpoint.Publish(message);
                }
                else
                {
                    _chatRepository.AddMessage(message);
                    await _hubContext.Clients.All.SendAsync(ChatHubMethods.ReceiveMessage, message);
                }

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _chatRepository.GetMessagesAsync());
            }
            catch (Exception )
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
