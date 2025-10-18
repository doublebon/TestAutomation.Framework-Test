using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using TestAutomation.Framework.PageObjects.Base;

namespace TestAutomation.Framework.PageObjects.Pages;

/// <summary>
/// LoginPage с generic родителем
/// TParent - любая страница, откуда был открыт логин
/// </summary>
public class LoginPage<TParent> : BasePage, IBaseFragment<TParent, LoginPage<TParent>>
    where TParent : BasePage
{
    // Locators - используем Playwright's recommended selectors
    private ILocator UsernameInput => Page.Locator("//*[@name=\"UserName\"]");
    private ILocator PasswordInput => Page.Locator("//*[@name=\"Password\"]");
    private ILocator LoginButton => Page.Locator("//*[@id=\"login\"]");
    private ILocator ErrorMessage => Page.Locator(".error-message, .alert-danger, [role='alert']");

    public TParent? PreviousFragment { get; }

    /// <summary>
    /// Конструктор БЕЗ родителя (первая страница)
    /// </summary>
    public LoginPage(IPage page, IBrowserContext context) 
        : base(page, context)
    {
        PreviousFragment = null;
    }

    /// <summary>
    /// Конструктор С родителем (открыта из другой страницы)
    /// </summary>
    public LoginPage(IPage page, IBrowserContext context, TParent previousFragment) 
        : base(page, context)
    {
        PreviousFragment = previousFragment;
    }

    /// <summary>
    /// Fluent methods - каждый метод возвращает Task<LoginPage> для chaining
    /// </summary>
    public async Task<LoginPage<TParent>> EnterUsernameAsync(string username)
    {
        await UsernameInput.FillAsync(username);
        return this;
    }

    public async Task<LoginPage<TParent>> EnterPasswordAsync(string password)
    {
        await PasswordInput.FillAsync(password);
        return this;
    }

    /// <summary>
    /// Клик по кнопке логина и переход на DashboardPage
    /// </summary>
    public async Task<DashboardPage<TParent>> ClickLoginAsync()
    {
        await LoginButton.ClickAsync();

        // Ждем навигации (примерная проверка)
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        return new DashboardPage<TParent>(Page, Context);
    }

    /// <summary>
    /// Composite метод для полного login flow
    /// </summary>
    public async Task<DashboardPage<TParent>> LoginAsync(string username, string password)
    {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return new DashboardPage<TParent>(Page, Context);
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
    
    /// <summary>
    /// Клик по кнопке логина и переход на DashboardPage
    /// </summary>
    public async Task<DashboardPage<LoginPage<TParent>>> GoToDashboard()
    {
        return await Task.FromResult(new DashboardPage<LoginPage<TParent>>(Page, Context, this));
    }
    
    /// <summary>
    /// Клик по кнопке логина и переход на DashboardPage
    /// </summary>
    public async Task<SettingsPage<LoginPage<TParent>>> GoToSettings()
    {
        return await Task.FromResult(new SettingsPage<LoginPage<TParent>>(Page, Context, this));
    }

    public async Task<LoginPage<TParent>> HelloLogin()
    {
        Console.WriteLine("Hello Login");
        return await Task.FromResult(this);
    }
}

/// <summary>
/// Non-generic alias для простоты
/// LoginPage = LoginPage<BasePage> (без родителя)
/// </summary>
public class LoginPage : LoginPage<BasePage>
{
    public LoginPage(IPage page, IBrowserContext context) 
        : base(page, context)
    {
    }
}
