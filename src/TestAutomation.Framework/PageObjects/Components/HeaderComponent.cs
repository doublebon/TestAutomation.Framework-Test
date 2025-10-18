using Microsoft.Playwright;

namespace TestAutomation.Framework.PageObjects.Components;

/// <summary>
/// Переиспользуемый компонент Header
/// </summary>
public class HeaderComponent
{
    private readonly IPage _page;

    private ILocator UserMenu => _page.Locator("#user-menu, .user-menu, button:has-text('Profile')");
    private ILocator LogoutLink => _page.Locator("a:has-text('Logout'), button:has-text('Logout')");
    private ILocator ProfileLink => _page.Locator("a:has-text('Profile')");

    public HeaderComponent(IPage page)
    {
        _page = page;
    }

    public async Task LogoutAsync()
    {
        await UserMenu.ClickAsync();
        await LogoutLink.ClickAsync();
    }

    public async Task GoToProfileAsync()
    {
        await UserMenu.ClickAsync();
        await ProfileLink.ClickAsync();
    }

    public async Task<bool> IsUserLoggedInAsync()
    {
        return await UserMenu.IsVisibleAsync();
    }
}
