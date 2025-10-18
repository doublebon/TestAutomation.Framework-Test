using Microsoft.Extensions.Configuration;

namespace TestAutomation.Framework.Core;

public static class TestConfiguration
{
    private static readonly IConfiguration Configuration;

    static TestConfiguration()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("TEST_ENV") ?? "Development"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
    }

    // Application settings
    public static string BaseUrl => Configuration["TestSettings:BaseUrl"] ?? "https://example.com";

    // Browser settings
    public static string Browser => Configuration["TestSettings:Browser"] ?? "chromium";
    public static bool Headless => bool.Parse(Configuration["TestSettings:Headless"] ?? "true");
    public static int SlowMo => int.Parse(Configuration["TestSettings:SlowMo"] ?? "0");

    // Timeouts (в миллисекундах)
    public static float DefaultTimeout => float.Parse(Configuration["TestSettings:DefaultTimeout"] ?? "30000");
    public static float NavigationTimeout => float.Parse(Configuration["TestSettings:NavigationTimeout"] ?? "30000");
    public static float LaunchTimeout => float.Parse(Configuration["TestSettings:LaunchTimeout"] ?? "60000");

    // Viewport
    public static int ViewportWidth => int.Parse(Configuration["TestSettings:ViewportWidth"] ?? "1920");
    public static int ViewportHeight => int.Parse(Configuration["TestSettings:ViewportHeight"] ?? "1080");

    // Recording
    public static bool RecordVideo => bool.Parse(Configuration["TestSettings:RecordVideo"] ?? "false");
}
