using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using TestAutomation.Framework.PageObjects.Base;

namespace TestAutomation.Framework.PageObjects.Pages;

public class LoginPage : BasePage
{
    // Locators - используем Playwright's recommended selectors
    private ILocator UsernameInput => Page.Locator("//*[@name=\"UserName\"]");
    private ILocator PasswordInput => Page.Locator("//*[@name=\"Password\"]");
    private ILocator LoginButton => Page.Locator("//*[@id=\"login\"]");
    private ILocator ErrorMessage => Page.Locator(".error-message, .alert-danger, [role='alert']");

    public LoginPage(IPage page, IBrowserContext context) : base(page, context)
    {
    }

    /// <summary>
    /// Fluent methods - каждый метод возвращает Task<LoginPage> для chaining
    /// </summary>
    public async Task<LoginPage> EnterUsernameAsync(string username)
    {
        await UsernameInput.FillAsync(username);
        return this;
    }

    public async Task<LoginPage> EnterPasswordAsync(string password)
    {
        await PasswordInput.FillAsync(password);
        return this;
    }

    /// <summary>
    /// Клик по кнопке логина и переход на DashboardPage
    /// </summary>
    public async Task<DashboardPage> ClickLoginAsync()
    {
        await LoginButton.ClickAsync();

        // Ждем навигации (примерная проверка)
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        return new DashboardPage(Page, Context);
    }

    /// <summary>
    /// Composite метод для полного login flow
    /// </summary>
    public async Task<DashboardPage> LoginAsync(string username, string password)
    {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        return new DashboardPage(Page, Context);
    }

    // Assertions
    public async Task AssertErrorMessageVisibleAsync()
    {
        await Expect(ErrorMessage).ToBeVisibleAsync();
    }

    public async Task AssertErrorMessageTextAsync(string expectedText)
    {
        await Expect(ErrorMessage).ToHaveTextAsync(expectedText, new LocatorAssertionsToHaveTextOptions
        {
            Timeout = 5000
        });
    }

    public async Task AssertLoginButtonDisabledAsync()
    {
        await Expect(LoginButton).ToBeDisabledAsync();
    }
}
