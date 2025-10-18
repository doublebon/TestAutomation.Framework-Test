using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace TestAutomation.Framework.PageObjects.Base;

/// <summary>
/// Базовый класс для всех Page Objects.
/// Использует только нативные возможности Playwright.
/// </summary>
public abstract class BasePage
{
    protected readonly IPage Page;
    protected readonly IBrowserContext Context;

    protected BasePage(IPage page, IBrowserContext context)
    {
        Page = page;
        Context = context;
    }

    /// <summary>
    /// Навигация с автоматическим ожиданием загрузки
    /// </summary>
    public async Task NavigateAsync(string url)
    {
        await Page.GotoAsync(url, new PageGotoOptions
        {
            WaitUntil = WaitUntilState.DOMContentLoaded
        });
    }

    /// <summary>
    /// Получение заголовка страницы
    /// </summary>
    public async Task<string> GetPageTitleAsync()
    {
        return await Page.TitleAsync();
    }

    /// <summary>
    /// Проверка текущего URL
    /// </summary>
    public bool IsCurrentUrl(string expectedUrl)
    {
        return Page.Url.Contains(expectedUrl);
    }
}
