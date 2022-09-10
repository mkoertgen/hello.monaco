using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace contracts.Posts;

public class PostsModule : AspNetCoreModule
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MockPosts>().AsImplementedInterfaces().SingleInstance();
    }
}
