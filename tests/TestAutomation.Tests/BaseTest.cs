using Microsoft.Playwright;
using NUnit.Framework;
using TestAutomation.Framework.Core;

namespace TestAutomation.Tests;

/// <summary>
/// Базовый класс для всех тестов.
/// Использует NUnit Parallelizable для параллельного выполнения.
/// Каждый тест получает изолированный BrowserContext.
/// </summary>
[TestFixture]
[Parallelizable(ParallelScope.Children)]
public abstract class BaseTest
{
    private PlaywrightFixture _fixture = null!;
    protected IBrowserContext Context = null!;
    protected IPage Page = null!;

    [OneTimeSetUp]
    public async Task OneTimeSetup()
    {
        _fixture = new PlaywrightFixture();
        await _fixture.InitializeAsync();
    }

    [SetUp]
    public async Task Setup()
    {
        // Новый BrowserContext для каждого теста = полная изоляция
        Context = await _fixture.CreateContextAsync();
        Page = await _fixture.CreatePageAsync(Context);

        // Навигация на базовый URL
        await Page.GotoAsync(TestConfiguration.BaseUrl);
    }

    [TearDown]
    public async Task TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            // Screenshot при падении
            var screenshotPath = $"screenshots/{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            Directory.CreateDirectory("screenshots");

            await Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = screenshotPath,
                FullPage = true
            });

            TestContext.AddTestAttachment(screenshotPath, "Screenshot on failure");
        }

        await Context.CloseAsync();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _fixture?.Dispose();
    }
}
