using Autofac;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shared.Console.Common;
using System;
using System.Threading.Tasks;

namespace MyConsumer
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Log.Information("Starting up");
                await CreateHostBuilder(args).RunConsoleAsync();
            }
            catch (Exception ex)
            {
                if (Log.Logger == null || Log.Logger.GetType().Name == "SilentLogger")
                {
                    // Loading configuration or Serilog failed.
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.Console()
                        .CreateLogger();
                }

                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .InitializeHost()
                .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule<AutofacModule>());
    }
}