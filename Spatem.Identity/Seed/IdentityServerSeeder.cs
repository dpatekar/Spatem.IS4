using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Spatem.Data.Identity;
using System.Linq;

namespace Spatem.Identity.Seed
{
    public class IdentityServerSeeder
    {
        public static void SeedConfigurationData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in IdentityServerConfiguration.GetClients().ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in IdentityServerConfiguration.GetIdentityResources().ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in IdentityServerConfiguration.GetApiResources().ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}