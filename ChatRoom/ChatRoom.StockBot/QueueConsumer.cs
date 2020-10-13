using System;
using System.Threading.Tasks;
using ChatRoom.Domain.Models;
using ChatRoom.Infrastructure.Hubs;
using ChatRoom.StockBot.Exceptions;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.StockBot
{
    internal class QueueConsumer : IConsumer<Message>
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IStockProcessor _stockProcessor;

        public QueueConsumer(IHubContext<ChatHub> hubContext, IStockProcessor stockProcessor)
        {
            _hubContext = hubContext;
            _stockProcessor = stockProcessor;
        }

        public async Task Consume(ConsumeContext<Message> context)
        {
            try
            {
                var info = _stockProcessor.GetStockInfo(context.Message.Content);

                context.Message.Content = $"{info.Symbol.ToUpper()} quote is ${info.Close} per share";
            }
            catch (Exception ex) when (ex is InvalidCommandExpressionException || ex is InvalidCommandException ||
                                       ex is InvalidStockArgumentException)
            {
                context.Message.Content = ex.Message;
            }
            catch (Exception)
            {
                context.Message.Content = "Sorry! An unexpected behavior has occured. Please contact me later.";
            }
            finally
            {
                context.Message.User = "Stocky";
                context.Message.Timestamp = DateTime.Now;

                await _hubContext.Clients.All.SendAsync(ChatHubMethods.ReceiveMessage, context.Message);
            }
        }
    }
}
