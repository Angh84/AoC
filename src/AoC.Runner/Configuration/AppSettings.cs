using Microsoft.Extensions.Configuration;

namespace AoC.Runner.Configuration;

public class AppSettings
{
    // AoC session token for downloading inputs
    public required string SessionToken { get; set; }
        
    // Base paths for different directories
    public required string InputsBasePath { get; set; }
        
    // Flags for runner behavior
    public bool RunTestsByDefault { get; set; }
    public bool AutoDownloadInputs { get; set; }
        
    // Default year to use when not specified
    public int DefaultYear { get; set; }
        
    // URL template for downloading inputs
    public required string InputUrlTemplate { get; set; }

    // Static method to load settings from appsettings.json
    public static AppSettings Load()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.local.json", optional: true); // For local overrides (git-ignored)

        IConfiguration config = builder.Build();
            
        var settings = new AppSettings
        {
            // Default values if not specified in config
            SessionToken = config["SessionToken"] ?? throw new InvalidOperationException(),
            InputsBasePath = config["InputsBasePath"] ?? "inputs",
            RunTestsByDefault = bool.Parse(config["RunTestsByDefault"] ?? "true"),
            AutoDownloadInputs = bool.Parse(config["AutoDownloadInputs"] ?? "true"),
            DefaultYear = int.Parse(config["DefaultYear"] ?? DateTime.Now.Year.ToString()),
            InputUrlTemplate = config["InputUrlTemplate"] ?? "https://adventofcode.com/{0}/day/{1}/input"
        };
            
        // Validate required settings
        if (settings.AutoDownloadInputs && string.IsNullOrEmpty(settings.SessionToken))
        {
            Console.WriteLine("Warning: SessionToken is not set, but AutoDownloadInputs is enabled.");
            Console.WriteLine("Input files will not be automatically downloaded.");
            Console.WriteLine("Please provide your session token in appsettings.json");
        }
            
        return settings;
    }
}