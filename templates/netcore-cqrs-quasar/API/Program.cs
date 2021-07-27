
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace API
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            ILogger logger = host.Services.GetService<ILogger<Program>>();
            try
            {
                var seed = args.Contains("/seed");
                if (seed)
                {
                    args = args.Except(new[] { "/seed" }).ToArray();
                }
                if (seed)
                {
                    logger.LogInformation("Seeding database...");
                    var config = host.Services.GetRequiredService<IConfiguration>();
                    var testUserPw = config["SeedUserPW"];

                    using (var scope = host.Services.CreateScope())
                    {
                        var services = scope.ServiceProvider;
                        Persistence.SeedData<Program>.EnsureSeedData(services, testUserPw);
                    }
                    logger.LogInformation("Done seeding database.");
                    return 0;
                }

                logger.LogInformation("Starting host...");
                host.Run();
                return 0;
            }
            catch (Exception ex)

            {
                logger.LogError(ex, "Host terminated unexpectedly.");
                return 1;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}