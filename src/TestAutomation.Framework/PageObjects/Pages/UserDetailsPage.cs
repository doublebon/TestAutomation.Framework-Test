using Microsoft.Playwright;
using TestAutomation.Framework.PageObjects.Base;

namespace TestAutomation.Framework.PageObjects.Pages;

public class UserDetailsPage : BasePage
{
    private ILocator UserName => Page.Locator(".user-name, h1, h2").First;
    private ILocator UserEmail => Page.Locator(".user-email, [data-testid='email']");
    private ILocator EditButton => Page.Locator("button:has-text('Edit')");
    private ILocator DeleteButton => Page.Locator("button:has-text('Delete')");
    private ILocator BackButton => Page.Locator("button:has-text('Back'), a:has-text('Back')");

    public UserDetailsPage(IPage page, IBrowserContext context) : base(page, context)
    {
    }

    public async Task<string> GetUserNameAsync()
    {
        return await UserName.TextContentAsync() ?? string.Empty;
    }

    public async Task<string> GetUserEmailAsync()
    {
        return await UserEmail.TextContentAsync() ?? string.Empty;
    }

    /// <summary>
    /// Возврат на список пользователей
    /// </summary>
    public async Task<UserManagementPage> GoBackAsync()
    {
        await BackButton.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return new UserManagementPage(Page, Context);
    }

    /// <summary>
    /// Удаление пользователя и возврат на список
    /// </summary>
    public async Task<UserManagementPage> DeleteUserAsync()
    {
        await DeleteButton.ClickAsync();

        // Обработка confirmation dialog если появится
        Page.Dialog += async (_, dialog) => await dialog.AcceptAsync();

        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return new UserManagementPage(Page, Context);
    }
}
