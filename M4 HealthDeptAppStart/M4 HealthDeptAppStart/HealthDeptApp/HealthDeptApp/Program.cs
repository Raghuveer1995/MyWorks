using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HealthDeptApp.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HealthDeptApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            if (args != null && args.Length > 0 && args[0].Equals("seed", StringComparison.InvariantCultureIgnoreCase))
            {

                Console.WriteLine("Seeding the database");
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    try
                    {
                        var context = services.GetRequiredService<AppDbContext>();
                        // requires using Microsoft.EntityFrameworkCore;
                        context.Database.Migrate();
                        // Requires using HealthDeptApp.Models;

                        // Requires using HealthDeptApp.Models;
                        var hostingEnvironment = services.GetService<IHostingEnvironment>();
                        if (hostingEnvironment.IsProduction())
                        {
                            System.Console.WriteLine("Seeding database");
                            SeedData.Initialize(services);
                        } else
                        {
                            Console.WriteLine("Must be run with --environment production switch");
                        }

                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred seeding the DB.");
                    }
                }
            }
            else
            {
                host.Run();
            }
        }

        public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .Build();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
