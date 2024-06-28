package engines.extensions;

import common.core.TestInMemoryParameters;
import common.utilities.LoggingManager;
import engines.helpers.JavaScriptHelper;
import manifold.ext.rt.api.Extension;
import manifold.ext.rt.api.This;
import org.junit.jupiter.api.Assertions;
import org.openqa.selenium.*;
import org.openqa.selenium.interactions.Actions;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.Wait;
import org.openqa.selenium.support.ui.WebDriverWait;

import java.awt.*;
import java.awt.datatransfer.StringSelection;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.function.Function;

import static common.core.contracts.IStepInfo.duration;
import static engines.selenium.base.BasePageActions.*;

@Extension
public class WebElementExtensions {
    // region Waits
    public static void waitToBeClickable(@This WebElement element, String elementName, int timeoutInSeconds) {
        WebDriverWait wait = new WebDriverWait(TestInMemoryParameters.getInstance().driver, duration.ofSeconds(timeoutInSeconds));
        LoggingManager.Info("Waiting for element to appear: " + elementName);
        wait.until(ExpectedConditions.elementToBeClickable(element));
    }

    public static void waitToBeDisplayed(By locator, String elementName, int timeoutInSeconds) {
        WebDriverWait wait = new WebDriverWait(TestInMemoryParameters.getInstance().driver, duration.ofSeconds(timeoutInSeconds));
        wait.until(ExpectedConditions.visibilityOfElementLocated(locator));
    }

    public static void waitToBeDisplayed(@This WebElement element, String elementName, int timeoutInSeconds) {
        WebDriverWait wait = new WebDriverWait(TestInMemoryParameters.getInstance().driver, duration.ofSeconds(timeoutInSeconds));
        wait.until(ExpectedConditions.visibilityOf(element));
    }

    public static void waitToDisapear(By locator, String elementName, int timeoutInSeconds) {
        WebDriverWait wait = new WebDriverWait(TestInMemoryParameters.getInstance().driver, duration.ofSeconds(timeoutInSeconds));
        wait.until(ExpectedConditions.invisibilityOfElementLocated(locator));
    }

    public static void waitToDisapear(@This WebElement element, String elementName, int numberOfRetries) throws Exception {
        int retry = 0;
        while (isDisplayedEx(element, elementName, false, true, 3) && retry < numberOfRetries) {
            Thread.sleep(5000);
            retry++;
        }

        if (retry == numberOfRetries) {
            throw new Exception(String.format("[{0}] is still visible", elementName));
        } else {
        }
    }

    public static void waitUntil(@This WebElement webElement, Function<WebElement, Object> condition, boolean ignoreExceptions, int timeoutInSeconds, int pollingIntervalInSeconds) {
        TestInMemoryParameters.getInstance().driver.manage().timeouts().implicitlyWait(duration.ofSeconds(timeoutInSeconds));
        Wait<WebDriver> wait = new WebDriverWait(TestInMemoryParameters.getInstance().driver, duration.ofSeconds(timeoutInSeconds));

        try {
            wait.until($ -> condition.apply(webElement));
        } catch (Exception exception) {
            if (!ignoreExceptions) {
                throw new RuntimeException("Wait condition has not been met");
            }
        }
    }

    public static void waitForProcessing(@This WebDriver webDriver, By locator, int timeoutInSeconds, int pollingIntervalInSeconds) {
        WebDriverWait wait = waitFluently(webDriver, locator, timeoutInSeconds, pollingIntervalInSeconds);
        AtomicInteger retry = new AtomicInteger();
        Function<WebDriver, Boolean> processFunc = (x) ->
        {
            WebElement element = findElementEx(x, locator, 5, 1);

            if (element == null) {
                return retry.getAndIncrement() == 2;
            }

            return ((element == null) || (element != null && element.getCssValue("display").equals("none")));
        };

        if (isDisplayed(findElementEx(webDriver, locator, 5, 1), 5)) {
            wait.until(processFunc);
        }
    }

    public static void waitForProcessing(@This WebElement element, int timeoutInSeconds) {
        TestInMemoryParameters.getInstance().driver.manage().timeouts().implicitlyWait(duration.ofSeconds(timeoutInSeconds));
        Wait<WebDriver> wait = new WebDriverWait(TestInMemoryParameters.getInstance().driver, duration.ofSeconds(timeoutInSeconds));

        Function<WebDriver, Boolean> processFunc = $ -> {
            return !isDisplayed(element, 5);
        };


        if (isDisplayed(element, 15)) {
            wait.until(processFunc);
        }
    }

    // endregion
    // region Checks
    public static boolean isClickable(@This WebElement element, int timeoutInSeconds) {
        TestInMemoryParameters.getInstance().driver.manage().timeouts().implicitlyWait(duration.ofSeconds(timeoutInSeconds));
        Wait<WebDriver> wait = new WebDriverWait(TestInMemoryParameters.getInstance().driver, duration.ofSeconds(timeoutInSeconds));

        try {
            wait.until(ExpectedConditions.elementToBeClickable(element));
            return true;
        } catch (Exception exception) {
            LoggingManager.Info("The element is not clickable: " + exception.getMessage());
            return false;
        }
    }

    public static boolean isDisplayed(@This WebElement element, int timeoutInSeconds) {
        TestInMemoryParameters.getInstance().driver.manage().timeouts().implicitlyWait(duration.ofSeconds(timeoutInSeconds));
        Wait<WebDriver> wait = new WebDriverWait(TestInMemoryParameters.getInstance().driver, duration.ofSeconds(timeoutInSeconds));

        try {
            return wait.until($ -> {
                return element != null && element.isDisplayed();
            });
        } catch (Exception exception) {
            LoggingManager.Info("The element is not displayed: " + exception.getMessage());
            return false;
        }
    }

    public static boolean isDisplayedEx(@This WebElement element, String elementName, boolean useJavaScript, boolean displayed, int timeoutInSeconds) {
        boolean result = false;
        try {
            TestInMemoryParameters.getInstance().driver.manage().timeouts().implicitlyWait(duration.ofSeconds(timeoutInSeconds));

            if (useJavaScript) {
                result = JavaScriptHelper.isDisplayed(TestInMemoryParameters.getInstance().driver, element, displayed);
            } else {
                result = isDisplayed(element, timeoutInSeconds);
            }

            if (result) {
            } else {
            }

            return result;
        } catch (Exception exception) {
            LoggingManager.Info(String.format("The element is not displayed: {0} - exception: {1}",
                    elementName, exception.getMessage()));
            return false;
        }
    }

    public static boolean isEnabled(@This WebElement element, String elementName, int timeoutInSeconds) {
        TestInMemoryParameters.getInstance().driver.manage().timeouts().implicitlyWait(duration.ofSeconds(timeoutInSeconds));
        Wait<WebDriver> wait = new WebDriverWait(TestInMemoryParameters.getInstance().driver, duration.ofSeconds(timeoutInSeconds));

        try {
            return wait.until($ -> {
                return element != null && element.isEnabled();
            });
        } catch (Exception exception) {
            LoggingManager.Info(String.format("The element: {0} is not enabled: ",
                    elementName, exception.getMessage()));
            return false;
        }
    }

    public static boolean doesElementExist(By locator, int timeoutInSeconds, int pollingIntervalInSeconds) {
        WebDriverWait wait = waitFluently(TestInMemoryParameters.getInstance().driver, locator, timeoutInSeconds, pollingIntervalInSeconds);

        try {
            wait.until(ExpectedConditions.presenceOfElementLocated(locator));
            return true;
        } catch (Exception exception) {
            LoggingManager.Info("The element does not exist: " + exception.getMessage());
            return false;
        }
    }

    public static boolean isAttributePresent(@This WebElement element, String attribute) {
        boolean result = false;
        try {
            String value = element.getAttribute(attribute);
            if (value != null) {
                result = true;
            }
        } catch (Exception exception) {
            LoggingManager.Info(String.format("Unable to locate element attribute: {0} - exception: {1}",
                    attribute, exception.getMessage()));
            result = false;
        }

        return result;
    }

    // endregion
    // region Actions
    public static void clickWithCoordinates(@This WebElement element, String elementName, int x, int y) {
        try {
            Actions builder = new Actions(TestInMemoryParameters.getInstance().driver);
            JavaScriptHelper.highlightElement(TestInMemoryParameters.getInstance().driver, element);
            builder.moveToElement(element).moveByOffset(x, y).click().build().perform();
        } catch (Exception exception) {
            LoggingManager.Error(String.format("Failed clicking element: {0} with coordinates: {1}:{2} - exception: {3}",
                    elementName, x, y, exception.getMessage()));
            throw new RuntimeException(exception);
        }
    }

    public static void selectDropdownValue(@This WebElement element, String value, String elementName, boolean useJavaScript, boolean validateElementValue, boolean partialMatch) {
        waitToBeClickable(element, elementName, 60);

        try {
            if (useJavaScript) {
                JavaScriptHelper.highlightElement(TestInMemoryParameters.getInstance().driver, element);
                JavaScriptHelper.selectDropdownValue(TestInMemoryParameters.getInstance().driver, element, value, elementName);
            } else {
                Select dropdown = new Select(element);
                JavaScriptHelper.highlightElement(TestInMemoryParameters.getInstance().driver, element);

                WebElement option = null;

                if (partialMatch) {
                    option = dropdown.getOptions().stream().filter(x -> x.getText().trim().contains(value)).findAny().orElse(null);
                } else {
                    option = dropdown.getOptions().stream().filter(x -> x.getText().trim() == value).findAny().orElse(null);
                }

                if (option == null) {
                    throw new NoSuchElementException(String.format("Element:%s has no option:%s", elementName, value));
                }

                if (!option.isEnabled()) {
                    throw new Exception(String.format("Element:%s option is disabled:%s, not able to select", elementName, value));
                }

                dropdown.selectByVisibleText(value);

                if (validateElementValue) {
                    Assertions.assertEquals(value, dropdown.getFirstSelectedOption().getText().trim(),
                            "Selected option in dropdown is not correct");
                }
            }
        } catch (Exception exception) {
            LoggingManager.Error(String.format("Failed to select value: {0} from: {1} - exception: ",
                    value, elementName, exception.getMessage()));
            throw new RuntimeException(exception);
        }
    }

    public static void selectDropdownValueByIndex(@This WebElement element, int index, String elementName, boolean useJavaScript) {
        if (index == 0) {
            return;
        }

        waitToBeClickable(element, elementName, 60);

        try {
            if (useJavaScript) {
                JavaScriptHelper.highlightElement(TestInMemoryParameters.getInstance().driver, element);
                JavaScriptHelper.selectDropdownValueByIndex(TestInMemoryParameters.getInstance().driver, element, index, elementName);
            } else {
                Select dropdown = new Select(element);
                JavaScriptHelper.highlightElement(TestInMemoryParameters.getInstance().driver, element);
                dropdown.selectByIndex(index);
            }
        } catch (Exception exception) {
            LoggingManager.Error(String.format("Failed to select dropdown value by index: {0} - exception: {1}",
                    elementName, exception.getMessage()));
            throw new RuntimeException(exception);
        }
    }

    public static String getSelectedDropdownValue(@This WebElement element, String elementName) {
        try {
            JavaScriptHelper.highlightElement(TestInMemoryParameters.getInstance().driver, element);
            Select selectedValue = new Select(element);
            String value = selectedValue.getFirstSelectedOption().getText().trim();
            return value;
        } catch (Exception exception) {
            LoggingManager.Error(String.format("Failed to get selected dropdown value for: {0} - exception: {1}",
                    elementName, exception.getMessage()));
            throw new RuntimeException(exception);
        }
    }

    public String getValueEx(@This WebElement element) {
        try {
            String value = null;

            if (element.getTagName() == "input") {
                String type = element.getAttribute("type");

                if (type == "text") {
                    value = element.getAttribute("value").trim();
                } else if (type == "radio" || type == "checkbox") {
                    value = element.getAttribute("checked") == null ? "false" : "true";
                } else {
                    value = element.getAttribute("value").trim();
                }
            } else if (element.getTagName() == "select") {
                value = getSelectedDropdownValue(element, "DropDown");
            } else if (element.getTagName().contains("button") && (isAttributePresent(element, "selected") || isAttributePresent(element, "aria-selected"))) {
                if (isAttributePresent(element, "selected")) {
                    value = element.getAttribute("selected") == null ? "false" : "true";
                } else if (isAttributePresent(element, "aria-selected")) {
                    value = element.getAttribute("aria-selected") == null ? "false" : "true";
                }
            } else {
                value = element.getAttribute("innerText").trim();
            }

            return value;
        } catch (Exception exception) {
            LoggingManager.Error("Unable to get element value: " + exception.getMessage());
            throw new RuntimeException(exception);
        }
    }

    public void setValueEx(@This WebElement element, String elementName, String value, boolean validateElementValue, boolean useJavaScript) {
        try {
            if (element.getTagName() == "input") {
                String type = element.getAttribute("type");

                if (type == "text") {
                    clearAndSendKeys(element, value, elementName);
                } else if (type == "radio") {
                    clickEx(element, elementName, false);
                } else if (type == "checkbox") {
                    boolean iValue = Boolean.valueOf(value);
                    if (!iValue) {
                        throw new Exception("Not able to parse checkbox value");
                    }

                    tickCheckboxEx(element, value, elementName);
                } else {
                    throw new Exception("Unsupported input control");
                }
            } else if (element.getTagName() == "select") {
                selectDropdownValue(element, value, elementName, useJavaScript, validateElementValue, false);
            } else {
                throw new Exception("Unsupported input control");
            }
        } catch (Exception exception) {
            LoggingManager.Error("Unable to set the element's: " + elementName + " value: " + exception.getMessage());
            throw new RuntimeException(exception);
        }
    }

    public static void clearEx(@This WebElement element, String elementName, boolean useJavaScript) {
        if (useJavaScript) {
            JavaScriptHelper.clear(TestInMemoryParameters.getInstance().driver, element, elementName);
        } else {
            element.clear();
        }

        Assertions.assertEquals(element.getText(), "", String.format("Element {0} not cleared", elementName));
    }

    public static void clickEx(@This WebElement element, String elementName, boolean useJavaScript) {
        int retry = 1;
        int maxRetry = 3;
        boolean isClicked = false;

        while (!isClicked && retry <= maxRetry) {
            try {
                waitToBeClickable(element, elementName, 60);

                if (useJavaScript) {
                    JavaScriptHelper.highlightElement(TestInMemoryParameters.getInstance().driver, element);
                    JavaScriptHelper.click(TestInMemoryParameters.getInstance().driver, element, elementName);
                } else {
                    JavaScriptHelper.highlightElement(TestInMemoryParameters.getInstance().driver, element);
                    element.click();
                }

                isClicked = true;
            } catch (Exception exception) {
                if (retry == maxRetry) {
                    LoggingManager.Error(String.format("Failed to click element: {0} - exception: {1}",
                            elementName, exception.getMessage()));
                    throw new RuntimeException(exception);
                }

                retry++;
            }
        }
    }

    public static void sendKeysEx(@This WebElement element, String value, String elementName, boolean useJavaScript, boolean validateElementValue) {
        try {
            LoggingManager.Info("Filling element: " + elementName + " with data: " + value.toString());
            waitToBeClickable(element, elementName, 15);

            if (useJavaScript) {
                JavaScriptHelper.highlightElement(TestInMemoryParameters.getInstance().driver, element);
                JavaScriptHelper.setValue(TestInMemoryParameters.getInstance().driver, element, value, elementName);
            } else {
                element.sendKeys(value);
            }

            if (validateElementValue) {
                Assertions.assertEquals(value, element.getAttribute("value"), String.format("Failed setting value to: {0}", elementName));
            }
        } catch (Exception exception) {
            LoggingManager.Error(String.format("Failed filling element: {0} with data: {1} - exception {2}",
                    elementName, value.toString(), exception.getMessage()));
            throw new RuntimeException(exception);
        }
    }

    public static void clearAndSendKeys(@This WebElement element, String value, String elementName) {
        element.clear();
        sendKeysEx(element, value, elementName, true, true);
        loseFocus();
    }

    public static void loseFocus() {
        try {
            TestInMemoryParameters.getInstance().driver.findElement(By.tagName("body")).sendKeys(Keys.chord(Keys.TAB));
        } catch (Exception exception) {

            throw new RuntimeException(exception);
        }
    }

    public static void pasteValueEx(@This WebElement element, String value, String elementName, boolean validateElementValue) {
        JavaScriptHelper.highlightElement(TestInMemoryParameters.getInstance().driver, element);
        Toolkit.getDefaultToolkit()
                .getSystemClipboard()
                .setContents(
                        new StringSelection(value),
                        null
                );

        element.sendKeys(Keys.chord(Keys.CONTROL, "v"));
        Assertions.assertEquals(value, element.getAttribute("value"),
                String.format("Failed pasting value {0} to element {1}", value, elementName));

        if (validateElementValue) {
            validateAttribute(element, "value", value, elementName, false);
        }
    }

    public static void tickCheckboxEx(@This WebElement checkbox, String value, String elementName) {
        waitToBeClickable(checkbox, elementName, 60);

        boolean isChecked = checkbox.getAttribute("checked") == "true";
        boolean shouldBeChecked = Boolean.valueOf(value.toLowerCase());

        try {
            if ((!isChecked && shouldBeChecked) ||
                    (isChecked && !shouldBeChecked)) {
                checkbox.click();
                JavaScriptHelper.highlightElement(TestInMemoryParameters.getInstance().driver, checkbox);
            }
        } catch (Exception exception) {
            LoggingManager.Error(String.format("Failed to tick checkbox: {0} - exception: {1}",
                    elementName, exception.getMessage()));
            throw new RuntimeException(exception);
        }
    }

    public static void navigateToUrl(@This WebDriver webDriver, String url) {
        try {
            TestInMemoryParameters.getInstance().driver.navigate().to(url);
        } catch (Exception exception) {
            LoggingManager.Error(String.format("Failed to navigate to URL: {0} - exception: {1}",
                    url, exception.getMessage()));
            throw new RuntimeException(exception);
        }
    }

    // region Mouse actions
    public static void mouseClickEx(@This WebElement element, String elementName) {
        waitToBeClickable(element, elementName, 60);

        try {
            Actions builder = new Actions(TestInMemoryParameters.getInstance().driver);
            JavaScriptHelper.highlightElement(TestInMemoryParameters.getInstance().driver, element);
            builder.moveToElement(element).click().perform();
        } catch (Exception exception) {
            LoggingManager.Error(String.format("Failed to click element: {0} - exception: {1}",
                    elementName, exception.getMessage()));
            throw new RuntimeException(exception);
        }
    }
    // endregion
    // endregion
}
