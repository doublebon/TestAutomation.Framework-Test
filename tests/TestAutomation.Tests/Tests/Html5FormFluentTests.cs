using NUnit.Framework;
using TestAutomation.Framework.Extensions;
using TestAutomation.Framework.PageObjects.Base;
using TestAutomation.Framework.PageObjects.Pages;

namespace TestAutomation.Tests.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class Html5FormFluentTests : BaseTest
{
    private const string BaseUrl = "https://testpages.eviltester.com/styled/html5-form-test.html";

    [SetUp]
    public async Task NavigateToForm()
    {
        await Page.GotoAsync(BaseUrl);
    }
    
    [Test]
    public async Task Test01_CompleteChain()
    {
        await new Html5FormPage(Page, Context)
            .WaitForFormLoadedAsync()
            .Then(p => p.FillDateAsync(DateTime.Today.ToString("yyyy-MM-dd")))
            .Then(p => p.FillLocalDateTimeAsync(DateTime.Today.ToString("yyyy-MM-ddTHH:mm")))
            .Then(p => p.FillEmailAsync("someEmail@mail.ru"))
            .Then(p => p.FillMonthAsync(DateTime.Today.ToString("yyyy-MM")))
            .Then(p => p.FillNumberAsync("88888"))
            .Then(p => p.SubmitAsync())
            .Then(p => p.ClickBackAsync())
            .Then(p => p.FillNumberAsync("11111"))
            .Then(p => p.SubmitAsync());
    }
}