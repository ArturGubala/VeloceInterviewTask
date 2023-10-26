using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UserSpying.Client;
using MudBlazor.Services;
using UserSpying.Client.HttpRepository.Users;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("https://localhost:7299") });
builder.Services.AddMudServices();
builder.Services.AddScoped<IUsers, Users>();

await builder.Build().RunAsync();
