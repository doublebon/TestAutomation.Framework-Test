using Microsoft.Playwright;
using TestAutomation.Framework.PageObjects.Base;

namespace TestAutomation.Framework.PageObjects.Pages;

public class DynamicIdPage(IPage page, IBrowserContext context) : BasePage(page, context)
{
    
    private ILocator StartBtn => Page.Locator("//*[@id=\"startButton\"]");
    private ILocator StopBtn  => Page.Locator("//*[@id=\"stopButton\"]");
    
    
    public async Task<DynamicIdPage> ClickOnStart()
    {
        await StartBtn.ClickAsync();
        return this;
    }
    
    public async Task<DynamicIdPage> ClickOnStop()
    {
        await StopBtn.ClickAsync();
        return this;
    }
}