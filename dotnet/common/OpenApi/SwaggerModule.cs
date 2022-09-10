using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace contracts.OpenApi;

internal class SwaggerModule : AspNetCoreModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Cloud Inventory API",
                    Description = "The Cloud Inventory Service",
                    License = new OpenApiLicense
                    {
                        Name = "Apache License 2.0", Url = new Uri("https://opensource.org/licenses/Apache-2.0")
                    }
                });
        });
    }

    public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            return;
        }

        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
