using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Spatem.Data.Identity;

namespace Spatem.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {                
                var services = scope.ServiceProvider;
                var hostingEnvironment = services.GetService<IHostingEnvironment>();

                if (hostingEnvironment.IsDevelopment())
                {
                    Seeder.SeedConfigurationData(services.GetRequiredService<ConfigurationDbContext>());
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}