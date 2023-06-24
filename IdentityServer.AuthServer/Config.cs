using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServer.AuthServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResource()
        {
            return new List<ApiResource>()
            {
                new ApiResource("resource_api1"){Scopes={"api1.read","api1.write","api1.update"},
                ApiSecrets= new[] {new Secret("secretapi1".Sha256()) } },
                new ApiResource("resource_api2"){Scopes={"api2.read","api2.write","api2.update"},
                ApiSecrets= new[] {new Secret("secretapi2".Sha256()) }}
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope("api1.read","API1 read permission"),
                new ApiScope("api1.write","API1 write permission"),
                new ApiScope("api1.update","API1 update permission"),
                new ApiScope("api2.read","API1 read permission"),
                new ApiScope("api2.write","API1 write permission"),
                new ApiScope("api2.update","API1 update permission"),
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.Email(),
                new IdentityResources.OpenId(), //subId
                new IdentityResources.Profile(),
                new IdentityResource()
                {
                    Name = "CountryAndCity",
                    DisplayName = "Country And City",
                    Description = "User's country and city information",
                    UserClaims = new[] {"country","city"}
                },
                new IdentityResource()
                {
                    Name = "Roles",
                    DisplayName = "Roles",
                    Description = "User roles",
                    UserClaims = new[] {"role"}
                }
            };
        }

        public static IEnumerable<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser {
                    SubjectId="1",Username="ehlkara",Password="password", Claims= new List<Claim>()
                    {
                        new Claim("given_name","Ehlullah"),
                        new Claim("family_name","Karakurt"),
                        new Claim("country","Turkey"),
                        new Claim("city","Istanbul"),
                        new Claim("role","admin")
                    },
                },
                new TestUser {
                    SubjectId="2",Username="ahmet16",Password="password", Claims= new List<Claim>()
                    {
                        new Claim("given_name","Ahmet"),
                        new Claim("family_name","Karakurt"),
                        new Claim("country","Turkey"),
                        new Claim("city","Sivas"),
                        new Claim("role", "customer")
                    },
                },
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "Client1",
                    ClientName = "Client 1 app application",
                    ClientSecrets = new[] {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"api1.read"}
                },
                new Client()
                {
                    ClientId = "Client2",
                    ClientName = "Client 2 app application",
                    ClientSecrets = new[] {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"api1.read", "api1.update", "api2.write","api2.update"}
                },
                new Client()
                {
                    ClientId = "Client1-Mvc",
                    RequirePkce = false,
                    ClientName = "Client1-Mvc app application",
                    ClientSecrets = new[] {new Secret("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.Hybrid,
                    RedirectUris = new List<string>() { "https://localhost:7089/signin-oidc", "https://localhost:7089/signin-facebook", "https://localhost:7089/signin-google", "https://localhost:7089/signin-apple" },
                    PostLogoutRedirectUris = new List<string>() {"https://localhost:7089/signout-callback-oidc","https://localhost:7089/signout-callback-facebook","https://localhost:7089/signout-callback-google", "https://localhost:7089/signout-callback-apple"},
                    AllowedScopes= {IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,"api1.read",
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "CountryAndCity","Roles"
                    },
                    AccessTokenLifetime = 2*60*60,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                    RequireConsent = true,
                },
                new Client()
                {
                    ClientId = "Client2-Mvc",
                    RequirePkce = false,
                    ClientName = "Client2-Mvc app application",
                    ClientSecrets = new[] {new Secret("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.Hybrid,
                    RedirectUris = new List<string>() { "https://localhost:7135/signin-oidc", "https://localhost:7135/signin-facebook", "https://localhost:7135/signin-google", "https://localhost:7135/signin-apple" },
                    PostLogoutRedirectUris = new List<string>() { "https://localhost:7135/signout-callback-oidc", "https://localhost:7135/signout-callback-facebook", "https://localhost:7135/signout-callback-google", "https://localhost:7135/signout-callback-apple"},
                    AllowedScopes= {IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,"api1.read","api2.read",
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "CountryAndCity","Roles"
                    },
                    AccessTokenLifetime = 2*60*60,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                    RequireConsent = false,
                }
            };
        }
    }
}
