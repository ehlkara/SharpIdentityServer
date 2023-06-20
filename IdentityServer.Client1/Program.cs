using IdentityServer4;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies").AddGoogle("Google", options =>
{
    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

    options.ClientId = "727823634857-qqk9poohpoihrebuhbk1js76hp09cc82.apps.googleusercontent.com";
    options.ClientSecret = "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";
    options.Scope.Add("CountryAndCity");
    options.Scope.Add("offline_access");
    options.ClaimActions.MapUniqueJsonKey("country", "country");
    options.ClaimActions.MapUniqueJsonKey("city", "city");
}).AddFacebook("Facebook", options =>
{
    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

    options.ClientId = "217164231260044";
    options.ClientSecret = "f1a459e448da15cd954fcad271faa1f5";
    options.CallbackPath = "/signin-facebook";
    options.SaveTokens = true;
}).AddOpenIdConnect("oidc", opts =>
{
    opts.SignInScheme = "Cookies";
    opts.Authority = "https://localhost:7183";
    opts.ClientId = "Client1-Mvc";
    opts.ClientSecret = "secret";
    opts.ResponseType = "code id_token";
    opts.GetClaimsFromUserInfoEndpoint = true;
    opts.SaveTokens = true;
    opts.Scope.Add("api1.read");
    opts.Scope.Add("offline_access");
    opts.Scope.Add("CountryAndCity");
    opts.Scope.Add("Roles");
    opts.ClaimActions.MapUniqueJsonKey("country", "country");
    opts.ClaimActions.MapUniqueJsonKey("city", "city");
    opts.ClaimActions.MapUniqueJsonKey("role", "role");

    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        RoleClaimType = "role",
    };
});


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
