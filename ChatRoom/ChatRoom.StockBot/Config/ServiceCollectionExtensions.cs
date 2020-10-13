using System;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoom.StockBot
{
    public static class ServiceCollectionExtensions
    {
        public static void AddStockBot(this IServiceCollection services)
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
                        new Uri("amqps://mxmvbvnt:CAYA_TllRwXgbQSyzlz9LSzCu_71yhPI@fly.rmq.cloudamqp.com/mxmvbvnt"));
                    
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
