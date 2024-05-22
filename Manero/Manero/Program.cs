using Manero.Components;
using Manero.Registrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://cardprovider.azurewebsites.net/")
});

builder.Services.AddScoped(sp => new HttpClient
{
	BaseAddress = new Uri("https://addressprovider.azurewebsites.net/")
});

builder.Services.AddServiceRegistrations(builder.Configuration);
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

builder.Logging.AddConsole();


var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseWebAssemblyDebugging();
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Manero.Client._Imports).Assembly);

app.Run();