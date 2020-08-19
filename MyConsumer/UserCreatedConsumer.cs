using Demo.Contracts;
using MassTransit;
using Serilog;
using System;
using System.Threading.Tasks;

namespace MyConsumer
{
    public class UserCreatedConsumer : IConsumer<IUserCreated>
    {
        private readonly ILogger _logger;

        public UserCreatedConsumer(ILogger logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IUserCreated> context)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            _logger.Information("FirstName: {0}", context.Message.FirstName);
        }
    }
}