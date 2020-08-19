using Demo.Contracts;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyPublisher
{
    internal class DemoHostedService : BackgroundService
    {
        private readonly ILogger<DemoHostedService> _logger;
        private readonly IPublishEndpoint _endpoint;
        private readonly IConfig _config;
        private readonly IHostApplicationLifetime _lifetime;

        public DemoHostedService(ILogger<DemoHostedService> logger, IPublishEndpoint endpoint, IConfig config, IHostApplicationLifetime lifetime)
        {
            _logger = logger;
            _endpoint = endpoint;
            _config = config;
            _lifetime = lifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested) return;

            if (_config.NumberOfMessageToPublish == null) return;

            for (int i = 0; i < _config.NumberOfMessageToPublish; i++)
            {
                var id = Guid.NewGuid();

                await _endpoint.Publish<IUserCreated>(new UserCreated
                {
                    CorrelationId = id,
                    FirstName = $"Nam {id}",
                    LastName = "Le"
                }, stoppingToken);

                //await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }

            _lifetime.StopApplication();
        }
    }
}