package testSuites.shared.steps;

import common.core.TestInMemoryParameters;
import common.utilities.LoggingManager;
import engines.selenium.driverInitialization.WebDriverFactory;
import org.openqa.selenium.Alert;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.remote.RemoteWebDriver;

public class CommonBrowserActions {
    public static void goToDefaultUrl() {
        try {
            LoggingManager.Info("Navigating to: " + TestInMemoryParameters.getInstance().url);
            WebDriverFactory.getThreadSafeInstance().driver.get(TestInMemoryParameters.getInstance().url);
        } catch (Exception exception) {
            LoggingManager.Error("Failed to start the browser: " + exception.getMessage());
        }
    }

    public static void closeBrowser() {
        try {
            LoggingManager.Info("Shutting down browser instance");
            WebDriverFactory.getThreadSafeInstance().driver.quit();
        } catch (Exception exception) {
            LoggingManager.Error("Failed shutting down browser instance");
        }
    }

    public void openNewTab() {
        RemoteWebDriver driver = WebDriverFactory.getThreadSafeInstance().driver;
        ((JavascriptExecutor) driver).executeScript("window.open();");
        driver.switchTo().window(driver.getWindowHandle());
    }

    public void closeCurrentTab() {
        RemoteWebDriver driver = WebDriverFactory.getThreadSafeInstance().driver;
        driver.switchTo().window(driver.getWindowHandles().stream().findFirst().toString());
        driver.close();
        driver.switchTo().window(driver.getWindowHandles().stream().findFirst().toString());
    }

    public void acceptAlertPopUp() {
        try {
            Thread.sleep(5000);
            Alert alert = WebDriverFactory.getThreadSafeInstance().driver.switchTo().alert();
            Thread.sleep(500);
            alert.accept();
            Thread.sleep(3000);
        } catch (Exception exception) {
            throw new RuntimeException(String.format("Failed accepting alert pop-up: %s", exception.getMessage()));
        }
    }
}
