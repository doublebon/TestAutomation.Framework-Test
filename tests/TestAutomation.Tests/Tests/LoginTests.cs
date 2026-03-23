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
        var s = await new LoginPage(Page, Context).Open()
            .Then(async page =>
            {
                await page.EnterPasswordAsync("1111");
                await page.EnterUsernameAsync("111");
                return page;
            })
            .Then(page => page.GoToSettings())
            .Then(page => page.Hello())
            .Then(page => page.Hello())
            .Then(page => page.UnFocus())
            .Then(page => page.HelloLogin())
            .Then(page => page.GoToSettings())
            .Then(page => page.Hello());
        Console.WriteLine(s);
    }

    [Test]
    [Category("Login")]
    public async Task MyTest_Login3333()
    {
        await new SettingsPage(Page, Context).Open()
            .Then(page => page.Hello());
    }
}