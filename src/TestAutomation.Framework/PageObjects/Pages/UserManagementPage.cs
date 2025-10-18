using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using TestAutomation.Framework.PageObjects.Base;
using TestAutomation.Framework.PageObjects.Components;

namespace TestAutomation.Framework.PageObjects.Pages;

public class UserManagementPage : BasePage
{
    private ILocator PageTitle => Page.Locator("h1:has-text('User'), h2:has-text('User')").First;
    private ILocator AddUserButton => Page.Locator("button:has-text('Add'), button:has-text('Create'), button:has-text('New')");
    private ILocator SearchInput => Page.Locator("input[type='search'], input[placeholder*='Search' i]");
    private ILocator UsersTable => Page.Locator("table").First;

    private HeaderComponent? _header;
    public HeaderComponent Header => _header ??= new HeaderComponent(Page);

    public UserManagementPage(IPage page, IBrowserContext context) : base(page, context)
    {
    }

    public async Task<UserManagementPage> SearchUserAsync(string searchTerm)
    {
        await SearchInput.FillAsync(searchTerm);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return this;
    }

    public async Task<UserManagementPage> WaitForPageLoadAsync()
    {
        await Expect(PageTitle).ToBeVisibleAsync();
        return this;
    }

    /// <summary>
    /// Навигация на страницу создания пользователя
    /// </summary>
    public async Task<CreateUserPage> ClickAddUserAsync()
    {
        await AddUserButton.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return new CreateUserPage(Page, Context);
    }

    /// <summary>
    /// Открытие деталей пользователя
    /// </summary>
    public async Task<UserDetailsPage> SelectUserAsync(string username)
    {
        var userRow = UsersTable.Locator($"tr:has-text('{username}')");
        await userRow.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return new UserDetailsPage(Page, Context);
    }

    public async Task<int> GetUsersCountAsync()
    {
        var rows = UsersTable.Locator("tbody tr");
        return await rows.CountAsync();
    }
}
