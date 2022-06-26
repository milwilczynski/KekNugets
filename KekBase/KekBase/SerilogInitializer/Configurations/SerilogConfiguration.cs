namespace KekBase.SerilogInitializer.Configurations;

public class SerilogConfiguration
{
    public string LogPath { get; set; } = ".\\logs\\";
    public string MinimumLevel { get; set; } = "Information";
}