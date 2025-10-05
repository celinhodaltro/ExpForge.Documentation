using ExpForge.Documentation.Presentation;
using ExpForge.Documentation.Presentation.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using System;
using System.Net.Http;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

var baseElement = builder.HostEnvironment.BaseAddress;
#if !DEBUG
baseElement = "https://celinhodaltro.github.io/ExpForge.Documentation/";
#endif
builder.Services.AddMudServices();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseElement) });
builder.Services.AddScoped<ThemeService>();


await builder.Build().RunAsync();