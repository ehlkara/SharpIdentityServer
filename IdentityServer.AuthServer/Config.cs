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
                new IdentityResources.OpenId(), //subId
                new IdentityResources.Profile(),
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
                        new Claim("family_name","Karakurt")
                    },
                },
                new TestUser {
                    SubjectId="2",Username="ahmet16",Password="password", Claims= new List<Claim>()
                    {
                        new Claim("given_name","Ahmet"),
                        new Claim("family_name","Karakurt")
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
                    ClientName = "Client1-Mvc app application",
                    ClientSecrets = new[] {new Secret("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.Hybrid,
                    RedirectUris = new List<string>() { "https://localhost:7089/sign-oidc" },
                    AllowedScopes= {IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile},
                }
            };
        }
    }
}
