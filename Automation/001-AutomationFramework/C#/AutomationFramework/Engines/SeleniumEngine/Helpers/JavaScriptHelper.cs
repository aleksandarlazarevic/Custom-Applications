using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumEngine.DriverInitialization;

namespace SeleniumEngine.Helpers
{
    public static class JavaScriptHelper
    {
        #region Actions
        public static object ExecuteJSCode(IWebDriver webdriver, string jsCode)
        {
            return (webdriver as IJavaScriptExecutor).ExecuteScript(jsCode);
        }

        public static void Click(IWebDriver webdriver, IWebElement element, string elementName)
        {
            (webdriver as IJavaScriptExecutor).ExecuteScript("arguments[0].click();", element);
        }

        public static void Clear(IWebDriver webdriver, IWebElement element, string elementName)
        {
            (webdriver as IJavaScriptExecutor).ExecuteScript("arguments[0].value = '';", element);
        }

        public static void SetValue(IWebDriver webdriver, IWebElement element, string value, string elementName)
        {
            string pattern = string.Format("arguments[0].value = '{0}';", value);
            (webdriver as IJavaScriptExecutor).ExecuteScript(pattern, element);
        }

        public static void ValidateDropdownValue(IWebDriver webdriver, IWebElement element, string value, string elementName)
        {
            object result = (webdriver as IJavaScriptExecutor).ExecuteScript("return arguments[0].options[arguments[0].selectedIndex].text", element);
            Assert.AreEqual(result.ToString(), value, string.Format("Failed to select [{0}] dropdown", elementName));
        }

        public static void SelectDropdownValue(IWebDriver webdriver, IWebElement element, string value, string elementName)
        {
            string pattern = string.Format("var length = arguments[0].options.length;  for (var i=0; i<length; i++){{  if (arguments[0].options[i].text == '{0}'){{ arguments[0].selectedIndex = i; break; }} }}", value);
            object result = (webdriver as IJavaScriptExecutor).ExecuteScript(pattern, element);
            ValidateDropdownValue(webdriver, element, value, elementName);
        }

        public static void SelectDropdownValueByIndex(IWebDriver webdriver, IWebElement element, int index, string elementName)
        {
            string pattern = string.Format("var length = arguments[0].options.length;  for (var i=1; i<length; i++){{  if (arguments[0].options[i].index == '{0}'){{ arguments[0].selectedIndex = i; break; }} }}", index);
            object result = (webdriver as IJavaScriptExecutor).ExecuteScript(pattern, element);
        }

        public static void TickCheckBox(IWebDriver webdriver, IWebElement element, bool value)
        {
            string statement = string.Format("if((arguments[0].checked == true && '{0}' == 'false') || (arguments[0].checked == false && '{0}' == 'true')) arguments[0].click(); ", value.ToString().ToLower());
            object result = (webdriver as IJavaScriptExecutor).ExecuteScript(statement, element);
        }

        public static void RefreshPage(IWebDriver webdriver)
        {
            (webdriver as IJavaScriptExecutor).ExecuteScript("location.reload();");
        }

        public static string GetText(IWebDriver webdriver, IWebElement element)
        {
            return (webdriver as IJavaScriptExecutor).ExecuteScript("return arguments[0].innerText", element).ToString();
        }

        public static string GetSelectedDropdownValue(IWebDriver webdriver, IWebElement element, string elementName)
        {
            return (webdriver as IJavaScriptExecutor).ExecuteScript("return arguments[0].options[arguments[0].selectedIndex].text", element).ToString();
        }

        #region Mouse actions
        public static void MouseOver(IWebDriver webdriver, IWebElement element)
        {
            object result = (webdriver as IJavaScriptExecutor).ExecuteScript("var evObj = document.createEvent('MouseEvents'); " +
                "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                "arguments[0].dispatchEvent(evObj);", element);
        }

        public static void MouseOut(IWebDriver webdriver, IWebElement element)
        {
            object result = (webdriver as IJavaScriptExecutor).ExecuteScript("var evObj = document.createEvent('MouseEvents'); " +
                "evObj.initMouseEvent(\"mouseout\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                "arguments[0].dispatchEvent(evObj);", element);
        }

        public static void ScrollIntoView(IWebElement element, string option = "true")
        {
            (WebDriverFactory.WebDriver as IJavaScriptExecutor).ExecuteScript($"arguments[0].scrollIntoView({option});", element);
        }
        #endregion
        public static void HighlightElement(IWebDriver webdriver, IWebElement element)
        {
            (webdriver as IJavaScriptExecutor).ExecuteScript("arguments[0].setAttribute('style', 'border: red; border: 1px blue solid');", element);
        }

        public static void HorizontalScrollToPosition(IWebDriver webdriver, IWebElement element, int position)
        {
            (webdriver as IJavaScriptExecutor).ExecuteScript("arguments[0].scrollLeft += arguments[1]", element, position);
        }

        public static void ScrollToPosition(IWebDriver webdriver, int x, int y)
        {
            (webdriver as IJavaScriptExecutor).ExecuteScript("window.scroll(arguments[0], arguments[1])", x, y);
        }

        public static void ScrollUntillFound(IWebDriver webdriver, IWebElement element)
        {
            (webdriver as IJavaScriptExecutor).ExecuteScript("arguments[0].scrollIntoView();", element);
        }
        #endregion
        #region Validations
        public static bool IsDisplayed(IWebDriver webdriver, IWebElement element, bool displayed = true)
        {
            bool r = false;
            object result = (webdriver as IJavaScriptExecutor).ExecuteScript("if(parseInt(arguments[0].offsetHeight) > 0 && parseInt(arguments[0].offsetWidth) > 0) return true; return false;", element);
            bool.TryParse(result.ToString(), out r);

            if (displayed == true && r == true || displayed == false && r == false)
                return true;

            return false;
        }

        public static void ValidateAttribute(IWebDriver webdriver, IWebElement element, string attribute, string expectedValue)
        {
            string pattern = string.Format("return arguments[0].getAttribute('{0}');", attribute);
            object attributeValue = (webdriver as IJavaScriptExecutor).ExecuteScript(pattern, element);
            Assert.AreEqual(attributeValue.ToString(), expectedValue, string.Format(@"Actual value '{0}' doesn't match the expected value of '{1}'", element.GetAttribute(attribute).ToString(), expectedValue));
        }
        #endregion
    }
}