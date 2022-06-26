using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumCore.WebDriver;
using SeleniumCore.Helpers.Utilities;
using System.Collections;

namespace SeleniumCore.Helpers.Extensions
{
    public static class WebElementExtension
    {
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int X, int Y);

        public static void WaitForControlToBeClickable(this IWebElement element, string elementName, uint timeoutInSeconds = 60)
        {
            IWait<IWebDriver> wait = new WebDriverWait(UIDriver.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        public static void WaitControlToBeDisplayed(By locator, string elementName, uint timeoutInSeconds = 60)
        {
            WebDriverWait wait = new WebDriverWait(UIDriver.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public static void WaitControlToDisapear(By locator, string elementName, uint timeoutInSeconds = 60)
        {
            WebDriverWait wait = new WebDriverWait(UIDriver.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public static void WaitControlToDisapear(this IWebElement element, string elementName, int numberOfRetries = 5)
        {
            int retry = 0;
            while (element.IsDisplayedWrapper(elementName, false, true, 3) && retry < numberOfRetries)
            {
                Thread.Sleep(5000);
                retry++;
            }

            if (retry == numberOfRetries)
            {
                throw new Exception(String.Format("[EXCEPTION] - Element:[{0}] didn't disapear from the page ", elementName));
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
            IWait<IWebDriver> wait = new WebDriverWait(UIDriver.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
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
                IWebElement element = x.SafeFindElement(locator, 5);

                if (element == null)
                {
                    return retry++ == 2;
                }

                return ((element == null) || (element != null && element.GetCssValue("display").Equals("none")));
            };

            if (webDriver.IsControlDisplayed(locator, 15))
            {
                wait.Until(processFunc);
            }
        }

        public static void WaitForProcessing(this IWebElement element, uint timeoutInSeconds = 540)
        {
            UIDriver.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(UIDriver.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.IgnoreExceptionTypes
            (
                typeof(NoSuchElementException),
                typeof(ElementNotVisibleException)
            );

            Func<IWebDriver, bool> processFunc = _ =>
            {
                return !element.IsControlDisplayed(5);
            };

            if (element.IsControlDisplayed(15))
            {
                wait.Until(processFunc);
            }
        }

        public static bool IsControlClickable(this IWebElement element, uint timeoutInSeconds = 60)
        {
            UIDriver.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(UIDriver.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
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

        public static bool IsControlDisplayed(this IWebDriver webDriver, By locator, uint timeoutInSeconds = 60)
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

        public static bool IsControlDisplayed(this IWebElement element, uint timeoutInSeconds = 60)
        {
            UIDriver.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(UIDriver.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.IgnoreExceptionTypes
            (
                typeof(NoSuchElementException),
                typeof(ElementNotVisibleException)
            );

            try
            {
                return wait.Until(_ =>
                {
                    return element != null && element.Displayed;
                });
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDisplayedWrapper(this IWebElement element, string elementName, bool js = false, bool displayed = true, uint timeoutInSeconds = 60)
        {
            bool result = false;
            try
            {
                UIDriver.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);

                if (js)
                {
                    result = JavaScriptHelper.IsDisplayed(UIDriver.WebDriver, element, displayed);
                }
                else
                {
                    result = element.IsControlDisplayed(timeoutInSeconds);
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

        public static bool IsControlEnabled(this IWebElement element, string elementName, uint timeoutInSeconds = 60)
        {
            UIDriver.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(UIDriver.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.IgnoreExceptionTypes
            (
                typeof(NoSuchElementException),
                typeof(ElementNotVisibleException)
            );

            try
            {
                return wait.Until(_ =>
                {
                    return element != null && element.Enabled;
                });
            }
            catch
            {
                return false;
            }
        }

        public static bool IsControlExisting(By locator, uint timeoutInSeconds = 60)
        {
            UIDriver.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(UIDriver.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
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

        public static void ClickActionWithCoordinatesWrapper(this IWebElement element, string elementName, int x, int y)
        {
            Actions builder = new Actions(UIDriver.WebDriver);
            JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
            builder.MoveToElement(element).MoveByOffset(x, y).Click().Build().Perform();
        }

        public static void DoubleClickActionWrapper(this IWebElement element, string elementName)
        {
            Actions ClickButton = new Actions(UIDriver.WebDriver);
            JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
            ClickButton.MoveToElement(element).DoubleClick().Build().Perform();
        }

        public static void KeyDownWrapper(this IWebElement element, string elementName, string keyDown)
        {
            Actions action = new Actions(UIDriver.WebDriver);
            JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
            action.KeyDown(keyDown);
        }

        public static void KeyUpWrapper(this IWebElement element, string elementName, string keyUp)
        {
            Actions action = new Actions(UIDriver.WebDriver);
            JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
            action.KeyUp(keyUp);
        }

        public static void SelectComboboxValue(this IWebElement element, string value, string elementName, bool js = false, bool validateElementValue = true, bool partialMatch = false)
        {
            WaitForControlToBeClickable(element, elementName);

            if (js)
            {
                JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
                JavaScriptHelper.SelectComboboxValue(UIDriver.WebDriver, element, value, elementName);
            }
            else
            {
                SelectElement dropdown = new SelectElement(element);
                JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);

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

        public static void SelectComboboxValueByIndex(this IWebElement element, int index, string elementName, bool js = false)
        {
            if (index == 0)
            {
                return;
            }

            WaitForControlToBeClickable(element, elementName);

            if (js)
            {
                JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
                JavaScriptHelper.SelectComboboxValueByIndex(UIDriver.WebDriver, element, index, elementName);
            }
            else
            {
                SelectElement dropdown = new SelectElement(element);
                JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
                dropdown.SelectByIndex(index);
            }
        }

        public static string GetSelectedComboboxValue(this IWebElement element, string elementName)
        {
            JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
            SelectElement selectedValue = new SelectElement(element);
            string value = selectedValue.SelectedOption.Text.Trim();
            return value;
        }

        public static string GetSelectedUIComboboxValue(this IWebElement element, string elementName)
        {
            string value = element.Text;
            return value;
        }

        public static void ClearWrapper(this IWebElement element, string elementName, bool js = false)
        {
            if (js)
            {
                JavaScriptHelper.Clear(UIDriver.WebDriver, element, elementName);
            }
            else
            {
                element.Clear();
            }

            Assert.AreEqual(element.Text, String.Empty, "Element is not cleared");
        }

        public static void ClickWrapper(this IWebElement element, string elementName, bool js = false)
        {
            int retry = 1;
            int maxRetry = 3;
            bool isClicked = false;

            while (!isClicked && retry <= maxRetry)
            {
                try
                {
                    WaitForControlToBeClickable(element, elementName);

                    if (js)
                    {
                        JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
                        JavaScriptHelper.Click(UIDriver.WebDriver, element, elementName);
                    }
                    else
                    {
                        JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
                        element.Click();
                    }

                    isClicked = true;
                }
                catch (Exception ex)
                {
                    if (retry == maxRetry)
                    {
                        throw new InvalidOperationException($"It's not able to click on element:[{elementName}] after [{maxRetry}] retrys", ex);
                    }

                    retry++;
                }
            }
        }

        public static void SendKeysWrapper(this IWebElement element, string value, string elementName, bool js = false, bool validateElementValue = true)
        {
            try
            {
                if (js)
                {
                    JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
                    JavaScriptHelper.SetValue(UIDriver.WebDriver, element, value, elementName);
                }
                else
                {
                    JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
                    element.SendKeys(value);
                }

                if (validateElementValue)
                {
                    Assert.AreEqual(value, element.GetAttribute("value"), string.Format("Element:[{0}] is NOT populated with correct value", elementName));
                }
            }
            catch
            {
                if (js)
                {
                    JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
                    JavaScriptHelper.SetValue(UIDriver.WebDriver, element, value, elementName);
                }
                else
                {
                    JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
                    element.ClearWrapper(elementName);
                    element.SendKeys(value);
                }

                if (validateElementValue)
                {
                    Assert.AreEqual(value, element.GetAttribute("value"), string.Format("Element:[{0}] is NOT populated with correct value", elementName));
                }
            }
        }

        public static void SendKeysWithClear(this IWebElement element, string value, string elementName, bool js = false, bool validateValue = true)
        {
            if (js)
            {
                SendKeysWrapper(element, value, elementName, true);
            }
            else
            {
                element.Clear();
                SendKeysWrapper(element, value, elementName, js, validateValue);
                LoseControlFocus();
            }
        }

        public static void PasteValueWrapper(this IWebElement element, string value, string elementName, bool validateElementValue = true)
        {
            JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
            //System.Windows.Forms.Clipboard.SetText(value);
            element.SendKeys(Keys.Control + "v");

            Assert.AreEqual(value, element.GetAttribute("value"));

            if (validateElementValue)
            {
                element.ValidateElementAttribute("value", value, elementName);
            }
        }

        public static void CheckboxWrapper(this IWebElement element, bool value, string elementName, bool js = false)
        {
            WaitForControlToBeClickable(element, elementName);

            if (js)
            {
                JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
                JavaScriptHelper.CheckBox(UIDriver.WebDriver, element, value);
            }
            else
            {
                if ((element.GetAttribute("checked") != "true" && value == true) || (element.GetAttribute("checked") == "true" && value == false))
                {
                    element.Click();
                    JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
                }
            }
        }

        public static void CheckboxWrapper(this IWebElement checkbox, IWebElement label, string value, string elementName)
        {
            WaitForControlToBeClickable(label, elementName);

            bool isChecked = checkbox.GetAttribute("checked") == "true";
            bool shouldBeChecked = Convert.ToBoolean(value.ToLower());

            if ((!isChecked && shouldBeChecked) ||
                (isChecked && !shouldBeChecked))
            {
                label.Click();
                JavaScriptHelper.HighlightElement(UIDriver.WebDriver, label);
            }
        }

        public static void ValidateElementValue(this IWebElement element, string elementName, string expectedValue)
        {
            string current = element.Text.Trim();
            string expected = expectedValue.Trim();

            Assert.IsTrue(current.Contains(expected),
                          string.Format("Element:[{0}] doesn't contains expected value, element value:[{1}], exected value:[{2}]",
                          elementName, current, expected));
        }

        public static void ValidateComboboxValue(this IWebElement element, string expectedValue, string elementName)
        {
            SelectElement selectElement = new SelectElement(element);
            var option = selectElement.Options.FirstOrDefault(x => x.Selected == true);

            Assert.IsTrue(option != null, string.Format("Combobox:[{0}] does not have pre-selected value of:[{1}]", elementName, expectedValue));
            Assert.AreEqual(expectedValue, option.Text, string.Format("Combobox [{0}] is NOT pre-selected with value:[{1}] ", elementName, expectedValue));
        }

        public static void ValidateElementAttribute(this IWebElement element, string attribute, string expectedValue, string elementName, bool js = false)
        {
            if (js)
            {
                JavaScriptHelper.ValidateElementAttribute(UIDriver.WebDriver, element, attribute, expectedValue);
            }
            else
            {
                Assert.AreEqual(expectedValue, element.GetAttribute(attribute), string.Format("Attribute:[{0}] value is not matching", attribute));
            }
        }

        public static void ValidateElementAttributeContains(this IWebElement element, string attribute, string expectedValue)
        {
            Assert.IsTrue(element.GetAttribute(attribute).Contains(expectedValue),
                          string.Format("Attribute:[{0}] with value:[{1}] does not contain expected value:[{2}]",
                                         attribute, element.GetAttribute(attribute), expectedValue));
        }

        public static void MouseClickActionWrapper(this IWebElement element, string elementName)
        {
            WaitForControlToBeClickable(element, elementName);
            Actions builder = new Actions(UIDriver.WebDriver);
            JavaScriptHelper.HighlightElement(UIDriver.WebDriver, element);
            builder.MoveToElement(element).Click().Perform();
        }

        public static void MouseHover(this IWebElement element, string elementName)
        {
            JavaScriptHelper.ExecuteMouseOver(UIDriver.WebDriver, element);
        }

        public static void MouseHoverOut(this IWebElement element, string elementName)
        {
            JavaScriptHelper.ExecuteMouseOut(UIDriver.WebDriver, element);
        }

        public static void MoveMouse(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public static IWebElement SafeFindElement(this IWebDriver webDriver, By locator, uint timeoutInSeconds = 60, int pollingIntervalInSeconds = 1)
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

        public static IWebElement SafeFindElement(this IWebElement element, By locator, uint timeoutInSeconds = 60, int pollingIntervalInSeconds = 1)
        {
            UIDriver.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(UIDriver.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
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

        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver webDriver, By locator, uint timeoutInSeconds = 60, int pollingIntervalInSeconds = 1)
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

        public static ReadOnlyCollection<IWebElement> FindElements(this IWebElement element, By locator, uint timeoutInSeconds = 60, int pollingIntervalInSeconds = 1)
        {
            UIDriver.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IWait<IWebDriver> wait = new WebDriverWait(UIDriver.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
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

        public static string SafeValue(this IWebElement element)
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
                    value = element.GetSelectedComboboxValue("DropDown");
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

        public static void SetValue(this IWebElement element, string elementName, string value, bool validateElementValue = true, bool js = false)
        {
            if (element.TagName == "input")
            {
                string type = element.GetAttribute("type");

                if (type == "text")
                {
                    element.SendKeysWithClear(value, elementName, js, validateElementValue);
                }
                else if (type == "radio")
                {
                    element.ClickWrapper(elementName);
                }
                else if (type == "checkbox")
                {
                    bool iValue = false;
                    if (!bool.TryParse(value, out iValue))
                    {
                        throw new FormatException("Not able to parse value for checkbox");
                    }

                    element.CheckboxWrapper(iValue, elementName);
                }
                else
                {
                    throw new InvalidOperationException("Unsupported input control");
                }
            }
            else if (element.TagName == "select")
            {
                element.SelectComboboxValue(value, elementName, js, validateElementValue);
            }
            else
            {
                throw new InvalidOperationException("Unsupported input control");
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
            catch (Exception e) { }

            return result;
        }

        public static void LoseControlFocus()
        {
            UIDriver.WebDriver.FindElement(By.TagName("body")).SendKeys(Keys.Tab);
        }
    }
}
