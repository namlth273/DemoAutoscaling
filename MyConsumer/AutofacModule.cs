using Autofac;
using Autofac.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Shared.Console.Common;
using Shared.MassTransit;

namespace MyConsumer
{
    internal class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterRabbitMqConfig();

            builder.AddMassTransit(massBuilder =>
            {
                massBuilder.UseRabbitMq();

                massBuilder.AddConsumerWithDefaultEndpoint<UserCreatedConsumer>();
            });

            var services = new ServiceCollection();

            services.AddMassTransitHostedService();

            builder.Populate(services);
        }
    }
}