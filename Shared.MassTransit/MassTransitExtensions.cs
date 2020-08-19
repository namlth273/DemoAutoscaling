using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Contracts.Configs;
using MassTransit;
using MassTransit.AutofacIntegration;

namespace Shared.MassTransit
{
    public static class MassTransitExtensions
    {
        public static void AddConsumerWithDefaultEndpoint<T>(this IContainerBuilderBusConfigurator builder)
            where T : class, IConsumer
        {
            builder.AddConsumer<T>().Endpoint(e =>
            {
                e.Name = typeof(T).FullName;
                e.PrefetchCount = 1;
            });
        }

        public static IEnumerable<Type> GetAllTypesFromAssembly<T>(Type genericType)
        {
            if (!genericType.IsGenericTypeDefinition)
                throw new ArgumentException("Specified type must be a generic type definition.", nameof(genericType));

            return typeof(T).Assembly
                .GetTypes()
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                              i.GetGenericTypeDefinition() == genericType));
        }

        public static void UseRabbitMq(this IContainerBuilderBusConfigurator builder)
        {
            builder.UsingRabbitMq((context, config) =>
            {
                var rabbitConfig = context.GetService<IRabbitMqConfig>();

                config.Host(rabbitConfig.Host, rabbitConfig.VirtualHost, h =>
                {
                    h.Username(rabbitConfig.Username);
                    h.Password(rabbitConfig.Password);
                });

                config.ConfigureEndpoints(context);
            });
        }
    }
}