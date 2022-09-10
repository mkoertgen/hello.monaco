using Autofac;
using Microsoft.Extensions.Configuration;

namespace contracts.Settings;

public class SettingsModule : Module
{
    public SettingsModule(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public IConfiguration Configuration { get; }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterInstance(Configuration);
    }
}
