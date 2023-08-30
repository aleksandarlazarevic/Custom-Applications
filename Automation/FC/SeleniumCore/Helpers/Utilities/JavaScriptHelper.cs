using System;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumCore.WebDriver;

namespace SeleniumCore.Helpers.Utilities
{
    public static class JavaScriptHelper
    {
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
            string pattern = String.Format("arguments[0].value = '{0}';", value);
            (webdriver as IJavaScriptExecutor).ExecuteScript(pattern, element);
        }

        public static void ValidateComboboxValue(IWebDriver webdriver, IWebElement element, string value, string elementName)
        {
            Object result = (webdriver as IJavaScriptExecutor).ExecuteScript("return arguments[0].options[arguments[0].selectedIndex].text", element);
            Assert.AreEqual(result.ToString(), value, String.Format("Selected combobox [{0}] is not correct", elementName));
        }

        public static void SelectComboboxValue(IWebDriver webdriver, IWebElement element, string value, string elementName)
        {
            string pattern = String.Format("var length = arguments[0].options.length;  for (var i=0; i<length; i++){{  if (arguments[0].options[i].text == '{0}'){{ arguments[0].selectedIndex = i; break; }} }}", value);
            object result = (webdriver as IJavaScriptExecutor).ExecuteScript(pattern, element);
            ValidateComboboxValue(webdriver, element, value, elementName);
        }

        public static void SelectComboboxValueByIndex(IWebDriver webdriver, IWebElement element, int index, string elementName)
        {
            string pattern = String.Format("var length = arguments[0].options.length;  for (var i=1; i<length; i++){{  if (arguments[0].options[i].index == '{0}'){{ arguments[0].selectedIndex = i; break; }} }}", index);
            object result = (webdriver as IJavaScriptExecutor).ExecuteScript(pattern, element);
        }

        public static void CheckBox(IWebDriver webdriver, IWebElement element, bool value)
        {
            string statement = string.Format("if((arguments[0].checked == true && '{0}' == 'false') || (arguments[0].checked == false && '{0}' == 'true')) arguments[0].click(); ", value.ToString().ToLower());
            Object result = (webdriver as IJavaScriptExecutor).ExecuteScript(statement, element);
        }

        public static bool IsDisplayed(IWebDriver webdriver, IWebElement element, bool displayed = true)
        {
            bool r = false;
            Object result = (webdriver as IJavaScriptExecutor).ExecuteScript("if(parseInt(arguments[0].offsetHeight) > 0 && parseInt(arguments[0].offsetWidth) > 0) return true; return false;", element);
            bool.TryParse(result.ToString(), out r);

            if (displayed == true && r == true || displayed == false && r == false)
                return true;

            return false;
        }

        public static void ValidateElementAttribute(IWebDriver webdriver, IWebElement element, string attribute, string expectedValue)
        {
            string pattern = String.Format("return arguments[0].getAttribute('{0}');", attribute);
            Object attributeValue = (webdriver as IJavaScriptExecutor).ExecuteScript(pattern, element);
            Assert.AreEqual(attributeValue.ToString(), expectedValue, String.Format(@"Actual value '{0}' doesn't match the expected value of '{1}'", element.GetAttribute(attribute).ToString(), expectedValue));
        }

        public static void RefreshPage(IWebDriver webdriver)
        {
            (webdriver as IJavaScriptExecutor).ExecuteScript("location.reload();");
        }

        public static string GetElementInnerText(IWebDriver webdriver, IWebElement element)
        {
            return (webdriver as IJavaScriptExecutor).ExecuteScript("return arguments[0].innerText", element).ToString();
        }

        public static string GetSelectedComboboxValue(IWebDriver webdriver, IWebElement element, string elementName)
        {
            return (webdriver as IJavaScriptExecutor).ExecuteScript("return arguments[0].options[arguments[0].selectedIndex].text", element).ToString();
        }

        public static void ExecuteMouseOver(IWebDriver webdriver, IWebElement element)
        {
            Object result = (webdriver as IJavaScriptExecutor).ExecuteScript("var evObj = document.createEvent('MouseEvents'); " +
                "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                "arguments[0].dispatchEvent(evObj);", element);
        }

        public static void ExecuteMouseOut(IWebDriver webdriver, IWebElement element)
        {
            Object result = (webdriver as IJavaScriptExecutor).ExecuteScript("var evObj = document.createEvent('MouseEvents'); " +
                "evObj.initMouseEvent(\"mouseout\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                "arguments[0].dispatchEvent(evObj);", element);
        }

        public static void MoveScrollIntoView(IWebElement element, string option = "true")
        {
            (UIDriver.WebDriver as IJavaScriptExecutor).ExecuteScript($"arguments[0].scrollIntoView({option});", element);
        }

        public static void HighlightElement(IWebDriver webdriver, IWebElement element)
        {
            (webdriver as IJavaScriptExecutor).ExecuteScript("arguments[0].setAttribute('style', 'border: red; border: 1px blue solid');", element);
        }

        public static void MoveHorizontalScroll(IWebDriver webdriver, IWebElement element, int position)
        {
            (webdriver as IJavaScriptExecutor).ExecuteScript("arguments[0].scrollLeft += arguments[1]", element, position);
        }

        public static void MoveScrollOnPosition(IWebDriver webdriver, int x, int y)
        {
            (webdriver as IJavaScriptExecutor).ExecuteScript("window.scroll(arguments[0], arguments[1])", x, y);
        }

        public static void MoveScrollTillElementIsFound(IWebDriver webdriver, IWebElement element)
        {
            (webdriver as IJavaScriptExecutor).ExecuteScript("arguments[0].scrollIntoView();", element);
        }
    }
}