
using Microsoft.Playwright;
using NUnit.Framework;
using TestAutomation.Framework.Core;
[assembly: LevelOfParallelism(10)]

namespace TestAutomation.Tests;

/// <summary>
/// Global fixture - создает Browser ОДИН раз для всех тестов
/// </summary>
[SetUpFixture]
public class GlobalPlaywrightFixture
{
    public static IPlaywright Playwright { get; private set; } = null!;
    public static IBrowser Browser { get; private set; } = null!;

    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        
        // Выбор браузера из конфигурации
        var browserType = TestConfiguration.Browser.ToLower() switch
        {
            "firefox" => Playwright.Firefox,
            "webkit" => Playwright.Webkit,
            _ => Playwright.Chromium
        };

        // Запуск браузера с настройками
        Browser = await browserType.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = TestConfiguration.Headless,
            SlowMo = TestConfiguration.SlowMo,
            Timeout = TestConfiguration.LaunchTimeout,
            Args = new[]
            {
                "--disable-blink-features=AutomationControlled", // Скрыть automation флаг
    
                // 💾 ПАМЯТЬ И РЕСУРСЫ
                "--no-sandbox",
                "--disable-dev-shm-usage",
    
                // ⚡ ПРОИЗВОДИТЕЛЬНОСТЬ
                "--disable-extensions",
                "--disable-plugins",
                "--disable-background-timer-throttling",       // ⭐ КРИТИЧНО для параллели!
                "--disable-backgrounding-occluded-windows",
                "--disable-renderer-backgrounding",
                "--memory-pressure-off",
                "--disable-sync",
    
                // 🛡️ СТАБИЛЬНОСТЬ
                "--disable-breakpad",
                "--enable-automation"
            }
        });
    }

    [OneTimeTearDown]
    public async Task GlobalTeardown()
    {
        await Browser.CloseAsync();
        Playwright.Dispose();
    }
}

/// <summary>
/// Базовый класс для всех тестов.
/// Использует NUnit Parallelizable для параллельного выполнения.
/// Каждый тест получает изолированный BrowserContext.
/// </summary>
[TestFixture]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.All)]
public abstract class BaseTest
{
    protected IBrowserContext Context = null!;
    protected IPage Page = null!;

    [SetUp]
    public async Task Setup()
    {
        // Создаем директорию для видео
        var videoDir = Path.Combine(Directory.GetCurrentDirectory(), "testResults", "videos");
        Directory.CreateDirectory(videoDir);

        // Конфигурируем контекст с записью видео
        var contextOptions = new BrowserNewContextOptions
        {
            RecordVideoDir = TestConfiguration.RecordVideo ? videoDir : null
        };

        Context = await GlobalPlaywrightFixture.Browser.NewContextAsync(contextOptions);
        await Context.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });


        Page = await Context.NewPageAsync();
        await Page.GotoAsync(TestConfiguration.BaseUrl);
    }

    [TearDown]
    public async Task TearDown()
    {
        // Проверяем, упал ли тест
        if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            var tracePath = Path.Combine(Directory.GetCurrentDirectory(), "testResults", "traces", $"{TestContext.CurrentContext.Test.Name}.zip");

            // Сохраняем трейс только если тест упал!
            await Context.Tracing.StopAsync(new TracingStopOptions
            {
                Path = tracePath
            });

            TestContext.AddTestAttachment(tracePath, "Playwright Trace");
        }
        else
        {
            await Context.Tracing.StopAsync(new TracingStopOptions { Path = null }); // Не сохраняем для успешных
        }

        await Context.CloseAsync();
    }
}
