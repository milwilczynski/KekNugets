using System;
using KekBase.SerilogInitializer.Configurations;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Enrichers.WithCaller;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace KekBase.SerilogInitializer.Initializer;

public static class SerilogInitializer
{
    private const string Template =
        "{level:u3}{Timestamp:HH:mm:ss.fff}[{ThreadId}][{Level}] {Message} (at {Caller}) {NewLine}{Exception}";


    public static void ConfigureSerilog(IConfiguration configuration, string appName)
    {
        var config = new SerilogConfiguration();
        configuration.GetSection("SerilogConfiguration").Bind(config);

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithCaller()
            .MinimumLevel.Is((LogEventLevel) Enum.Parse(typeof(LogEventLevel), config.MinimumLevel))
            .WriteTo.File($"{config.LogPath}\\{appName}\\{appName}-.txt",
                rollingInterval: RollingInterval.Day,
                outputTemplate: Template
            )
            .WriteTo.Console(theme: AnsiConsoleTheme.Literate,
                outputTemplate: Template)
            .CreateLogger();
        
            Log.Debug("SerilogConfiguration Loaded! [SerilogConfiguration: {@config}]",
                config);
    }
}