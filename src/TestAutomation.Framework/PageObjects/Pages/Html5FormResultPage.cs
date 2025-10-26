using Microsoft.Playwright;
using TestAutomation.Framework.PageObjects.Base;

namespace TestAutomation.Framework.PageObjects.Pages;

public class Html5FormResultPage<TParent>(IPage page, IBrowserContext context, TParent? previousFragment = null)
    : BasePage(page, context), IBaseFragment<TParent, Html5FormResultPage<TParent>>
    where TParent : BasePage
{
    public TParent? PreviousFragment { get; } = previousFragment;
    
    private ILocator BackLinkLocator => Page.Locator("//a[@id='back_to_form']")
        .Describe("Back Link");
    
    // ========== NAVIGATION ==========

    public async Task<TParent> ClickBackAsync()
    {
        await BackLinkLocator.ClickAsync();
        return await this.GetPreviousFragment();
    }
}