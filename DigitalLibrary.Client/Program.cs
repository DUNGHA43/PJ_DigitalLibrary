using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DigitalLibrary.Client;
using DigitalLibrary.Client.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("https://localhost:7211/") });

builder.Services.AddScoped<AuthInterceptorHandler>();

builder.Services.AddHttpClient<UserServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddScoped<UserServices>();

await builder.Build().RunAsync();
