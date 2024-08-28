using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ManjDb.DataModels;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ManjDb
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Run migration
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<MyDbContext>();
                try
                {
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            // Run the application
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()  // Set as Windows Service so it runs in background and can be set to auto restart
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                    // Register Startup class
                    var startup = new Startup(hostContext.Configuration, hostContext.HostingEnvironment);

                    startup.ConfigureServices(services);
                });
    }
}