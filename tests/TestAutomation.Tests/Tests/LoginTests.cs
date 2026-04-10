using System.Text.RegularExpressions;
using Microsoft.Playwright;
using NUnit.Framework;
using TestAutomation.Framework;
using TestAutomation.Framework.Extensions;
using TestAutomation.Framework.PageObjects.Base;
using TestAutomation.Framework.PageObjects.Pages;
using static Microsoft.Playwright.Assertions;

namespace TestAutomation.Tests.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class LoginTests : BaseTest
{
    
    [Test]
    [Category("Login")]
    public async Task MyTest_Login22222()
    {
        var s = await new LoginPage(Page, Context)
            .ClickLoginAsync()
            .Then(page => page.GoToLoginPageAsync())
            .Then(async page => {
                await page.EnterPasswordAsync("1111");
                await page.EnterUsernameAsync("111");
                return page;
            })
            .Then(page => page.ReturnHelloAsync());
        Console.WriteLine(s);
    }

    [Test]
    [Category("Login")]
    public async Task MyTest_Login2222233()
    {
        await new LoginPage(Page, Context)
            .GoToSettings()
                .Then(page => page.GoToDashboard())
                .Then(page => page.HelloDashBoard())
                .Then(page => page.UnFocus())
                .Then(page => page.HelloSettings())
                .Then(page => page.GoToLoginPageAsync())
                .Then(page => page.HelloLogin())
                .Then(page => page.GoToSettings())
                .Then(page => page.HelloSettings());
    }

    [Test]
    [Category("Login")]
    public async Task MyTest_1111()
    {
        Console.WriteLine("Hello 1111");
    }
}
