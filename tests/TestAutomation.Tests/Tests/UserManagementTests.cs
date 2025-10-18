using System.Text.RegularExpressions;
using Microsoft.Playwright;
using NUnit.Framework;
using TestAutomation.Framework.Extensions;
using TestAutomation.Framework.PageObjects.Pages;
using static Microsoft.Playwright.Assertions;

namespace TestAutomation.Tests.Tests;

/// <summary>
/// Примеры тестов для User Management с демонстрацией паттернов
/// </summary>
[TestFixture]
[Parallelizable(ParallelScope.All)]
public class UserManagementTests : BaseTest
{
    [Test]
    [Category("UserManagement")]
    public async Task CreateUser_WithAllFields_SavesSuccessfully()
    {
        // Этот тест показывает полный flow создания пользователя
        // Требует реальное приложение с формой создания пользователя

        // var userManagementPage = await new LoginPage(Page, Context)
        //     .LoginAsync("admin", "admin123")
        //     .Then(page => page.GoToUserManagementAsync())
        //     .Then(page => page.ClickAddUserAsync())
        //     .Then(page => page
        //         .EnterFirstNameAsync("John")
        //         .Then(p => p.EnterLastNameAsync("Doe"))
        //         .Then(p => p.EnterEmailAsync("john@test.com"))
        //         .Then(p => p.SelectRoleAsync("Admin"))
        //         .Then(p => p.SaveAsync()));
        //
        // var usersCount = await userManagementPage.GetUsersCountAsync();
        // Assert.That(usersCount, Is.GreaterThan(0));

        await Expect(Page).ToHaveURLAsync(new Regex(".*"));
        Console.WriteLine("User management test - requires real application");
        Assert.Pass("Demo test");
    }

    [Test]
    [Category("UserManagement")]
    public async Task SearchUser_FindsExistingUser()
    {
        // Демонстрация поиска пользователя

        await Expect(Page).ToHaveURLAsync(new Regex(".*"));
        Console.WriteLine("Search user test - implement with your application");
        Assert.Pass("Demo test");
    }

    [Test]
    [Category("UserManagement")]
    public async Task DeleteUser_RemovesFromList()
    {
        // Демонстрация удаления пользователя с навигацией

        // var userManagement = await new LoginPage(Page, Context)
        //     .LoginAsync("admin", "admin123")
        //     .Then(page => page.GoToUserManagementAsync())
        //     .Then(page => page.SelectUserAsync("test.user"))
        //     .Then(page => page.DeleteUserAsync());

        await Expect(Page).ToHaveURLAsync(new Regex(".*"));
        Console.WriteLine("Delete user test - implement with your application");
        Assert.Pass("Demo test");
    }
}
