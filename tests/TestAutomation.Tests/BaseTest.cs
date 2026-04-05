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
    private static IPlaywright Playwright { get; set; } = null!;
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
            Args =
            [
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
            ]
        });


        foreach (var dir in new[] { "testResults/videos", "testResults/traces", "testResults/screenshots" })
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), dir));
        }
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
    protected IBrowserContext Context { get; private set; } = null!;
    protected IPage Page { get; private set; } = null!;

    [SetUp]
    public async Task Setup()
    {
        //// Создаем директорию для видео
        //var videoDir = Path.Combine(Directory.GetCurrentDirectory(), "testResults", "videos");
        //Directory.CreateDirectory(videoDir);

        //// Конфигурируем контекст с записью видео
        //var contextOptions = new BrowserNewContextOptions
        //{
        //    RecordVideoDir = TestConfiguration.RecordVideo ? videoDir : null
        //};
        //Context = await GlobalPlaywrightFixture.Browser.NewContextAsync(contextOptions);

        Context = await GlobalPlaywrightFixture.Browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize
            {
                Width = TestConfiguration.ViewportWidth,
                Height = TestConfiguration.ViewportHeight
            }
        });

        await Context.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });


        Page = await Context.NewPageAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        var testName = TestContext.CurrentContext.Test.Name;
        var isFailed = TestContext.CurrentContext.Result.Outcome.Status ==
                        NUnit.Framework.Interfaces.TestStatus.Failed;

        var outputDir = Directory.GetCurrentDirectory();
        var tracePath = Path.Combine(outputDir, "testResults/traces", $"{testName}.zip");
        var screenshotPath = Path.Combine(outputDir, "testResults/screenshots", $"{testName}.png");
      
        try
        {
            // Сохраняем трейс ТОЛЬКО если тест упал
            await Context.Tracing.StopAsync(new TracingStopOptions
            {
                Path = isFailed ? tracePath : null
            });

            if (isFailed)
            {
                await Page.ScreenshotAsync(new PageScreenshotOptions
                {
                    Path = screenshotPath,
                    FullPage = true
                });

                TestContext.AddTestAttachment(screenshotPath, "Screenshot");
                TestContext.AddTestAttachment(tracePath, "Playwright Trace");
            }
        }
        catch (Exception ex)
        {
            TestContext.WriteLine($"Error saving artifacts: {ex.Message}");
        }
        finally
        {
            await Page.CloseAsync();
            await Context.CloseAsync();
        }
    }
}
