using CCM.TesteAcesso.Application.Consumers.ProcessOperation;
using CCM.TesteAcesso.Application.Consumers.ProcessTransfer;
using CCM.TesteAcesso.Application.Consumers.ValidateTransfer;
using CCM.TesteAcesso.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Sockets;

namespace CCM.TesteAcesso.Application.Configurations
{
    internal static class MassTransitConfiguration
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var host = configuration.GetSection("MassTransit:Host").Value;
            var user = configuration.GetSection("MassTransit:User").Value;
            var password = configuration.GetSection("MassTransit:Password").Value;
            var virtualHost = configuration.GetSection("MassTransit:VirtualHost").Value;

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<ValidateTransferConsumer, ValidateTransferConsumerDefinition>();
                x.AddConsumer<ProcessTransferConsumer, ProcessTransferConsumerDefinition>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(host, virtualHost, h =>
                    {
                        h.Username(user);
                        h.Password(password);
                    });

                    cfg.AddRetryConsumer<ValidateTransferConsumer, ValidateTransferEvent>(context);
                    cfg.AddRetryConsumer<ProcessTransferConsumer, ProcessTransferEvent>(context);


                    cfg.ConfigureEndpoints(context);
                });
            });


            return services;
        }

        private static void AddRetryConsumer<TConsumerQueue, TEvent>(this IRabbitMqBusFactoryConfigurator config, IBusRegistrationContext context)
            where TConsumerQueue : IConsumer
        {
            config.ReceiveEndpoint($"{typeof(TEvent)}", e =>
            {
                e.UseMessageRetry(r =>
                {
                    r.Exponential(5, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(5));
                    r.Handle<TimeoutException>();
                    r.Handle<Exception>();
                    r.Handle<SocketException>(x => x.SocketErrorCode.Equals(SocketError.TimedOut));
                });

                e.ConfigureConsumer(context, typeof(TConsumerQueue));
            });

            config.ConfigureEndpoints(context);
        }
    }
}
