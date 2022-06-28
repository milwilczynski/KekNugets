using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static KekBase.SerilogInitializer.Initializer.SerilogInitializer;
using static KekBase.RoutingExtensions.RoutingInitalizer;

namespace KekBase.Initializer;

public static class BaseInitializer
{
    private static IConfiguration? _configuration;
    private static string _appName = string.Empty;

    public static void ConfigureKekServices(this IServiceCollection services, IConfiguration configuration)
    {
        _configuration = configuration;
        if (Assembly.GetEntryAssembly()?.GetName().Name is not null)
        {
            _appName = Assembly.GetEntryAssembly()?.GetName().Name!;
        }

        ConfigureSerilog(_configuration, _appName);
        RoutingInitializer(services, configuration);
    }

    public static void ConfigureKekApp(this IApplicationBuilder app, ILoggerFactory loggery)
    {
        app.UseRouting();
        app.UseAuthorization();
        app.UseCors(p => { p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
        app.UseAuthentication();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}