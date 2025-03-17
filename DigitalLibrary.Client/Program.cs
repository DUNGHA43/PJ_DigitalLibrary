using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DigitalLibrary.Client;
using DigitalLibrary.Client.Services;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using MudBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = true;
});

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("https://localhost:7211/") });

builder.Services.AddScoped<AuthInterceptorHandler>();

builder.Services.AddHttpClient<UserServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<CategoriesServices>();
builder.Services.AddScoped<AuthorsServices>();
builder.Services.AddScoped<NationServices>();
builder.Services.AddScoped<SubjectsServices>();

await builder.Build().RunAsync();
