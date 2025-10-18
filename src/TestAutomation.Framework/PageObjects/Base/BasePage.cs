using Microsoft.Playwright;
using TestAutomation.Framework.PageObjects.Pages;
using static Microsoft.Playwright.Assertions;

namespace TestAutomation.Framework.PageObjects.Base;

/// <summary>
/// Базовый класс для всех Page Objects.
/// Использует только нативные возможности Playwright.
/// </summary>
public abstract class BasePage
{
    /// <summary>
    /// IPage - публичное свойство для реализации IBaseFragment
    /// </summary>
    public IPage Page { get; }
    
    /// <summary>
    /// IBrowserContext - остается protected или public по желанию
    /// </summary>
    public IBrowserContext Context { get; }

    protected BasePage(IPage page, IBrowserContext context)
    {
        Page = page;
        Context = context;
    }

    /// <summary>
    /// Навигация с автоматическим ожиданием загрузки
    /// </summary>
    protected async Task NavigateAsync(string url)
    {
        await Page.GotoAsync(url, new PageGotoOptions
        {
            WaitUntil = WaitUntilState.DOMContentLoaded
        });
    }
    
    /// <summary>
    /// Проверка текущего URL
    /// </summary>
    public bool IsCurrentUrl(string expectedUrl)
    {
        return Page.Url.Contains(expectedUrl);
    }
    
    /// <summary>
    /// Клик по кнопке логина и переход на DashboardPage
    /// </summary>
    public Task<LoginPage> GoToLoginPageAsync()
    {
        return Task.FromResult(new LoginPage(Page, Context));
    }
}
