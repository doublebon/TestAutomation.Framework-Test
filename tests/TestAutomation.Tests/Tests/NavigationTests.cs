using System.Text.RegularExpressions;
using Microsoft.Playwright;
using NUnit.Framework;
using TestAutomation.Framework.Extensions;
using TestAutomation.Framework.PageObjects.Pages;
using static Microsoft.Playwright.Assertions;

namespace TestAutomation.Tests.Tests;

/// <summary>
/// Демонстрация навигации между страницами с fluent chaining
/// </summary>
[TestFixture]
public class NavigationTests : BaseTest
{
    [Test]
    [Category("Navigation")]
    public async Task NavigateThroughPages_FluentChaining_Example()
    {
        // Этот тест демонстрирует синтаксис, но требует реального приложения

        // Пример того, как будет выглядеть навигация:
        // var userManagementPage = await new LoginPage(Page, Context)
        //     .LoginAsync("admin", "admin123")                    // DashboardPage
        //     .Then(page => page.GoToUserManagementAsync());      // UserManagementPage

        // Для демо просто проверяем URL
        await Expect(Page).ToHaveURLAsync(new Regex(".*"));

        Console.WriteLine("Navigation test - requires real application");
        Console.WriteLine("See the commented code for fluent navigation examples");

        Assert.Pass("Demo test - implement with your real application");
    }

    [Test]
    [Category("Navigation")]
    public async Task ComplexNavigationFlow_CreateAndManageUser_Example()
    {
        // Демонстрация сложного flow с множественной навигацией:

        var finalPage = await new LoginPage(Page, Context)
            .LoginAsync("admin", "admin123")                           // DashboardPage
            .Then(page => page.GoToUserManagementAsync())              // UserManagementPage
            .Then(page => page.ClickAddUserAsync())                    // CreateUserPage
            .Then(page => page.EnterFirstNameAsync("John")
                .Then(p => p.EnterLastNameAsync("Doe"))
                .Then(p => p.EnterEmailAsync("john@example.com"))
                .Then(p => p.SaveAsync()))                             // UserManagementPage
            .Then(page => page.SearchUserAsync("john@example.com"));

        await Expect(Page).ToHaveURLAsync(new Regex(".*"));

        Console.WriteLine("Complex navigation demo");
        Console.WriteLine("This shows how to chain multiple page transitions");

        Assert.Pass("Demo test - implement with your real application");
    }
}
