using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Sandbox.TestTemplates;
using Sandbox.Utilities;

namespace Sandbox.WebInputs
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]

    public class TextBoxTest : WebInputTestTemplate
    {
        private string boxId = "input-text";

        private List<string> boxValuePositive = new List<string>
        {
            "1234567890",
            "abc",
            "abc123",
            "1.23",
            "1,23",
            string.Empty,
            "   ",
            "!@#$%^&*()",
        };

        private List<string> boxValuesNegative = new List<string>
        {
            "\n\n",
        };

        [Test]
        public async Task TestWebInputTextBoxPositive()
        {
            await this.TestWebInputTextBox(boxValuePositive, expected: true);
        }

        [Test]
        public async Task TestWebInputTextBoxNegative()
        {
            await this.TestWebInputTextBox(boxValuesNegative, expected: false);
        }

        private async Task TestWebInputTextBox(List<string> values, bool expected)
        {
            await this.Prepare("https://practice.expandtesting.com/inputs");

            foreach (var value in values)
            {
                await this.RefreshPage();
                await this.TestInputBox(boxId, value, expected);
            }

            await Util.TakeScreenshot(Page);

            this.CheckTestResult();
        }
    }
}
