using IdentityServer.Client1.Models;

namespace IdentityServer.Client1.Services
{
    public interface IApiResourceHttpClient
    {
        Task<HttpClient> GetHttpClient();
        Task<List<string>> RegisterUser(UserSaveViewModel userSaveViewModel);
    }
}
