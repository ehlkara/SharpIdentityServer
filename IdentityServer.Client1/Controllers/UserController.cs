using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace IdentityServer.Client1.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IConfiguration _config;

        public UserController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task LogOut()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        public async Task<IActionResult> GetRefreshToken()
        {
            HttpClient httpClient = new HttpClient();

            var disco = await httpClient.GetDiscoveryDocumentAsync("https://localhost:7183");

            if (disco.IsError)
            {
                // logging
            }

            var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            RefreshTokenRequest refreshTokenRequest = new RefreshTokenRequest();

            refreshTokenRequest.ClientId = _config["Client1Mvc:ClientId"];
            refreshTokenRequest.ClientSecret = _config["Client1Mvc:ClientSecret"];
            refreshTokenRequest.RefreshToken = refreshToken;
            refreshTokenRequest.Address = disco.TokenEndpoint;

            var token = await httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

            if (token.IsError)
            {
                //must be route
            }

            var tokens = new List<AuthenticationToken>()
            {
                new AuthenticationToken {Name = OpenIdConnectParameterNames.IdToken, Value = token.IdentityToken},
                new AuthenticationToken {Name = OpenIdConnectParameterNames.AccessToken, Value = token.AccessToken},
                new AuthenticationToken {Name = OpenIdConnectParameterNames.RefreshToken, Value = token.RefreshToken},
                new AuthenticationToken {Name = OpenIdConnectParameterNames.ExpiresIn, Value = 
                DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString("O",CultureInfo.InvariantCulture)}
            };

            var authenticationResult = await HttpContext.AuthenticateAsync();

            var properties = authenticationResult.Properties;

            properties.StoreTokens(tokens);

            await HttpContext.SignInAsync("Cookies", authenticationResult.Principal, properties);

            return RedirectToAction("Index");
        }

        [Authorize(Roles ="admin")]
        public IActionResult AdminAction()
        {
            return View();
        }
        [Authorize(Roles = "admin,customer")]
        public IActionResult CustomerAction()
        {
            return View();
        }
    }
}