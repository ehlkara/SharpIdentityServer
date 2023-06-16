using IdentityServer4.Models;

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
                }
            };
        }
    }
}
