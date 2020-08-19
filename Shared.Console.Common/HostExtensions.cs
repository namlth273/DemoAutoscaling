using System;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shared.Common;

namespace Shared.Console.Common
{
    public static class HostExtensions
    {
        private static void ConfigureConfigJsonFiles(this IConfigurationBuilder builder)
        {
            // Read environment variable to know which environment the application is (Development or Staging or Production)
            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ??
                              EnumEnvironment.Production.ToString();

            if (environment.Equals(EnumEnvironment.Production.ToString())) return;

            var homeDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var machineConfigFile = Path.Combine(homeDir, $"demo.appsettings.{environment}.json");

            builder.AddJsonFile(machineConfigFile, optional: true);
        }

        public static IHostBuilder InitializeHost(this IHostBuilder builder)
        {
            builder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration((context, configApp) => configApp.ConfigureConfigJsonFiles())
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                });

            return builder;
        }
    }
}