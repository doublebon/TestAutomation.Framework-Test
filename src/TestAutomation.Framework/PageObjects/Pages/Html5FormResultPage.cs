using Microsoft.Playwright;
using TestAutomation.Framework.PageObjects.Base;

namespace TestAutomation.Framework.PageObjects.Pages;

public class Html5FormResultPage<TParent>(IPage page, IBrowserContext context, TParent? previousFragment = null)
    : BasePage<TParent, Html5FormResultPage<TParent>>(page, context)
    where TParent : class, IBaseFragment<object, object>
{
    public TParent? PreviousFragment { get; } = previousFragment;

    public override Html5FormResultPage<TParent> CurrentFragment => this;

    private ILocator BackLinkLocator => Page.Locator("//a[@id='back_to_form']")
        .Describe("Back Link");
    
    // ========== NAVIGATION ==========

    public async Task<TParent> ClickBackAsync()
    {
        await BackLinkLocator.ClickAsync();
        return await this.GetPreviousFragment();
    }
}