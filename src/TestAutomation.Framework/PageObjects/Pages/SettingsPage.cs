using Microsoft.Playwright;
using TestAutomation.Framework.PageObjects.Base;

namespace TestAutomation.Framework.PageObjects.Pages;

/// <summary>
/// SettingsPage с generic родителем
/// </summary>
public class SettingsPage<TParent> : BasePage, IBaseFragment<TParent, SettingsPage<TParent>>
    where TParent : BasePage
{
    private ILocator SaveButton => Page.Locator("button:has-text('Save')");
    private ILocator DashboardLink => Page.Locator("a:has-text('Dashboard')");

    public TParent? PreviousFragment { get; }

    public SettingsPage(IPage page, IBrowserContext context) 
        : base(page, context)
    {
        PreviousFragment = null;
    }

    public SettingsPage(IPage page, IBrowserContext context, TParent previousFragment) 
        : base(page, context)
    {
        PreviousFragment = previousFragment;
    }

    public async Task<SettingsPage<TParent>> UpdateSettingAsync(string key, string value)
    {
        await Page.Locator($"input[name='{key}']").FillAsync(value);
        return this;
    }
    
    public async Task<LoginPage<SettingsPage<TParent>>> GoToLoginPageAsync()
    {
        return await Task.FromResult(new LoginPage<SettingsPage<TParent>>(Page, Context, this));
    }
    
    /// <summary>
    /// Клик по кнопке логина и переход на DashboardPage
    /// </summary>
    public Task<DashboardPage<SettingsPage<TParent>>> GoToDashboard()
    {
        return Task.FromResult(new DashboardPage<SettingsPage<TParent>>(Page, Context, this));
    }
    
    public async Task<SettingsPage<TParent>> HelloSettings()
    {
        Console.WriteLine("Hello Settings");
        return await Task.FromResult(this);;
    }
    
}
