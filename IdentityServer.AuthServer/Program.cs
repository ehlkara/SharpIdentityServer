using System.Reflection;
using IdentityServer.AuthServer;
using IdentityServer.AuthServer.Models;
using IdentityServer.AuthServer.Repository;
using IdentityServer.AuthServer.Seeds;
using IdentityServer.AuthServer.Services;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICustomUserRepository, CustomUserRepository>();

builder.Services.AddDbContext<CustomDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb"));
});

var assemblyName = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

builder.Services.AddIdentityServer()
    .AddConfigurationStore(opts =>
    {
        opts.ConfigureDbContext = c => c.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb"), sqlopts => sqlopts.MigrationsAssembly(assemblyName));
    })
    .AddOperationalStore(opts =>
    {
        opts.ConfigureDbContext = c => c.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb"), sqlopts => sqlopts.MigrationsAssembly(assemblyName));
    })
    .AddInMemoryApiResources(Config.GetApiResources())
    .AddInMemoryApiScopes(Config.GetApiScopes())
    .AddInMemoryClients(Config.GetClients())
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    //      .AddTestUsers(Config.GetUsers().ToList())
    .AddDeveloperSigningCredential()
    .AddProfileService<CustomProfileService>()
    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();



builder.Services.AddAuthentication()
    .AddGoogle("Google", options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

        options.ClientId = "343659810142-gkdjq66118r6696sshij2k0kr7cau9m2.apps.googleusercontent.com";
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
        //options.ClientSecret = GenerateAppleClientSecret(); // Burada, elde etti�iniz private key'i kullanarak bir client secret olu�turmal�s�n�z
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

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var context = services.GetRequiredService<ConfigurationDbContext>();

    await IdentityServerSeedData.Seed(context);
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
