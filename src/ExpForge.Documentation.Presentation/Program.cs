using ExpForge.Documentation.Presentation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

var baseElement = builder.HostEnvironment.BaseAddress;
#if !DEBUG
baseElement = "https://celinhodaltro.github.io/ExpForge.Documentation/";
#endif
builder.Services.AddMudServices();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseElement) });



await builder.Build().RunAsync();