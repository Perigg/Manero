using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://cardprovider.azurewebsites.net/")
});

await builder.Build().RunAsync();