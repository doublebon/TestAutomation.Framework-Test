using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using TestAutomation.Framework.PageObjects.Base;
using TestAutomation.Framework.PageObjects.Components;

namespace TestAutomation.Framework.PageObjects.Pages;

/// <summary>
/// DashboardPage с generic родителем
/// TParent - ЛЮБАЯ страница (LoginPage, SettingsPage, UserManagementPage и т.д.)
/// </summary>
public class DashboardPage<TParent> : BasePage, IBaseFragment<TParent, DashboardPage<TParent>>
    where TParent : BasePage
{
    
    public TParent? PreviousFragment { get; }
    
    /// <summary>
    /// Конструктор БЕЗ родителя (первая страница или root)
    /// </summary>
    public DashboardPage(IPage page, IBrowserContext context) 
        : base(page, context)
    {
        PreviousFragment = null;
    }

    /// <summary>
    /// Конструктор С родителем
    /// TParent может быть LoginPage, SettingsPage, UserManagementPage - что угодно!
    /// </summary>
    public DashboardPage(IPage page, IBrowserContext context, TParent previousFragment) 
        : base(page, context)
    {
        PreviousFragment = previousFragment;
    }
    
    /// <summary>
    /// Клик по кнопке логина и переход на DashboardPage
    /// </summary>
    public Task<SettingsPage<DashboardPage<TParent>>> GoToSettings()
    {
        return Task.FromResult(new SettingsPage<DashboardPage<TParent>>(Page, Context, this));
    }
    
    public Task<DashboardPage<TParent>> HelloDashBoard()
    {
        Console.WriteLine("Hello DashBoard");
        return Task.FromResult(this);
    }
}
