using Microsoft.Playwright;
using Sandbox.TestTemplates;
using Sandbox.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox.WebInputs
{
    public class GoToInputPageTest : WebInputTestTemplate
    {
        private int expectedResponseCode = 200;
        private string inputsUrl = "https://practice.expandtesting.com/inputs";

        [Test]
        public async Task CheckInputsUrlButtonPositive()
        {
            await this.CheckInputsUrlButton(this.inputsUrl, expected: true);
        }

        [Test]
        public async Task CheckInputsUrlButtonNegative()
        {
            await this.CheckInputsUrlButton(string.Empty, expected: false);
        }

        private async Task CheckInputsUrlButton(string url, bool expected)
        {
            await base.Prepare();
            await base.ClickAndWaitForResponseCode(this.UrlButton, expectedResponseCode);
            await this.TestCurrentUrl(url, expected);

            await Util.TakeScreenshot(Page);

            this.CheckTestResult();
        }
    }
}
