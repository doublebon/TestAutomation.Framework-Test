using Microsoft.Playwright;
using TestAutomation.Framework.PageObjects.Base;

namespace TestAutomation.Framework.PageObjects.Pages;

/// <summary>
/// SettingsPage с generic родителем
/// </summary>
public class SettingsPage<TParent>(IPage page, IBrowserContext context, TParent? previousFragment = null)
    : BasePage<TParent, SettingsPage<TParent>>(page, context, previousFragment)
    where TParent : class, IBaseFragment<object, object>
{
    // Используем Page из BasePage, а не параметр page
    private ILocator SaveButton => Page.Locator("button:has-text('Save')");
    private ILocator DashboardLink => Page.Locator("a:has-text('Dashboard')");

    public override SettingsPage<TParent> CurrentFragment => this;

    public async Task<SettingsPage<TParent>> Open()
    {
        await (await new LoginPage(Page, Context).Open()).GoToSettings();
        // Здесь можно добавить логику для открытия страницы, если это необходимо
        return this;
    }

    public async Task<SettingsPage<TParent>> Hello()
    {
        Console.WriteLine("Hello Settings");
        return this;
    }

}

/// <summary>
/// Non-generic alias для простоты
/// SettingsPage = SettingsPage<BasePage> (без родителя)
/// </summary>
public class SettingsPage(IPage page, IBrowserContext context) : SettingsPage<BasePage<object, object>>(page, context);
