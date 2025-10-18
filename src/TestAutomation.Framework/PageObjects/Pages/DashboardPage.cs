using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using TestAutomation.Framework.PageObjects.Base;
using TestAutomation.Framework.PageObjects.Components;

namespace TestAutomation.Framework.PageObjects.Pages;

public class DashboardPage : BasePage
{
    private ILocator PageHeading => Page.Locator("h1, h2").First;
    private ILocator WelcomeMessage => Page.Locator("text=/welcome/i").First;
    private ILocator UserManagementLink => Page.Locator("a:has-text('Users'), a:has-text('User Management')");
    private ILocator SettingsLink => Page.Locator("a:has-text('Settings')");
    private ILocator ReportsButton => Page.Locator("button:has-text('Reports')");

    // Header компонент
    private HeaderComponent? _header;
    public HeaderComponent Header => _header ??= new HeaderComponent(Page);

    public DashboardPage(IPage page, IBrowserContext context) : base(page, context)
    {
    }

    public async Task<DashboardPage> WaitForPageLoadAsync()
    {
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        return this;
    }

    public async Task<DashboardPage> ClickReportsAsync()
    {
        await ReportsButton.ClickAsync();
        return this;
    }

    /// <summary>
    /// Навигация на страницу User Management
    /// </summary>
    public async Task<UserManagementPage> GoToUserManagementAsync()
    {
        await UserManagementLink.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return new UserManagementPage(Page, Context);
    }

    /// <summary>
    /// Навигация на страницу Settings
    /// </summary>
    public async Task<SettingsPage> GoToSettingsAsync()
    {
        await SettingsLink.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return new SettingsPage(Page, Context);
    }
    
    /// <summary>
    /// Навигация на страницу Settings
    /// </summary>
    public async Task<SettingsPage> GoToOtherPageAsync()
    {
        await NavigateAsync("http://www.uitestingplayground.com/dynamicid");
        return new SettingsPage(Page, Context);
    }

    public async Task<string> GetWelcomeMessageAsync()
    {
        return await WelcomeMessage.TextContentAsync() ?? string.Empty;
    }

    public async Task AssertPageLoadedAsync()
    {
        await Expect(PageHeading).ToBeVisibleAsync();
    }
}
