using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumEngine.DriverInitialization;
using SeleniumEngine.Helpers;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace SeleniumEngine.Extensions
{
    public static class WebElementExtension
    {
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetMouseCursorPosition(int X, int Y);
        #region Waits
        public static void WaitToBeClickable(this IWebElement element, string elementName, uint timeoutInSeconds = 60)
        {
            IWait<IWebDriver> wait = new WebDriverWait(WebDriverFactory.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        public static void WaitToBeDisplayed(By locator, string elementName, uint timeoutInSeconds = 60)
        {
            WebDriverWait wait = new WebDriverWait(WebDriverFactory.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public static void WaitToDisapear(By locator, string elementName, uint timeoutInSeconds = 60)
        {
            WebDriverWait wait = new WebDriverWait(WebDriverFactory.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public static void WaitToDisapear(this IWebElement element, string elementName, int numberOfRetries = 5)
        {
            int retry = 0;
            while (element.IsDisplayed(elementName, false, true, 3) && retry < numberOfRetries)
            {
                Thread.Sleep(5000);
                retry++;
            }

            if (retry == numberOfRetries)
            {
                throw new Exception(string.Format("[{0}] is still visible", elementName));
            }
            else
            {
            }
        }

        public static void WaitUntil(this IWebDriver webDriver, Func<IWebDriver, bool> condition, bool ignoreExceptions = false, uint timeoutInSeconds = 60, int pollingIntervalInSeconds = 1)
        {
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
            wait.PollingInterval = TimeSpan.FromSeconds(pollingIntervalInSeconds);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            try
            {
                wait.Until(condition);
            }
            catch
            {
                if (!ignoreExceptions)
                {
                    throw;
                }
            }
        }

        public static void WaitUntil(this IWebElement webElement, Func<IWebElement, object> condition, bool ignoreExceptions = false, uint timeoutInSeconds = 60, int pollingIntervalInSeconds = 1)
        {
            IWait<IWebDriver> wait = new WebDriverWait(WebDriverFactory.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
            wait.PollingInterval = TimeSpan.FromSeconds(pollingIntervalInSeconds);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            try
            {
                wait.Until(_ => condition.Invoke(webElement));
            }
            catch
            {
                if (!ignoreExceptions)
                {
                    throw;
                }
            }
        }

        public static void WaitForProcessing(this IWebDriver webDriver, By locator, uint timeoutInSeconds = 540)
        {
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.IgnoreExceptionTypes
            (
                typeof(NoSuchElementException),
                typeof(ElementNotVisibleException)
            );

            int retry = 0;
            Func<IWebDriver, bool> processFunc = (x) =>
            {
                IWebElement element = x.FindElementEx(locator, 5);

                if (element == null)
                {
                    return retry++ == 2;
                }

                return ((element == null) || (element != null && element.GetCssValue("display").Equals("none")));
            };

            if (webDriver.IsDisplayed(locator, 15))
            {
                wait.Until(processFunc);
            }
        }

        public static void WaitForProcessing(this IWebElement element, uint timeoutInSeconds = 540)
        {
            WebDriverFactory.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(WebDriverFactory.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.IgnoreExceptionTypes
            (
                typeof(NoSuchElementException),
                typeof(ElementNotVisibleException)
            );

            Func<IWebDriver, bool> processFunc = _ =>
            {
                return !element.IsDisplayed(5);
            };

            if (element.IsDisplayed(15))
            {
                wait.Until(processFunc);
            }
        }
        #endregion
        #region Checks
        public static bool IsClickable(this IWebElement element, uint timeoutInSeconds = 60)
        {
            WebDriverFactory.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(WebDriverFactory.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.IgnoreExceptionTypes
            (
                typeof(NoSuchElementException),
                typeof(ElementNotVisibleException)
            );

            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDisplayed(this IWebDriver webDriver, By locator, uint timeoutInSeconds = 60)
        {
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.IgnoreExceptionTypes
            (
                typeof(NoSuchElementException),
                typeof(ElementNotVisibleException)
            );

            try
            {
                IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                return element != null && element.Displayed;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDisplayed(this IWebElement element, uint timeoutInSeconds = 60)
        {
            WebDriverFactory.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(WebDriverFactory.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.IgnoreExceptionTypes
            (
                typeof(NoSuchElementException),
                typeof(ElementNotVisibleException)
            );

            try
            {
                return wait.Until(_ => { return element != null && element.Displayed; });
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDisplayed(this IWebElement element, string elementName, bool useJavaScript = false, bool displayed = true, uint timeoutInSeconds = 60)
        {
            bool result = false;
            try
            {
                WebDriverFactory.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);

                if (useJavaScript)
                {
                    result = JavaScriptHelper.IsDisplayed(WebDriverFactory.WebDriver, element, displayed);
                }
                else
                {
                    result = element.IsDisplayed(timeoutInSeconds);
                }

                if (result)
                {
                }
                else
                {
                }

                return result;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsEnabled(this IWebElement element, string elementName, uint timeoutInSeconds = 60)
        {
            WebDriverFactory.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(WebDriverFactory.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.IgnoreExceptionTypes
            (
                typeof(NoSuchElementException),
                typeof(ElementNotVisibleException)
            );

            try
            {
                return wait.Until(_ => { return element != null && element.Enabled; });
            }
            catch
            {
                return false;
            }
        }

        public static bool DoesElementExist(By locator, uint timeoutInSeconds = 60)
        {
            WebDriverFactory.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(WebDriverFactory.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.IgnoreExceptionTypes
            (
                typeof(NoSuchElementException),
                typeof(ElementNotVisibleException)
            );

            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsAttributePresent(this IWebElement element, string attribute)
        {
            bool result = false;
            try
            {
                string value = element.GetAttribute(attribute);
                if (value != null)
                {
                    result = true;
                }
            }
            catch (Exception exception) { }

            return result;
        }
        #endregion
        #region Actions
        public static void ClickWithCoordinates(this IWebElement element, string elementName, int x, int y)
        {
            Actions builder = new Actions(WebDriverFactory.WebDriver);
            JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
            builder.MoveToElement(element).MoveByOffset(x, y).Click().Build().Perform();
        }

        public static void DoubleClickEx(this IWebElement element, string elementName)
        {
            Actions ClickButton = new Actions(WebDriverFactory.WebDriver);
            JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
            ClickButton.MoveToElement(element).DoubleClick().Build().Perform();
        }

        public static void KeyDownEx(this IWebElement element, string elementName, string keyDown)
        {
            Actions action = new Actions(WebDriverFactory.WebDriver);
            JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
            action.KeyDown(keyDown);
        }

        public static void KeyUpEx(this IWebElement element, string elementName, string keyUp)
        {
            Actions action = new Actions(WebDriverFactory.WebDriver);
            JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
            action.KeyUp(keyUp);
        }

        public static void SelectDropdownValue(this IWebElement element, string value, string elementName, bool useJavaScript = false, bool validateElementValue = true, bool partialMatch = false)
        {
            WaitToBeClickable(element, elementName);

            if (useJavaScript)
            {
                JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
                JavaScriptHelper.SelectDropdownValue(WebDriverFactory.WebDriver, element, value, elementName);
            }
            else
            {
                SelectElement dropdown = new SelectElement(element);
                JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);

                IWebElement option = null;

                if (partialMatch)
                {
                    option = dropdown.Options.Where(x => x.Text.Trim().Contains(value)).FirstOrDefault();
                }
                else
                {
                    option = dropdown.Options.Where(x => x.Text.Trim() == value).FirstOrDefault();
                }

                if (option == null)
                {
                    throw new NoSuchElementException(string.Format(@"Element:[{0}] has no option:[{1}]", elementName, value));
                }

                if (!option.Enabled)
                {
                    throw new InvalidOperationException(string.Format(@"Element:[{0}] option is disabled:[{1}], not able to select", elementName, value));
                }

                dropdown.SelectByText(value, partialMatch);

                if (validateElementValue)
                {
                    Assert.AreEqual(value, dropdown.SelectedOption.Text.Trim(), "Selected option in dropdown is not correct");
                }
            }
        }

        public static void SelectDropdownValueByIndex(this IWebElement element, int index, string elementName, bool useJavaScript = false)
        {
            if (index == 0) { return; }

            WaitToBeClickable(element, elementName);

            if (useJavaScript)
            {
                JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
                JavaScriptHelper.SelectDropdownValueByIndex(WebDriverFactory.WebDriver, element, index, elementName);
            }
            else
            {
                SelectElement dropdown = new SelectElement(element);
                JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
                dropdown.SelectByIndex(index);
            }
        }

        public static string GetSelectedDropdownValue(this IWebElement element, string elementName)
        {
            JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
            SelectElement selectedValue = new SelectElement(element);
            string value = selectedValue.SelectedOption.Text.Trim();
            return value;
        }

        public static string GetValueEx(this IWebElement element)
        {
            try
            {
                string value = null;

                if (element.TagName == "input")
                {
                    string type = element.GetAttribute("type");

                    if (type == "text")
                    {
                        value = element.GetAttribute("value").Trim();
                    }
                    else if (type == "radio" || type == "checkbox")
                    {
                        value = element.GetAttribute("checked") == null ? "false" : "true";
                    }
                    else
                    {
                        value = element.GetAttribute("value").Trim();
                    }
                }
                else if (element.TagName == "select")
                {
                    value = element.GetSelectedDropdownValue("DropDown");
                }
                else if (element.TagName.Contains("button") && (element.IsAttributePresent("selected") || element.IsAttributePresent("aria-selected")))
                {
                    if (element.IsAttributePresent("selected"))
                    {
                        value = element.GetAttribute("selected") == null ? "false" : "true";
                    }
                    else if (element.IsAttributePresent("aria-selected"))
                    {
                        value = element.GetAttribute("aria-selected") == null ? "false" : "true";
                    }
                }
                else
                {
                    value = element.GetAttribute("innerText").Trim();
                }

                return value;
            }
            catch
            {
                return null;
            }
        }

        public static void SetValue(this IWebElement element, string elementName, string value, bool validateElementValue = true, bool useJavaScript = false)
        {
            if (element.TagName == "input")
            {
                string type = element.GetAttribute("type");

                if (type == "text")
                {
                    element.ClearAndSendKeys(value, elementName, useJavaScript, validateElementValue);
                }
                else if (type == "radio")
                {
                    element.ClickEx(elementName);
                }
                else if (type == "checkbox")
                {
                    bool iValue = false;
                    if (!bool.TryParse(value, out iValue))
                    {
                        throw new FormatException("Not able to parse checkbox value");
                    }

                    element.TickCheckboxEx(iValue, elementName);
                }
                else
                {
                    throw new InvalidOperationException("Unsupported input control");
                }
            }
            else if (element.TagName == "select")
            {
                element.SelectDropdownValue(value, elementName, useJavaScript, validateElementValue);
            }
            else
            {
                throw new InvalidOperationException("Unsupported input control");
            }
        }

        public static void ClearEx(this IWebElement element, string elementName, bool useJavaScript = false)
        {
            if (useJavaScript)
            {
                JavaScriptHelper.Clear(WebDriverFactory.WebDriver, element, elementName);
            }
            else
            {
                element.Clear();
            }

            Assert.AreEqual(element.Text, String.Empty, string.Format("Element {0} not cleared", elementName));
        }

        public static void ClickEx(this IWebElement element, string elementName, bool useJavaScript = false)
        {
            int retry = 1;
            int maxRetry = 3;
            bool isClicked = false;

            while (!isClicked && retry <= maxRetry)
            {
                try
                {
                    WaitToBeClickable(element, elementName);

                    if (useJavaScript)
                    {
                        JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
                        JavaScriptHelper.Click(WebDriverFactory.WebDriver, element, elementName);
                    }
                    else
                    {
                        JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
                        element.Click();
                    }

                    isClicked = true;
                }
                catch (Exception ex)
                {
                    if (retry == maxRetry)
                    {
                        throw new InvalidOperationException(string.Format("Cannot click:{0} after {1} retries", elementName, maxRetry), ex);
                    }

                    retry++;
                }
            }
        }

        public static void SendKeysEx(this IWebElement element, string value, string elementName, bool useJavaScript = false, bool validateElementValue = true)
        {
            try
            {
                if (useJavaScript)
                {
                    JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
                    JavaScriptHelper.SetValue(WebDriverFactory.WebDriver, element, value, elementName);
                }
                else
                {
                    JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
                    element.SendKeys(value);
                }

                if (validateElementValue)
                {
                    Assert.AreEqual(value, element.GetAttribute("value"), string.Format("Failed setting value to: {0}", elementName));
                }
            }
            catch
            {
                if (useJavaScript)
                {
                    JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
                    JavaScriptHelper.SetValue(WebDriverFactory.WebDriver, element, value, elementName);
                }
                else
                {
                    JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
                    element.ClearEx(elementName);
                    element.SendKeys(value);
                }

                if (validateElementValue)
                {
                    Assert.AreEqual(value, element.GetAttribute("value"), string.Format("Failed setting value to: {0}", elementName));
                }
            }
        }

        public static void ClearAndSendKeys(this IWebElement element, string value, string elementName, bool useJavaScript = false, bool validateValue = true)
        {
            if (useJavaScript)
            {
                SendKeysEx(element, value, elementName, true);
            }
            else
            {
                element.Clear();
                SendKeysEx(element, value, elementName, useJavaScript, validateValue);
                LoseFocus();
            }
        }

        public static void PasteValueEx(this IWebElement element, string value, string elementName, bool validateElementValue = true)
        {
            JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
            TextCopy.ClipboardService.SetText(value);
            element.SendKeys(Keys.Control + "v");

            Assert.AreEqual(value, element.GetAttribute("value"));

            if (validateElementValue)
            {
                element.ValidateAttribute("value", value, elementName);
            }
        }

        public static void TickCheckboxEx(this IWebElement element, bool value, string elementName, bool useJavaScript = false)
        {
            WaitToBeClickable(element, elementName);

            if (useJavaScript)
            {
                JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
                JavaScriptHelper.TickCheckBox(WebDriverFactory.WebDriver, element, value);
            }
            else
            {
                if ((element.GetAttribute("checked") != "true" && value == true) || (element.GetAttribute("checked") == "true" && value == false))
                {
                    element.Click();
                    JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
                }
            }
        }

        public static void TickCheckboxEx(this IWebElement checkbox, IWebElement label, string value, string elementName)
        {
            WaitToBeClickable(label, elementName);

            bool isChecked = checkbox.GetAttribute("checked") == "true";
            bool shouldBeChecked = Convert.ToBoolean(value.ToLower());

            if ((!isChecked && shouldBeChecked) ||
                (isChecked && !shouldBeChecked))
            {
                label.Click();
                JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, label);
            }
        }

        public static void LoseFocus()
        {
            WebDriverFactory.WebDriver.FindElement(By.TagName("body")).SendKeys(Keys.Tab);
        }

        public static void NavigateToUrl(this IWebDriver webDriver, string url)
        {
            WebDriverFactory.WebDriver.Navigate().GoToUrl(url);
        }

        #region Mouse actions
        public static void MouseClickEx(this IWebElement element, string elementName)
        {
            WaitToBeClickable(element, elementName);
            Actions builder = new Actions(WebDriverFactory.WebDriver);
            JavaScriptHelper.HighlightElement(WebDriverFactory.WebDriver, element);
            builder.MoveToElement(element).Click().Perform();
        }

        public static void MouseHover(this IWebElement element, string elementName)
        {
            JavaScriptHelper.MouseOver(WebDriverFactory.WebDriver, element);
        }

        public static void MouseHoverOut(this IWebElement element, string elementName)
        {
            JavaScriptHelper.MouseOut(WebDriverFactory.WebDriver, element);
        }

        public static void MoveMouse(int x, int y)
        {
            SetMouseCursorPosition(x, y);
        }
        #endregion
        #endregion
        #region Validations
        public static void ValidateValue(this IWebElement element, string elementName, string expectedValue)
        {
            string current = element.Text.Trim();
            string expected = expectedValue.Trim();

            Assert.IsTrue(current.Contains(expected),
                          string.Format("Failed validating value for: {0} - expected value: {1}, actual value: {2}",
                          elementName, current, expected));
        }

        public static void ValidateDropdownValue(this IWebElement element, string expectedValue, string elementName)
        {
            SelectElement selectElement = new SelectElement(element);
            var option = selectElement.Options.FirstOrDefault(x => x.Selected == true);

            Assert.IsTrue(option != null, string.Format("Value: {1} is not selected for: {1} dropdown", elementName, expectedValue));
            Assert.AreEqual(expectedValue, option.Text, string.Format("{0} dropdown value doesn't match expected value: {1} ", elementName, expectedValue));
        }

        public static void ValidateAttribute(this IWebElement element, string attribute, string expectedValue, string elementName, bool useJavaScript = false)
        {
            if (useJavaScript)
            {
                JavaScriptHelper.ValidateAttribute(WebDriverFactory.WebDriver, element, attribute, expectedValue);
            }
            else
            {
                Assert.AreEqual(expectedValue, element.GetAttribute(attribute), string.Format("Attribute's: {0} value not matching expected value: {1}", attribute, expectedValue));
            }
        }

        public static void ValidateAttributeValue(this IWebElement element, string attribute, string expectedValue)
        {
            Assert.IsTrue(element.GetAttribute(attribute).Contains(expectedValue),
                          string.Format("Attribute's: {0} value: {1} does not contain expected value:{2}",
                                         attribute, element.GetAttribute(attribute), expectedValue));
        }
        #endregion
        #region Find
        public static IWebElement FindElementEx(this IWebDriver webDriver, By locator, uint timeoutInSeconds = 60, int pollingIntervalInSeconds = 1)
        {
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.PollingInterval = TimeSpan.FromSeconds(pollingIntervalInSeconds);

            wait.IgnoreExceptionTypes
            (
                typeof(NoSuchElementException),
                typeof(WebDriverTimeoutException),
                typeof(UnhandledAlertException),
                typeof(StaleElementReferenceException)
            );

            try
            {
                return wait.Until(x =>
                {
                    try
                    {
                        return x.FindElement(locator);
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                });
            }
            catch
            {
                return null;
            }
        }

        public static IWebElement FindElementEx(this IWebElement element, By locator, uint timeoutInSeconds = 60, int pollingIntervalInSeconds = 1)
        {
            WebDriverFactory.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(WebDriverFactory.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.PollingInterval = TimeSpan.FromSeconds(pollingIntervalInSeconds);
            wait.IgnoreExceptionTypes
            (
                typeof(NoSuchElementException),
                typeof(WebDriverTimeoutException),
                typeof(UnhandledAlertException)
            );

            try
            {
                return wait.Until(_ =>
                {
                    try
                    {
                        return element.FindElement(locator);
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                });
            }
            catch
            {
                return null;
            }
        }

        public static ReadOnlyCollection<IWebElement> FindElementsEx(this IWebDriver webDriver, By locator, uint timeoutInSeconds = 60, int pollingIntervalInSeconds = 1)
        {
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.PollingInterval = TimeSpan.FromSeconds(pollingIntervalInSeconds);

            wait.IgnoreExceptionTypes
            (
                typeof(NoSuchElementException),
                typeof(WebDriverTimeoutException),
                typeof(UnhandledAlertException),
                typeof(StaleElementReferenceException)
            );

            return wait.Until(x => x.FindElements(locator));
        }

        public static ReadOnlyCollection<IWebElement> FindElementsEx(this IWebElement element, By locator, uint timeoutInSeconds = 60, int pollingIntervalInSeconds = 1)
        {
            WebDriverFactory.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(WebDriverFactory.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.PollingInterval = TimeSpan.FromSeconds(pollingIntervalInSeconds);

            wait.IgnoreExceptionTypes
            (
                typeof(NoSuchElementException),
                typeof(WebDriverTimeoutException),
                typeof(UnhandledAlertException),
                typeof(StaleElementReferenceException)
            );

            return wait.Until(_ => element.FindElements(locator));
        }
        #endregion
    }
}