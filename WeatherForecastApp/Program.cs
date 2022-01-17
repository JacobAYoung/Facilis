using System;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ASP.NETCoreWebApplication
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            ILogger<Program> logger = null;
            try
            {
                var host = CreateHostBuilder(args).Build();
                logger = host.Services.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Host create and ready to run");

                ICache cache = host.Services.GetRequiredService<ICache>();

                await host.RunAsync(CancellationToken.None);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                logger?.LogError(e, "Host exception: process stopped.");
                throw;
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}