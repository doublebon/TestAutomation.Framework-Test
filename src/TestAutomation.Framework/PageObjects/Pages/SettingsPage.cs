using Microsoft.Playwright;
using TestAutomation.Framework.PageObjects.Base;

namespace TestAutomation.Framework.PageObjects.Pages;

/// <summary>
/// SettingsPage с generic родителем
/// </summary>
public class SettingsPage<TParent>(IPage page, IBrowserContext context, TParent? previousFragment = null)
    : BasePage(page, context), IBaseFragment<TParent, SettingsPage<TParent>> where TParent : BasePage
{
    private readonly ILocator SaveButton = page.Locator("button:has-text('Save')").Describe("Save Button");
    private readonly ILocator DashboardLink = page.Locator("a:has-text('Dashboard')");

    public TParent? PreviousFragment { get; } = previousFragment;

    /// <summary>
    /// Клик по кнопке логина и переход на DashboardPage
    /// </summary>
    public Task<DashboardPage<SettingsPage<TParent>>> GoToDashboard()
    {
        return Task.FromResult(new DashboardPage<SettingsPage<TParent>>(Page, Context, this));
    }
    
    public Task<SettingsPage<TParent>> HelloSettings()
    {
        Console.WriteLine("Hello Settings");
        return Task.FromResult(this);
    }
    
}
