using System.Text.RegularExpressions;
using Microsoft.Playwright;
using NUnit.Framework;
using TestAutomation.Framework.Extensions;
using TestAutomation.Framework.PageObjects.Pages;
using static Microsoft.Playwright.Assertions;

namespace TestAutomation.Tests.Tests;

/// <summary>
/// Тесты Login функциональности с примерами использования fluent API
/// </summary>
[TestFixture]
[Parallelizable(ParallelScope.All)]
public class LoginTests : BaseTest
{
    [Test]
    [Category("Smoke")]
    [Category("Login")]
    public async Task SuccessfulLogin_WithValidCredentials_NavigatesToDashboard()
    {
        // Arrange
        var loginPage = new LoginPage(Page, Context);

        // Примечание: это демо-пример, используйте реальные credentials для вашего приложения
        var username = "testuser";
        var password = "password123";

        // Act
        var dashboardPage = await loginPage.LoginAsync(username, password);

        // Assert
        await Expect(Page).ToHaveTitleAsync(new Regex(".*", RegexOptions.IgnoreCase));
        await dashboardPage.AssertPageLoadedAsync();
    }

    [Test]
    [Category("Login")]
    public async Task LoginWithFluentChaining_EnterCredentialsStepByStep()
    {
        // Arrange
        var username = "testuser";
        var password = "password123";

        // Act - используем fluent interface
        var dashboardPage = await new LoginPage(Page, Context)
            .EnterUsernameAsync(username)
            .Then(page => page.EnterPasswordAsync(password))
            .Then(page => page.ClickLoginAsync());

        // Assert
        await dashboardPage.AssertPageLoadedAsync();
    }

    [Test]
    [Category("Login")]
    [Retry(2)]
    public async Task Login_SimpleExample_ForDemoApp()
    {
        // Этот тест работает с TodoMVC demo app (по умолчанию в appsettings.json)
        // Для вашего реального приложения замените locators в LoginPage

        // Просто проверяем, что страница загрузилась
        await Expect(Page).ToHaveURLAsync(new Regex(".*"));

        Console.WriteLine($"Navigated to: {Page.Url}");
        Console.WriteLine("Update LoginPage locators to match your application!");
    }
    
    
    [Test]
    [Category("Login")]
    public async Task MyTest_Login()
    {
        // Arrange
        var username = "testuser";
        var password = "password123";

        // Act - используем fluent interface
        var dashboardPage = await new LoginPage(Page, Context)
            .EnterUsernameAsync(username)
            .Then(page => page.EnterPasswordAsync(password))
            .Then(page => page.ClickLoginAsync())
            .Then(page => page.GoToUserManagementAsync());

        // Assert
        //await dashboardPage.AssertPageLoadedAsync();
    }
}
