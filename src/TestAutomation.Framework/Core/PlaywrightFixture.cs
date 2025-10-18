using Microsoft.Playwright;

namespace TestAutomation.Framework.Core;

/// <summary>
/// Fixture для управления Playwright экземплярами на уровне тестового класса.
/// Обеспечивает thread-safety и изоляцию через BrowserContext.
/// </summary>
public class PlaywrightFixture : IDisposable
{
    private IPlaywright? _playwright;
    private IBrowser? _browser;

    // Каждый тест получает свой BrowserContext для полной изоляции
    private readonly List<IBrowserContext> _contexts = new();
    private readonly List<IPage> _pages = new();

    public async Task InitializeAsync()
    {
        // Playwright автоматически управляет драйверами
        _playwright = await Playwright.CreateAsync();

        // Выбор браузера из конфигурации
        var browserType = TestConfiguration.Browser.ToLower() switch
        {
            "firefox" => _playwright.Firefox,
            "webkit" => _playwright.Webkit,
            _ => _playwright.Chromium
        };

        // Запуск браузера с настройками
        _browser = await browserType.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = TestConfiguration.Headless,
            SlowMo = TestConfiguration.SlowMo,
            Timeout = TestConfiguration.LaunchTimeout
        });
    }

    /// <summary>
    /// Создание нового изолированного BrowserContext для теста.
    /// Context = инкогнито режим, полная изоляция cookies/storage.
    /// </summary>
    public async Task<IBrowserContext> CreateContextAsync()
    {
        if (_browser == null)
            throw new InvalidOperationException("Browser not initialized. Call InitializeAsync first.");

        var context = await _browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize 
            { 
                Width = TestConfiguration.ViewportWidth, 
                Height = TestConfiguration.ViewportHeight 
            },
            Locale = "ru-RU",
            TimezoneId = "Europe/Moscow",
            RecordVideoDir = TestConfiguration.RecordVideo ? "videos/" : null
        });

        _contexts.Add(context);
        return context;
    }

    /// <summary>
    /// Создание новой страницы в контексте
    /// </summary>
    public async Task<IPage> CreatePageAsync(IBrowserContext context)
    {
        var page = await context.NewPageAsync();

        // Встроенные timeouts Playwright
        page.SetDefaultTimeout(TestConfiguration.DefaultTimeout);
        page.SetDefaultNavigationTimeout(TestConfiguration.NavigationTimeout);

        _pages.Add(page);
        return page;
    }

    public void Dispose()
    {
        foreach (var page in _pages)
            page.CloseAsync().GetAwaiter().GetResult();

        foreach (var context in _contexts)
            context.CloseAsync().GetAwaiter().GetResult();

        _browser?.CloseAsync().GetAwaiter().GetResult();
        _playwright?.Dispose();
    }
}
