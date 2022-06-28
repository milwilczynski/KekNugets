using KekBase.BaseConfigurations;
using KekBase.RoutingExtensions.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KekBase.RoutingExtensions;

public static class RoutingInitalizer
{
    public static void RoutingInitializer(IServiceCollection services, IConfiguration configuration)
    {
        var config = new AppConfiguration();
        configuration.GetSection("AppConfiguration").Bind(config);

        services.AddControllers(options => options.Conventions.Add(
            new RoutingConvention(config)));
        services.AddRouting();
    }
}