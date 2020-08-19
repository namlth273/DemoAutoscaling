using Autofac;
using Autofac.Extensions.DependencyInjection;
using Demo.Contracts;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Console.Common;
using Shared.MassTransit;

namespace MyPublisher
{
    internal class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterRabbitMqConfig();

            builder.AddMassTransit(massBuilder => { massBuilder.UseRabbitMq(); });

            MassTransit.Context.MessageCorrelation.UseCorrelationId<IUserCreated>(c => c.CorrelationId);

            builder.Register(r =>
            {
                var config = new WorkerConfig();
                var configuration = r.Resolve<IConfiguration>();
                config.NumberOfMessageToPublish = configuration.GetValue<int?>("NumberOfMessageToPublish");
                return config;
            }).As<IConfig>();

            var services = new ServiceCollection();

            services.AddHostedService<DemoHostedService>();

            builder.Populate(services);
        }
    }
}