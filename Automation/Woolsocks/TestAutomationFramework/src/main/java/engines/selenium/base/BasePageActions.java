package engines.selenium.base;

import common.core.TestInMemoryParameters;
import engines.selenium.driverInitialization.WebDriverFactory;
import engines.helpers.JavaScriptHelper;
import manifold.ext.rt.api.This;
import org.junit.jupiter.api.Assertions;
import org.openqa.selenium.*;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.springframework.util.StopWatch;

import java.lang.reflect.InvocationTargetException;
import java.util.List;
import java.util.concurrent.TimeUnit;

import static common.core.contracts.IStepInfo.duration;
import static engines.extensions.WebElementExtensions.getSelectedDropdownValue;
import static engines.extensions.WebElementExtensions.isAttributePresent;

public class BasePageActions extends BasePage {
    // region Waits
    public <TPage extends BasePage> TPage ImplicitlyWaitForPageToBeReady(Class<TPage> pageClass, int seconds) {
        TPage page;

        TestInMemoryParameters.getInstance().driver.manage().timeouts().implicitlyWait(seconds, TimeUnit.SECONDS);
        try {
//            return pageClass.getDeclaredConstructor(WebDriver.class, WebDriverWait.class)
//                            .newInstance(TestInMemoryParameters.getInstance().driver, wait);
            page = pageClass.newInstance();
        } catch (Exception e) {
            throw new RuntimeException(e);
        }

        return page;
    }

    public void waitForPageToBeReady(By locator, String description, int timeout, boolean isProcessing, boolean shouldPass) throws Exception {
        if (hasBecameVisible(locator, description, isProcessing, timeout)) {
            try {
                if (!isProcessing) {
                    return;
                } else if (hasBecameInvisible(locator, description, timeout)) {
                }
            } catch (Exception e) {
            }
        } else if (!shouldPass) {
            throw new Exception(String.format("%s never showed up", description));
        } else {
        }
    }

    private boolean hasBecameVisible(By locator, String description, boolean isProcessing, int timeout) {
        boolean isVisible = false;
        StopWatch stopWatch = new StopWatch();
        stopWatch.start();

        while (stopWatch.getTotalTimeMillis() < timeout * 1000L) {
            WebElement element = WebDriverFactory.getThreadSafeInstance().driver.findElement(locator);

            if (element.isDisplayed()) {
                isVisible = true;
                break;
            } else {
                if (isProcessing && stopWatch.getTotalTimeMillis() > 3 * 1000L) {
                    isVisible = false;
                    break;
                }
            }
        }

        stopWatch.stop();

        return isVisible;
    }

    private boolean hasBecameInvisible(By locator, String description, int timeout) throws InterruptedException {
        boolean isInvisible = false;
        StopWatch stopWatch = new StopWatch();
        stopWatch.start();
        while (stopWatch.getTotalTimeMillis() < timeout * 1000L) {
            if (WebDriverFactory.getThreadSafeInstance().driver.findElement(locator).isDisplayed()) {
                Thread.sleep(250L);
                continue;
            }

            isInvisible = true;
            break;
        }

        if (!isInvisible) {
        }

        stopWatch.stop();

        return isInvisible;
    }

    // endregion
    // region Getters
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
            return null;
        }
    }

    //
    // region Find
    public static WebElement findElementEx(@This WebDriver webDriver, By locator, int timeoutInSeconds, int pollingIntervalInSeconds) {
        WebDriverWait wait = waitFluently(webDriver, locator, timeoutInSeconds, pollingIntervalInSeconds);

        try {
            return wait.until(x ->
            {
                try {
                    return x.findElement(locator);
                } catch (NoSuchElementException exception) {
                    return null;
                }
            });
        } catch (Exception exception) {
            return null;
        }
    }

    public static List<WebElement> findElementsEx(@This WebDriver webDriver, By locator, int timeoutInSeconds, int pollingIntervalInSeconds) {
        WebDriverWait wait = waitFluently(webDriver, locator, timeoutInSeconds, pollingIntervalInSeconds);

        return wait.until(x -> x.findElements(locator));
    }

    public static WebDriverWait waitFluently(WebDriver webDriver, By locator, int timeoutInSeconds, int pollingIntervalInSeconds) {
        webDriver.manage().timeouts().implicitlyWait(duration.ofSeconds(timeoutInSeconds));
        WebDriverWait wait = new WebDriverWait(webDriver, duration.ofSeconds(timeoutInSeconds));
        wait.pollingEvery(duration.ofSeconds(pollingIntervalInSeconds));
        wait.until(ExpectedConditions.elementToBeClickable(locator));

        wait.ignoring(NoSuchElementException.class)
                .ignoring(UnhandledAlertException.class)
                .ignoring(StaleElementReferenceException.class)
                .ignoring(TimeoutException.class);

        return wait;
    }

    // endregion
    // region Validations
    public static void validateValue(@This WebElement element, String elementName, String expectedValue) {
        String current = element.getText().trim();
        String expected = expectedValue.trim();

        Assertions.assertTrue(current.contains(expected),
                String.format("Failed validating value for: %s - expected value: %s, actual value: %s",
                        elementName, current, expected));
    }

    public static void validateAttribute(@This WebElement element, String attribute, String expectedValue, String elementName, boolean useJavaScript) {
        if (useJavaScript) {
            JavaScriptHelper.validateAttribute(WebDriverFactory.getThreadSafeInstance().driver, element, attribute, expectedValue);
        } else {
            Assertions.assertEquals(expectedValue, element.getAttribute(attribute), String.format("Attribute's: %s value not matching expected value: %s", attribute, expectedValue));
        }
    }

    public static void validateAttributeValue(@This WebElement element, String attribute, String expectedValue) {
        Assertions.assertTrue(element.getAttribute(attribute).contains(expectedValue),
                String.format("Attribute's: %s value: %s does not contain expected value:%s",
                        attribute, element.getAttribute(attribute), expectedValue));
    }
    // endregion
}
