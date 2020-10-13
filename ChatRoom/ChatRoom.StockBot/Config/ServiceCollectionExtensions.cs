using System;
using ChatRoom.StockBot.Config;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoom.StockBot
{
    public static class ServiceCollectionExtensions
    {
        public static void AddStockBot(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IWebClient, AppWebClient>();
            services.AddTransient<IStockProcessor, StockProcessor>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<QueueConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.UseHealthCheck(context);

                    cfg.Host(
                        new Uri(configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>().HostAddress));
                    
                    cfg.ReceiveEndpoint("messageQueue", e =>
                    {
                        e.UseMessageRetry(r => r.Interval(2, 100));
                        e.ConfigureConsumer<QueueConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}
