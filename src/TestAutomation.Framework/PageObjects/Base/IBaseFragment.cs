using Microsoft.Playwright;
using TestAutomation.Framework.PageObjects.Pages;

namespace TestAutomation.Framework.PageObjects.Base;

public interface IBaseFragment<out TParent, out TCurrent>
    where TParent : class
    where TCurrent : class
{
    /// <summary>
    /// Предыдущий фрагмент
    /// </summary>
    TParent? PreviousFragment { get; }
    TCurrent CurrentFragment { get; }

    /// <summary>
    /// IPage для доступа к Playwright
    /// </summary>
    IPage Page { get; }
}