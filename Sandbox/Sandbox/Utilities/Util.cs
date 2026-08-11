using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox.Utilities
{
    public static class Util
    {
        public static async Task BlockAds(IPage page)
        {
            await page.RouteAsync("**/*", async route =>
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
        }

        public static async Task GoToPage(IPage page, string url) =>
            await page.GotoAsync(url);

        public static async Task TakeScreenshot(IPage page) => 
            await page.ScreenshotAsync(new PageScreenshotOptions { Path = @$"C:\Users\kordo\Pictures\Playwright\screenshot-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.png" });
    }
}
