using System.Text.RegularExpressions;
using Microsoft.Playwright;
using NUnit.Framework;
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
    }
}
