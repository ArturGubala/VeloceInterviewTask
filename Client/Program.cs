using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UserSpying.Client;
using MudBlazor.Services;
using UserSpying.Client.HttpRepository.Users;
using UserSpying.Client.HttpRepository.Genders;
using UserSpying.Client.HttpRepository.CustomFields;
using MudBlazor;
using UserSpying.Client.HttpRepository.Report;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("https://localhost:7299") });
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;

    config.SnackbarConfiguration.PreventDuplicates = true;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 3000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
});
builder.Services.AddScoped<IUsersHttpRepository, UsersHttpRepository>();
builder.Services.AddScoped<IGenderHttpRepository, GenderHttpRepository>();
builder.Services.AddScoped<ICustomFieldHttpRepository, CustomFieldHttpRepository>();
builder.Services.AddScoped<IReportHttpRepository, ReportHttpRepository>();

await builder.Build().RunAsync();
