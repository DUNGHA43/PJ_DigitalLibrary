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

builder.Services.AddScoped<AuthServices>();
builder.Services.AddScoped<AuthInterceptorHandler>();

builder.Services.AddHttpClient<UserServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddHttpClient<CategoriesServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddHttpClient<AuthorsServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddHttpClient<NationServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddHttpClient<SubjectsServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddHttpClient<DocumentsServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddHttpClient<DocumentAuthorServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddHttpClient<DocumentCategoriesServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddHttpClient<DocumentSubjectsServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddHttpClient<UserManagementServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddHttpClient<RoleServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddHttpClient<UserPermissionServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddHttpClient<StatisticServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddHttpClient<UserSubscriptionServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddHttpClient<PaymentServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddHttpClient<TrafficLogServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

builder.Services.AddHttpClient<FavoListServices>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7211/");
}).AddHttpMessageHandler<AuthInterceptorHandler>();

await builder.Build().RunAsync();
