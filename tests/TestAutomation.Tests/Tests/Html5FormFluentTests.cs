using NUnit.Framework;
using TestAutomation.Framework.Extensions;
using TestAutomation.Framework.PageObjects.Base;
using TestAutomation.Framework.PageObjects.Pages;

namespace TestAutomation.Tests.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class Html5FormFluentTests : BaseTest
{
    private const string BaseUrl = "https://www.youtube.com/watch?v=0aOBefSBNqk";

    // [SetUp]
    // public async Task NavigateToForm()
    // {
    //     await Page.GotoAsync(BaseUrl);
    // }

    [Test]
    public async Task Test1()
    {
        await Page.GotoAsync("https://www.youtube.com/watch?v=0aOBefSBNqk");
        await Task.Delay(10000);
    }

    [Test]
    public async Task Test2()
    {
        await Page.GotoAsync("https://www.youtube.com/watch?v=0aOBefSBNqk");
        await Task.Delay(10000);
    }
        
    
    
    [Test] public async Task Test3() {
        await Page.GotoAsync("https://www.youtube.com/watch?v=0aOBefSBNqk");
        await Task.Delay(10000);
    }
    
    [Test] public async Task Test4() {
        await Page.GotoAsync("https://www.youtube.com/watch?v=0aOBefSBNqk");
        await Task.Delay(10000);
    }
    
    [Test] public async Task Test6() {
        await Page.GotoAsync("https://www.youtube.com/watch?v=0aOBefSBNqk");
        await Task.Delay(10000);
    }

    [Test] public async Task Test7() {
        await Page.GotoAsync("https://www.youtube.com/watch?v=0aOBefSBNqk");
        await Task.Delay(10000);
    }

    [Test] public async Task Test8() {
        await Page.GotoAsync("https://www.youtube.com/watch?v=0aOBefSBNqk");
        await Task.Delay(10000);
    }

    [Test] public async Task Test9() {
        await Page.GotoAsync("https://www.youtube.com/watch?v=0aOBefSBNqk");
        await Task.Delay(10000);
    }
    
    [Test] public async Task Test10() {
        await Page.GotoAsync("https://www.youtube.com/watch?v=0aOBefSBNqk");
        await Task.Delay(10000);
    }

    
    
    // [Test]
    // public async Task Test01_CompleteChain()
    // {
    //     await new Html5FormPage(Page, Context)
    //         .WaitForFormLoadedAsync()
    //         .Then(p => p.FillDateAsync(DateTime.Today.ToString("yyyy-MM-dd")))
    //         .Then(p => p.FillLocalDateTimeAsync(DateTime.Today.ToString("yyyy-MM-ddTHH:mm")))
    //         .Then(p => p.FillEmailAsync("someEmail@mail.ru"))
    //         .Then(p => p.FillMonthAsync(DateTime.Today.ToString("yyyy-MM")))
    //         .Then(p => p.FillNumberAsync("88888"))
    //         .Then(p => p.SubmitAsync())
    //         .Then(p => p.ClickBackAsync())
    //         .Then(p => p.FillNumberAsync("11111"))
    //         .Then(p => p.SubmitAsync());
    // }
    //
    // [Test]
    // public async Task Test02_CompleteChain()
    // {
    //     await new Html5FormPage(Page, Context)
    //         .WaitForFormLoadedAsync()
    //         .Then(p => p.FillDateAsync(DateTime.Today.ToString("yyyy-MM-dd")))
    //         .Then(p => p.FillLocalDateTimeAsync(DateTime.Today.ToString("yyyy-MM-ddTHH:mm")))
    //         .Then(p => p.FillEmailAsync("someEmail@mail.ru"))
    //         .Then(p => p.FillMonthAsync(DateTime.Today.ToString("yyyy-MM")))
    //         .Then(p => p.FillNumberAsync("88888"))
    //         .Then(p => p.SubmitAsync())
    //         .Then(p => p.ClickBackAsync())
    //         .Then(p => p.FillNumberAsync("11111"))
    //         .Then(p => p.SubmitAsync());
    // }
    //
    // [Test]
    // public async Task Test03_CompleteChain()
    // {
    //     await new Html5FormPage(Page, Context)
    //         .WaitForFormLoadedAsync()
    //         .Then(p => p.FillDateAsync(DateTime.Today.ToString("yyyy-MM-dd")))
    //         .Then(p => p.FillLocalDateTimeAsync(DateTime.Today.ToString("yyyy-MM-ddTHH:mm")))
    //         .Then(p => p.FillEmailAsync("someEmail@mail.ru"))
    //         .Then(p => p.FillMonthAsync(DateTime.Today.ToString("yyyy-MM")))
    //         .Then(p => p.FillNumberAsync("88888"))
    //         .Then(p => p.SubmitAsync())
    //         .Then(p => p.ClickBackAsync())
    //         .Then(p => p.FillNumberAsync("11111"))
    //         .Then(p => p.SubmitAsync());
    // }
}