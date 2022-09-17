using Autofac;
using Autofac.Extensions.DependencyInjection;
using contracts.Inventory;
using contracts.Weather;

namespace contracts.Sites;

public class SitesModule : AspNetCoreModule
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MockSites>().AsImplementedInterfaces().SingleInstance();
    }
}
