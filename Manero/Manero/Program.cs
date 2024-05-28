using Manero.Components;
using Manero.Registrations;
using YourBlazorApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddBlazorBootstrap();
builder.Services.AddScoped<ApiService>();
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