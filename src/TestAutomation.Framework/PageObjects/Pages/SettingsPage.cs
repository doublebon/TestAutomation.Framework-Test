using Microsoft.Playwright;
using TestAutomation.Framework.PageObjects.Base;

namespace TestAutomation.Framework.PageObjects.Pages;

public class SettingsPage : BasePage
{
    private ILocator SaveButton => Page.Locator("button:has-text('Save')");
    private ILocator DashboardLink => Page.Locator("a:has-text('Dashboard')");

    public SettingsPage(IPage page, IBrowserContext context) : base(page, context)
    {
    }

    public async Task<SettingsPage> UpdateSettingAsync(string key, string value)
    {
        await Page.Locator($"input[name='{key}']").FillAsync(value);
        return this;
    }

    public async Task<DashboardPage> SaveAndReturnToDashboardAsync()
    {
        await SaveButton.ClickAsync();
        await DashboardLink.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return new DashboardPage(Page, Context);
    }
}
