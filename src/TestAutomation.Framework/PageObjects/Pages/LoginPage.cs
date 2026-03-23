using Microsoft.Playwright;
using TestAutomation.Framework.Core;
using TestAutomation.Framework.PageObjects.Base;
using static Microsoft.Playwright.Assertions;

namespace TestAutomation.Framework.PageObjects.Pages;

/// <summary>
/// LoginPage с generic родителем
/// TParent - любая страница, откуда был открыт логин
/// </summary>
public class LoginPage<TParent>(IPage page, IBrowserContext context, TParent? previousFragment = null) 
    : BasePage<TParent, LoginPage<TParent>>(page, context, previousFragment)
    where TParent : class, IBaseFragment<object, object>
{
    // Locators - используем Playwright's recommended selectors
    private ILocator UsernameInput => Page.Locator("//*[@name=\"UserName\"]");
    private ILocator PasswordInput => Page.Locator("//*[@name=\"Password\"]");
    private ILocator LoginButton => Page.Locator("//*[@id=\"login\"]");

    public override LoginPage<TParent> CurrentFragment => this;

    public async Task<LoginPage<TParent>> Open()
    {
        // ✅ Если элементы видны, значит страница уже готова
        if (await UsernameInput.IsVisibleAsync())
        {
            return this;
        }

        // Здесь можно добавить логику для открытия страницы, если это необходимо
        await Page.GotoAsync(TestConfiguration.BaseUrl);
        return this;
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
    /// Composite метод для полного login flow
    /// </summary>
    public async Task<SettingsPage<LoginPage<TParent>>> LoginAsync(string username, string password)
    {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return await GoToSettings();
    }
    
  
    /// <summary>
    /// Клик по кнопке логина и переход на DashboardPage
    /// </summary>
    public async Task<SettingsPage<LoginPage<TParent>>> GoToSettings()
    {
        return new SettingsPage<LoginPage<TParent>>(Page, Context, this);
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
public class LoginPage(IPage page, IBrowserContext context) : LoginPage<BasePage<object, object>>(page, context);