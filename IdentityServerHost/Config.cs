using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServerHost;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope("api", "My API"),
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new[]
        {
            new ApiResource("backend.api")
            {
                Scopes = { "api" }
            }
        };

    public static IEnumerable<Client> Clients =>
        new[]
        {
            // machine to machine client
            new Client
            {
                ClientId = "backend_api",
                ClientName = "Backend API",
                ClientSecrets = { new Secret("s@ha$$a".Sha256()) },
                
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "api" }
            },

            // interactive ASP.NET Core Web App
            new Client
            {
                ClientId = "web",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                    
                // where to redirect to after login
                RedirectUris = { "https://localhost:5002/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                
                AllowOfflineAccess = true,
                
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api"
                }
            },

            // vue frontend client 1
            new Client
            {
                ClientId = "frontend_1",
                ClientName = "Vue Frontend Client 1",
                ClientSecrets = { new Secret("s@ha$$a".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                //AccessTokenLifetime = 75,
                
                RedirectUris = { "http://localhost:8080/auth/signin" },
                //FrontChannelLogoutUri = "http://localhost:8080/signout-oidc",
                PostLogoutRedirectUris = { "http://localhost:8080/home" },
                AllowedCorsOrigins = { "http://localhost:8080" },
                
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "api" }
            },

            // vue frontend client 2
            new Client
            {
                ClientId = "frontend_2",
                ClientName = "Vue Frontend Client 2",
                ClientSecrets = { new Secret("s@ha$$a".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                //AccessTokenLifetime = 75,
                
                RedirectUris = { "http://localhost:8081/auth/signin" },
                PostLogoutRedirectUris = { "http://localhost:8081/home" },
                AllowedCorsOrigins = { "http://localhost:8081" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "api" }
            },

            // flutter client
            new Client
            {
                ClientId = "flutter",
                ClientName = "Flutter Client",
                ClientSecrets = { new Secret("s@ha$$a".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                //AccessTokenLifetime = 75,
                
                RedirectUris = { "com.example.myapp/callback" },
                //PostLogoutRedirectUris = { "com.example.myapp/callback" },
                AllowedCorsOrigins = { "http://localhost:8082" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "api" }
            }
        };
}
