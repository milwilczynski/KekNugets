using System.Reflection;
using static KekBase.SerilogInitializer.Initializer.SerilogInitializer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KekBase.Initializer;

public static class BaseInitializer
{
    private static IConfiguration? _configuration;
    private static string _appName = string.Empty;

    public static void PrepareApplication(this IServiceCollection services, IConfiguration configuration)
    {
        _configuration = configuration;
        if (Assembly.GetEntryAssembly()?.GetName().Name is not null)
            _appName = Assembly.GetEntryAssembly()?.GetName().Name!;
        
        ConfigureSerilog(_configuration, _appName);
    }
}