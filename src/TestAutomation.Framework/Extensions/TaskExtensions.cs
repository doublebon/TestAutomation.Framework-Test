namespace TestAutomation.Framework.Extensions;

/// <summary>
/// Extension methods для async method chaining.
/// Позволяют писать await Page.Method1().Then(p => p.Method2()).Then(p => p.Method3())
/// </summary>
public static class TaskExtensions
{
    /// <summary>
    /// Then для chaining с трансформацией типа (LoginPage -> DashboardPage -> UserManagementPage)
    /// Это ОСНОВНОЙ метод для навигации между страницами
    /// </summary>
    // Async методы — навигация, действия Playwright
    public static async Task<TResult> Then<T, TResult>(
        this Task<T> task,
        Func<T, Task<TResult>> next)
    {
        var result = await task;
        return await next(result);
    }

    // Sync методы — чтение свойств, проверки, string, List и т.д.
    public static async Task<TResult> Then<T, TResult>(
        this Task<T> task,
        Func<T, TResult> next)
    {
        var result = await task;
        return next(result);
    }

}
