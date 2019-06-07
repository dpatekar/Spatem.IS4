using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Spatem.Identity.Data
{
    public class IdentityServerConfiguration
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("spatem.api", "Spatem API", new string[]{ JwtClaimTypes.Role, JwtClaimTypes.Email, JwtClaimTypes.GivenName, JwtClaimTypes.FamilyName, JwtClaimTypes.Name })
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {   new Client
                {
                    ClientId = "spatem.postman",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "spatem.api"
                    },
                    AllowOfflineAccess = true,
                    RequireConsent = false
                },
                new Client
                {
                    ClientId = "spatem.vue",
                    ClientName = "VUE Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "http://localhost:5005/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5005/signout-callback-oidc" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "spatem.api"
                    },
                    AllowOfflineAccess = false,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false
                },
                new Client
                {
                    ClientId = "spatem.swagger",
                    ClientName = "Swagger Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "http://localhost:5000/swagger/oauth2-redirect.html" },
                    AllowedScopes =
                    {
                        "spatem.api"
                    },
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false
                }
            };
        }
    }
}