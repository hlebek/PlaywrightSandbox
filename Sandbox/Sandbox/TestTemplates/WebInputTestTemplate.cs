using Microsoft.Playwright;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Sandbox.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox.TestTemplates
{
    public class WebInputTestTemplate : PageTest
    {
        public ILocator UrlButton;

        internal string UrlBtnText = string.Empty;

        internal string UrlBtnHref = string.Empty;

        private bool testPassed;

        #region Constructors

        #endregion

        #region methods

        public void CheckTestResult()
        {
            Assert.That(this.testPassed, Is.True, "One or more assertions failed.");
        }

        #endregion

        #region internal methods

        /// <summary>
        /// Prepares the page for testing. Blocks ads and goes to specified URL.
        /// </summary>
        /// <param name="url">
        /// The URL to navigate to for testing.
        /// </param>
        /// <returns>
        /// The task representing the asynchronous operation of preparing the page.
        /// </returns>
        internal virtual async Task Prepare(string url = "https://practice.expandtesting.com")
        {
            this.testPassed = true;
            this.UrlBtnText = "Try it out";
            this.UrlBtnHref = "/inputs";
            this.UrlButton = GetElement.GetButtonByHref(Page, UrlBtnText, UrlBtnHref);
            WebInputTestTemplate.SetDefaultExpectTimeout(10000);
            await Util.BlockAds(Page);
            await Util.GoToPage(Page, url);
        }

        /// <summary>
        /// Tests the number input box by filling it with a test value and asserting that the value is correctly set.
        /// </summary>
        /// <param name="boxId">
        /// The ID of the number input box to be tested.
        /// </param>
        /// <returns>
        /// The task representing the asynchronous operation of testing the number input box.
        /// </returns>
        internal async Task<bool> TestNumberInputBox(string boxId, string input, bool expected = true)
        {
            ILocator inputNumber = GetElement.GetElementById(Page, boxId);

            try
            {
                await inputNumber.FillAsync(input);
            }
            catch { }

            return await MyAssert(async () => await Expect(inputNumber).ToHaveValueAsync(input), expected);
        }

        /// <summary>
        /// Tests the current URL of the page against the expected URL.
        /// </summary>
        /// <param name="url">
        /// The expected URL to test against the current page URL.
        /// </param>
        /// <returns>
        /// The task representing the asynchronous operation of testing the current URL.
        /// </returns>
        internal async Task<bool> TestCurrentUrl(string url, bool expected = true) =>
            await MyAssert(async () => await Expect(Page).ToHaveURLAsync(url), expected);

        /// <summary>
        /// Tests the input box by filling it with a test value and asserting that the value is correctly set.
        /// </summary>
        /// <param name="boxId">
        /// The ID of the input box to be tested.
        /// </param>
        /// <returns>
        /// The task representing the asynchronous operation of testing the input box.
        /// </returns>
        internal async Task<bool> TestInputBox(string boxId, string input, bool expected = true)
        {
            ILocator inputBox = GetElement.GetElementById(Page, boxId);

            await inputBox.FillAsync(input);
            return await MyAssert(async () => await Expect(inputBox).ToHaveValueAsync(input), expected);
        }

        internal async Task RefreshPage() =>
            await Page.ReloadAsync();

        internal async Task ClickAndWaitForResponseCode(ILocator button, int ExpectedRespCode) =>
            await Page.RunAndWaitForResponseAsync(async () => await button.ClickAsync(), x => x.Status == ExpectedRespCode);

        #endregion

        #region private methods

        /// <summary>
        /// Asserts the result of an asynchronous assertion function against an expected boolean value.
        /// </summary>
        /// <param name="assertion">
        /// The asynchronous assertion delegate to be executed.
        /// </param>
        /// <param name="expected">
        /// The expected boolean value for the assertion result.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation, containing the result of the assertion.
        /// </returns>
        private async Task<bool> MyAssert(Func<Task> assertion, bool expected = true)
        {
            bool result = true;

            try
            {
                await assertion();
            }
            catch
            {
                result = false;
            }

            Console.WriteLine($"Testing {this.GetType().Name}. Assertion result: {result}. Expected: {expected}.");

            if (result != expected)
            {
                this.testPassed = false;
            }

            return result;
        }

        #endregion
    }
}
