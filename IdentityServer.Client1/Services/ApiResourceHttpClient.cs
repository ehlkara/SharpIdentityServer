﻿using IdentityModel.Client;
using IdentityServer.Client1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace IdentityServer.Client1.Services
{
    public class ApiResourceHttpClient : IApiResourceHttpClient
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ApiResourceHttpClient(IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _contextAccessor = contextAccessor;
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        public async Task<HttpClient> GetHttpClient()
        {
            var accessToken = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            _httpClient.SetBearerToken(accessToken);

            return _httpClient;
        }

        public async Task<List<string>> RegisterUser(UserSaveViewModel userSaveViewModel)
        {
            var disco = await _httpClient.GetDiscoveryDocumentAsync(_configuration["AuthServerUrl"]);

            if(disco.IsError)
            {
                //logging
            }
            var clientCredentialsTokenRequest = new ClientCredentialsTokenRequest();

            clientCredentialsTokenRequest.ClientId = _configuration["ClientResourceOwner:ClientId"];
            clientCredentialsTokenRequest.ClientSecret = _configuration["ClientResourceOwner:ClientSecret"];
            clientCredentialsTokenRequest.Address = disco.TokenEndpoint;

            var token = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

            if(token.IsError)
            {
                // logging
            }

            var stringContent = new StringContent(JsonConvert.SerializeObject(userSaveViewModel), Encoding.UTF8,"application/json");

            _httpClient.SetBearerToken(token.AccessToken);

            var response = await _httpClient.PostAsync("https://localhost:5001/api/user/signup", stringContent);

            if(!response.IsSuccessStatusCode)
            {
                var errorList = JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());

                return errorList;
            }

            return null;
        }
    }
}
