using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.AuthServer.Seeds
{
    public class IdentityServerSeedData
    {
        public static async Task Seed(ConfigurationDbContext context)
        {
            if (!await context.Clients.AnyAsync())
            {
                foreach (var item in Config.GetClients())
                {

                    var entity = new IdentityServer4.EntityFramework.Entities.Client
                    {
                        ClientId = item.ClientId,
                        ClientName = item.ClientName,
                        RequireClientSecret = item.RequireClientSecret,
                        AllowedGrantTypes = item.AllowedGrantTypes.Select(s => new ClientGrantType
                        {
                            GrantType = s
                        }).ToList(),
                        AllowedScopes = item.AllowedScopes.Select(s => new ClientScope
                        {
                            Scope = s
                        }).ToList(),
                        AccessTokenLifetime = item.AccessTokenLifetime,
                        AllowOfflineAccess = item.AllowOfflineAccess,
                        RefreshTokenUsage = item.RefreshTokenUsage.GetHashCode(),
                        AbsoluteRefreshTokenLifetime = item.AbsoluteRefreshTokenLifetime
                    };

                    context.Clients.Add(entity);
                }
            }

            if (!await context.ApiResources.AnyAsync())
            {
                foreach (var itemApiResource in Config.GetApiResources())
                {

                    var entity = new ApiResource
                    {
                        Name = itemApiResource.Name,
                        Scopes = itemApiResource.Scopes.Select(s => new ApiResourceScope
                        {
                            Scope = s
                        }).ToList(),
                        Secrets = itemApiResource.Scopes.Select(s => new ApiResourceSecret
                        {
                            Value = s
                        }).ToList(),
                    };
                    context.ApiResources.Add(entity);
                }
            }

            if (!await context.ApiScopes.AnyAsync())
            {
                foreach (var itemApiScope in Config.GetApiScopes())
                {
                    var entity = new ApiScope
                    {

                        Name = itemApiScope.Name,
                        DisplayName = itemApiScope.DisplayName,

                    };
                    context.ApiScopes.Add(entity);
                }
            }

            if (!await context.IdentityResources.AnyAsync())
            {
                foreach (var itemIdentityResource in Config.GetIdentityResources())
                {
                    var enttiy = new IdentityResource
                    {
                        Name = itemIdentityResource.Name,
                        DisplayName = itemIdentityResource.DisplayName,
                        Emphasize = itemIdentityResource.Emphasize,
                        UserClaims = itemIdentityResource.UserClaims.Select(s => new IdentityResourceClaim
                        {
                            Type = s
                        }).ToList()
                    };
                    context.IdentityResources.Add(enttiy);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}

