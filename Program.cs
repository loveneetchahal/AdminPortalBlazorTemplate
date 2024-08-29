using AdminPortalBlazorTemplate;
using AdminPortalBlazorTemplate.Helpers;
using AdminPortalBlazorTemplate.Services.AuthService;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
 
builder.Services.AddScoped<IAuthService, AuthService>(); 

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore(config =>
{
    config.AddPolicy(Policies.IsSuperAdmin, Policies.IsSuperAdminPolicy());
    config.AddPolicy(Policies.IsAdmin, Policies.IsAdminPolicy());
    config.AddPolicy(Policies.IsUser, Policies.IsUserPolicy());
});

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

await builder.Build().RunAsync();
