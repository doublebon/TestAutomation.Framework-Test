using Microsoft.Playwright;

namespace TestAutomation.Framework.PageObjects.Base;

public static class BaseFragmentExtensions
{
    /// <summary>
    /// Получить текущий фрагмент (this)
    /// </summary>
    public static Task<TCurrent> GetCurrentFragment<TParent, TCurrent>(this IBaseFragment<TParent, TCurrent> fragment)
        where TParent : class
        where TCurrent : class
    {
        return Task.FromResult((TCurrent)fragment);
    }

    /// <summary>
    /// Получить предыдущий фрагмент
    /// </summary>
    public static Task<TParent> GetPreviousFragment<TParent, TCurrent>(this IBaseFragment<TParent, TCurrent> fragment)
        where TParent : class
        where TCurrent : class
    {
        if (fragment.PreviousFragment == null)
        {
            throw new InvalidOperationException(
                $"{fragment.GetType().Name} has no previous fragment. It's a root page.");
        }
        
        return Task.FromResult(fragment.PreviousFragment);
    }

    /// <summary>
    /// Alias для GetPreviousFragment - "снять фокус"
    /// </summary>
    public static Task<TParent> UnFocus<TParent, TCurrent>(this IBaseFragment<TParent, TCurrent> fragment)
        where TParent : class
        where TCurrent : class
    {
        return fragment.GetPreviousFragment();
    }

    /// <summary>
    /// Проверить наличие предыдущего фрагмента
    /// </summary>
    public static bool HasPrevious<TParent, TCurrent>(this IBaseFragment<TParent, TCurrent> fragment)
        where TParent : class
        where TCurrent : class
    {
        return fragment.PreviousFragment != null;
    }
    
    /// <summary>
    /// Assert существования фрагмента
    /// </summary>
    public static async Task<TCurrent> AssertFragmentExistAsync<TParent, TCurrent>(
        this IBaseFragment<TParent, TCurrent> fragment, 
        bool shouldExist = true, 
        float timeoutMs = 30000)
        where TParent : class
        where TCurrent : class
    {
        if (shouldExist)
        {
            await fragment.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new PageWaitForLoadStateOptions
            {
                Timeout = timeoutMs
            });
        }
        
        return await fragment.GetCurrentFragment();
    }

    /// <summary>
    /// Проверка существования фрагмента (без exception)
    /// </summary>
    public static async Task<bool> IsFragmentExistAsync<TParent, TCurrent>(
        this IBaseFragment<TParent, TCurrent> fragment, 
        int timeoutSeconds = 30)
        where TParent : class
        where TCurrent : class
    {
        try
        {
            await fragment.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded, new PageWaitForLoadStateOptions
            {
                Timeout = timeoutSeconds * 1000
            });
            return true;
        }
        catch
        {
            return false;
        }
    }

}