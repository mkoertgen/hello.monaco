using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace contracts.Weather;

public class WeatherModule : AspNetCoreModule
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MockWeather>().AsImplementedInterfaces().SingleInstance();
    }
}
