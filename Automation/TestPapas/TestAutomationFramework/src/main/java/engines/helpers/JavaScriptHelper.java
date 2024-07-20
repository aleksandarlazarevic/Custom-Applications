package engines.helpers;

import engines.selenium.driverInitialization.WebDriverFactory;
import org.junit.jupiter.api.Assertions;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class JavaScriptHelper {
    // region Actions
    public static Object executeJSCode(WebDriver webdriver, String jsCode) {
        return ((JavascriptExecutor) webdriver).executeScript(jsCode);
    }

    public static void click(WebDriver webdriver, WebElement element, String elementName) {
        ((JavascriptExecutor) webdriver).executeScript("arguments[0].click();", element);
    }

    public static void clear(WebDriver webdriver, WebElement element, String elementName) {
        ((JavascriptExecutor) webdriver).executeScript("arguments[0].value = '';", element);
    }

    public static void setValue(WebDriver webdriver, WebElement element, String value, String elementName) {
        String pattern = String.format("arguments[0].value = '{0}';", value);
        ((JavascriptExecutor) webdriver).executeScript(pattern, element);
    }

    public static void validateDropdownValue(WebDriver webdriver, WebElement element, String value, String elementName) {
        Object result = ((JavascriptExecutor) webdriver).executeScript("return arguments[0].options[arguments[0].selectedIndex].text", element);
        Assertions.assertEquals(result.toString(), value, String.format("Failed to select [{0}] dropdown", elementName));
    }

    public static void selectDropdownValue(WebDriver webdriver, WebElement element, String value, String elementName) {
        String pattern = String.format("var length = arguments[0].options.length;  for (var i=0; i<length; i++){{  if (arguments[0].options[i].text == '{0}'){{ arguments[0].selectedIndex = i; break; }} }}", value);
        Object result = ((JavascriptExecutor) webdriver).executeScript(pattern, element);
        validateDropdownValue(webdriver, element, value, elementName);
    }

    public static void selectDropdownValueByIndex(WebDriver webdriver, WebElement element, int index, String elementName) {
        String pattern = String.format("var length = arguments[0].options.length;  for (var i=1; i<length; i++){{  if (arguments[0].options[i].index == '{0}'){{ arguments[0].selectedIndex = i; break; }} }}", index);
        Object result = ((JavascriptExecutor) webdriver).executeScript(pattern, element);
    }

    public static void tickCheckBox(WebDriver webdriver, WebElement element, boolean value) {
        String statement = String.format("if((arguments[0].checked == true && '{0}' == 'false') || (arguments[0].checked == false && '{0}' == 'true')) arguments[0].click(); ", String.valueOf(value).toLowerCase());
        Object result = ((JavascriptExecutor) webdriver).executeScript(statement, element);
    }

    public static void refreshPage(WebDriver webdriver) {
        ((JavascriptExecutor) webdriver).executeScript("location.reload();");
    }

    public static String getText(WebDriver webdriver, WebElement element) {
        return ((JavascriptExecutor) webdriver).executeScript("return arguments[0].innerText", element).toString();
    }

    public static String getSelectedDropdownValue(WebDriver webdriver, WebElement element, String elementName) {
        return ((JavascriptExecutor) webdriver).executeScript("return arguments[0].options[arguments[0].selectedIndex].text", element).toString();
    }

    // region Mouse actions
    public static void mouseOver(WebDriver webdriver, WebElement element) {
        Object result = ((JavascriptExecutor) webdriver).executeScript("var evObj = document.createEvent('MouseEvents'); " +
                "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                "arguments[0].dispatchEvent(evObj);", element);
    }

    public static void mouseOut(WebDriver webdriver, WebElement element) {
        Object result = ((JavascriptExecutor) webdriver).executeScript("var evObj = document.createEvent('MouseEvents'); " +
                "evObj.initMouseEvent(\"mouseout\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                "arguments[0].dispatchEvent(evObj);", element);
    }

    public static void scrollIntoView(WebElement element, String option) {
        ((JavascriptExecutor) WebDriverFactory.getThreadSafeInstance().driver).executeScript("arguments[0].scrollIntoView({option});", element);
    }

    // endregion
    public static void highlightElement(WebDriver webdriver, WebElement element) {
        ((JavascriptExecutor) webdriver).executeScript("arguments[0].setAttribute('style', 'border: red; border: 1px blue solid');", element);
    }

    public static void horizontalScrollToPosition(WebDriver webdriver, WebElement element, int position) {
        ((JavascriptExecutor) webdriver).executeScript("arguments[0].scrollLeft += arguments[1]", element, position);
    }

    public static void scrollToPosition(WebDriver webdriver, int x, int y) {
        ((JavascriptExecutor) webdriver).executeScript("window.scroll(arguments[0], arguments[1])", x, y);
    }

    public static void scrollUntillFound(WebDriver webdriver, WebElement element) {
        ((JavascriptExecutor) webdriver).executeScript("arguments[0].scrollIntoView();", element);
    }

    // endregion
    // region Validations
    public static boolean isDisplayed(WebDriver webdriver, WebElement element, boolean displayed) {
        boolean r = false;
        Object result = ((JavascriptExecutor) webdriver).executeScript("if(parseInt(arguments[0].offsetHeight) > 0 && parseInt(arguments[0].offsetWidth) > 0) return true; return false;", element);
        r = Boolean.valueOf(result.toString());

        if (displayed == true && r == true || displayed == false && r == false)
            return true;

        return false;
    }

    public static void validateAttribute(WebDriver webdriver, WebElement element, String attribute, String expectedValue) {
        String pattern = String.format("return arguments[0].getAttribute('{0}');", attribute);
        Object attributeValue = ((JavascriptExecutor) webdriver).executeScript(pattern, element);
        Assertions.assertEquals(attributeValue.toString(), expectedValue, String.format("Actual value '{0}' doesn't match the expected value of '{1}'", element.getAttribute(attribute).toString(), expectedValue));
    }
    // endregion
}
