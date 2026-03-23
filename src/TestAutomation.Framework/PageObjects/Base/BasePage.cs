using Microsoft.Playwright;
using TestAutomation.Framework.PageObjects.Pages;
using static Microsoft.Playwright.Assertions;

namespace TestAutomation.Framework.PageObjects.Base;

/// <summary>
/// Базовый класс для всех Page Objects.
/// Использует только нативные возможности Playwright.
/// </summary>
public abstract class BasePage<TParent, TCurrent>(IPage page, IBrowserContext context, TParent? previousFragment = null)
    : IBaseFragment<TParent, TCurrent>
    where TParent : class
    where TCurrent : class
{
    /// <summary>
    /// IPage - публичное свойство для реализации IBaseFragment
    /// </summary>
    public IPage Page { get; } = page;

    /// <summary>
    /// IBrowserContext - остается protected или public по желанию
    /// </summary>
    protected IBrowserContext Context { get; } = context;

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

    public TParent? PreviousFragment => previousFragment;
    public abstract TCurrent CurrentFragment { get; }
}
