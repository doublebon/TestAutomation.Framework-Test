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
        Assert.Fail("bad test");
    }

    [Test]
    [Category("Login")]
    //[Repeat(15)]  // ← Повторить 15 раз
    public async Task MyTest_Login2222233([Range(1, 10)] int i)
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
}
