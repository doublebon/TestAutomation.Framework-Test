using Microsoft.Playwright;
using TestAutomation.Framework.PageObjects.Base;

namespace TestAutomation.Framework.PageObjects.Pages;

public class CreateUserPage : BasePage
{
    private ILocator FirstNameInput => Page.Locator("input[name*='first' i], #firstName");
    private ILocator LastNameInput => Page.Locator("input[name*='last' i], #lastName");
    private ILocator EmailInput => Page.Locator("input[type='email'], input[name*='email' i]");
    private ILocator RoleDropdown => Page.Locator("select[name*='role' i], #role");
    private ILocator SaveButton => Page.Locator("button[type='submit'], button:has-text('Save')");
    private ILocator CancelButton => Page.Locator("button:has-text('Cancel')");

    public CreateUserPage(IPage page, IBrowserContext context) : base(page, context)
    {
    }

    public async Task<CreateUserPage> EnterFirstNameAsync(string firstName)
    {
        await FirstNameInput.FillAsync(firstName);
        return this;
    }

    public async Task<CreateUserPage> EnterLastNameAsync(string lastName)
    {
        await LastNameInput.FillAsync(lastName);
        return this;
    }

    public async Task<CreateUserPage> EnterEmailAsync(string email)
    {
        await EmailInput.FillAsync(email);
        return this;
    }

    public async Task<CreateUserPage> SelectRoleAsync(string role)
    {
        await RoleDropdown.SelectOptionAsync(role);
        return this;
    }

    /// <summary>
    /// Сохранение и возврат на UserManagementPage
    /// </summary>
    public async Task<UserManagementPage> SaveAsync()
    {
        await SaveButton.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return new UserManagementPage(Page, Context);
    }

    /// <summary>
    /// Отмена и возврат на UserManagementPage
    /// </summary>
    public async Task<UserManagementPage> CancelAsync()
    {
        await CancelButton.ClickAsync();
        return new UserManagementPage(Page, Context);
    }
}
