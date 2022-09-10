using Autofac.Extensions.DependencyInjection;
using contracts.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

// Add AutoFac: https://stackoverflow.com/questions/69754985/adding-autofac-to-net-core-6-0-using-the-new-single-file-template
builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureAppConfiguration((_, config) => config.AddEnvironmentVariables());

// 1. Register Autofac/AspNetModules
var modules = builder.UseModules()
    .With(new SettingsModule(builder.Configuration))
    .Scanning(new []{typeof(Program).Assembly, typeof(SettingsModule).Assembly})
    .Build();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

// 2. Use Autofac/AspNetModules
modules.Use(app);

app.MapFallbackToFile("index.html");

app.Run();
