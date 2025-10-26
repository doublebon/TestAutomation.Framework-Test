using Microsoft.Playwright;
using NUnit.Framework;
using TestAutomation.Framework.PageObjects.Base;
using static Microsoft.Playwright.Assertions;

namespace TestAutomation.Framework.PageObjects.Pages;

public class Html5FormPage<TParent>(IPage page, IBrowserContext context, TParent? previousFragment = null)
    : BasePage(page, context), IBaseFragment<TParent, Html5FormPage<TParent>>
    where TParent : BasePage
{
    public TParent? PreviousFragment { get; } = previousFragment;
    // ========== LOCATORS С .Describe() ==========
    
    /// <summary>
    /// .Describe() добавляет описание в trace viewer и reports
    /// Видно в Playwright Inspector и при отладке
    /// </summary>
    private ILocator FormLocator => Page.Locator("form")
        .Describe("Main Form");
    
    private ILocator DateInputLocator => Page.Locator("//input[@type='date']")
        .Describe("Date Input");
    
    private ILocator LocalDateTimeInputLocator => Page.Locator("//input[@id='date-time-picker']")
        .Describe("Local Date Time Input");
    
    private ILocator EmailInputLocator => Page.Locator("//input[@type='email']")
        .Describe("Email Input");

    private ILocator MonthLocator => Page.Locator("//input[@id='month-field']")
        .Describe("Month Input Field");
    
    private ILocator NumberLocator => Page.Locator("//input[@id='number-field']")
        .Describe("Number Input");
    
    private ILocator SubmitButtonLocator => Page.Locator("//input[@type='submit']")
        .Describe("Submit Button");

    private ILocator ResetButtonLocator => Page.Locator("//input[@type='reset']")
        .Describe("Reset Button");

    // ========== HELPER МЕТОДЫ ==========

    /// <summary>
    /// Проверить существование элемента
    /// .NET 8: ValueTask для оптимизации
    /// </summary>
    private async ValueTask<bool> ElementExistsAsync(ILocator locator)
    {
        return await locator.CountAsync() > 0;
    }

    /// <summary>
    /// Проверить видимость элемента
    /// </summary>
    private async ValueTask<bool> ElementIsVisibleAsync(ILocator locator)
    {
        if (await ElementExistsAsync(locator) is false)
            return false;

        return await locator.IsVisibleAsync();
    }

    /// <summary>
    /// Выполнить действие если элемент доступен
    /// </summary>
    private async Task<Html5FormPage<TParent>> IfAvailableAsync(
        ILocator locator,
        Func<ILocator, Task> action)
    {
        await action(locator);
        return this;
    }
    
    // ========== FILL ACTIONS ==========
    public Task<Html5FormPage<TParent>> FillDateAsync(string value)
        => IfAvailableAsync(DateInputLocator, l => l.FillAsync(value));
    
    public Task<Html5FormPage<TParent>> FillLocalDateTimeAsync(string value)
        => IfAvailableAsync(LocalDateTimeInputLocator, l => l.FillAsync(value));

    public Task<Html5FormPage<TParent>> FillEmailAsync(string value)
        => IfAvailableAsync(EmailInputLocator, l => l.FillAsync(value));
    
    public Task<Html5FormPage<TParent>> FillMonthAsync(string value)
        => IfAvailableAsync(MonthLocator, l => l.FillAsync(value));
    
    public Task<Html5FormPage<TParent>> FillNumberAsync(string value)
        => IfAvailableAsync(NumberLocator, l => l.FillAsync(value));
    
    public async Task<Html5FormResultPage<Html5FormPage<TParent>>> SubmitAsync()
    {
        await IfAvailableAsync(SubmitButtonLocator, async l =>
        {
            await l.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.Load);
        });
        return new Html5FormResultPage<Html5FormPage<TParent>>(Page, Context, this);
    }

    public Task<Html5FormPage<TParent>> ResetAsync()
        => IfAvailableAsync(ResetButtonLocator, l => l.ClickAsync());
    
    // ========== WAITS ==========

    public async Task<Html5FormPage<TParent>> WaitForFormLoadedAsync(float timeoutMs = 15000)
    {
        await FormLocator.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = timeoutMs
        });
        return this;
    }
}

public class Html5FormPage(IPage page, IBrowserContext context) : Html5FormPage<BasePage>(page, context);