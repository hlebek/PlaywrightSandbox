using Microsoft.Playwright;
using Sandbox.TestTemplates;
using Sandbox.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox.WebInputs
{
    public class NumberBoxTest : WebInputTestTemplate
    {
        private string boxId = "input-number";

        private List<string> boxValuePositive = new List<string>
        {
            "1234567890",
            "0",
            "9999999999",
            "42",
            "1.23",
            string.Empty,
        };

        private List<string> boxValuesNegative = new List<string>
        {
            "abc",
            "!@#$%^&*()",
            "1,23",
        };

        [Test]
        public async Task TestWebInputNumberBoxPositive()
        {
            await this.TestWebInputNumberBox(boxValuePositive, expected: true);
        }

        [Test]
        public async Task TestWebInputNumberBoxNegative()
        {
            await this.TestWebInputNumberBox(boxValuesNegative, expected: false);
        }

        private async Task TestWebInputNumberBox(List<string> values, bool expected)
        {
            await this.Prepare("https://practice.expandtesting.com/inputs");

            foreach (var value in values)
            {
                await this.RefreshPage();
                await this.TestNumberInputBox(boxId, value, expected);
            }

            await Util.TakeScreenshot(Page);

            this.CheckTestResult();
        }
    }
}
