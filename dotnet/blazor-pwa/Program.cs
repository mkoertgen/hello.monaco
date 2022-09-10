using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using blazor_pwa;
using contracts.Posts;
using contracts.Weather;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProvideWeather, JsonWeather>(sp => new JsonWeather(sp.GetService<HttpClient>()!, "sample-data/weather.json"));
builder.Services.AddScoped<IProvidePosts, JsonPosts>(sp => new JsonPosts(sp.GetService<HttpClient>()!, "sample-data/posts.json"));

await builder.Build().RunAsync();
