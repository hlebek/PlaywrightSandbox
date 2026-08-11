using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox.Utilities
{
    public static class GetElement
    {
        public static ILocator GetButtonByHref(IPage page, string buttonText, string buttonHref) =>
            page.Locator($"a[href='{buttonHref}']").Filter(new() { HasText = buttonText });

        public static ILocator GetNumberInputBoxByName(IPage page, string name) =>
            page.GetByRole(AriaRole.Spinbutton, new() { Name = name });
        
        public static ILocator GetElementById(IPage page, string id) =>
            page.Locator($"#{id}");
    }
}
