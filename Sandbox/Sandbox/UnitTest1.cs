using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace Sandbox
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]

    public class Tests : PageTest
    {
        public void Setup()
        {
            // This method is called before each test. You can use it to set up any necessary state or configuration.
        }

        [Test]
        public async Task WebInput()
        {
            await Page.RouteAsync("**/*", async route =>
            {
                var url = route.Request.Url;

                if (url.Contains("googleads") ||
                    url.Contains("doubleclick") ||
                    url.Contains("adservice"))
                {
                    await route.AbortAsync();
                }
                else
                {
                    await route.ContinueAsync();
                }
            });

            Tests.SetDefaultExpectTimeout(10000);
            await Page.GotoAsync("https://practice.expandtesting.com/");
            //var getBtn = Page.GetByText("Tips", new PageGetByTextOptions { Exact = true });
            //var getBtn = Page.Locator("a[href='/inputs']");
            //var getBtn = Page.GetByRole(AriaRole.Link, new() { Name = "Try it out" }).Filter( new() { Has = Page.Locator("a[href='/my-ip']") });
            //var getBtn = Page
            //    .Locator("a[href='/inputs']")
            //    .Filter( new() { HasText = "Try it out" });
            var getBtn = GetButton("Try it out", "/inputs");
            await Expect(getBtn).ToHaveAttributeAsync("href", "/inputs");
            await getBtn.ClickAsync();
            await Expect(Page).ToHaveURLAsync("https://practice.expandtesting.com/inputs");
            //await Page.WaitForTimeoutAsync(15000);
        }

        public ILocator GetButton(string text, string href)
        {
            return Page
                .Locator($"a[href='{href}']")
                .Filter(new() { HasText = text });
        }


        [Test]
        public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
        {
            await Page.GotoAsync("https://playwright.dev");

            // Expect a title "to contain" a substring.
            await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));

            // create a locator
            var getStarted = Page.Locator("text=Get Started");

            // Expect an attribute "to be strictly equal" to the value.
            await Expect(getStarted).ToHaveAttributeAsync("href", "/docs/intro");

            // Click the get started link.
            await getStarted.ClickAsync();

            // Expects the URL to contain intro.
            await Expect(Page).ToHaveURLAsync(new Regex(".*intro"));
        }
    }
}
