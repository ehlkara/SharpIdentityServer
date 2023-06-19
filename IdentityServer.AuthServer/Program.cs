using IdentityServer.AuthServer;
using IdentityServer4;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentityServer().AddInMemoryApiResources(Config.GetApiResource())
    .AddInMemoryApiScopes(Config.GetApiScopes())
    .AddInMemoryClients(Config.GetClients())
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddTestUsers(Config.GetUsers().ToList())
    .AddDeveloperSigningCredential();

builder.Services.AddAuthentication()
    .AddGoogle("Google", options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

        options.ClientId = "727823634857-qqk9poohpoihrebuhbk1js76hp09cc82.apps.googleusercontent.com";
        options.ClientSecret = "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";
    });

builder.Services.AddAuthentication()
    .AddFacebook("Facebook", options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

        options.ClientId = "217164231260044";
        options.ClientSecret = "f1a459e448da15cd954fcad271faa1f5";
    });

builder.Services.AddAuthentication().AddOpenIdConnect("Apple", "Sign in with Apple", options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
        options.Authority = "https://appleid.apple.com";
        options.ClientId = "<YOUR SERVICE ID FROM APPLE>";
        //options.ClientSecret = GenerateAppleClientSecret(); // Burada, elde ettiðiniz private key'i kullanarak bir client secret oluþturmalýsýnýz
        options.CallbackPath = "/signin-apple";
        options.ResponseType = "code";
        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.Scope.Add("name");
        options.Scope.Add("email");
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
app.UseIdentityServer();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
