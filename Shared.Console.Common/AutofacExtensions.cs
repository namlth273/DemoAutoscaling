using Autofac;
using Demo.Contracts.Configs;
using Microsoft.Extensions.Configuration;

namespace Shared.Console.Common
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterRabbitMqConfig(this ContainerBuilder builder)
        {
            builder.Register(r =>
            {
                var rabbitConfig = new RabbitMqConfig();
                var configuration = r.Resolve<IConfiguration>();
                configuration.GetSection("RabbitMq").Bind(rabbitConfig);
                return rabbitConfig;
            }).As<IRabbitMqConfig>();

            return builder;
        }
    }
}