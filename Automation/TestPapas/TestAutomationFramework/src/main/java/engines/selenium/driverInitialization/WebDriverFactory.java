package engines.selenium.driverInitialization;

import java.time.Duration;
import java.util.concurrent.TimeUnit;

import common.core.TestInMemoryParameters;
import common.core.configuration.ConfigurationManager;
import engines.selenium.driverInitialization.browsers.Chrome;
import org.openqa.selenium.remote.RemoteWebDriver;
import org.openqa.selenium.support.ui.WebDriverWait;

import static engines.helpers.CommonUtilities.defaultWhenZero;

public class WebDriverFactory {
    // region Fields and Properties
    private static Duration elementTimeout;
    private static Duration pageLoadTimeout;
    public RemoteWebDriver driver;
    public WebDriverWait wait;
    private static WebDriverFactory instance = null;
    // endregion

    // region Methods
    public static WebDriverFactory getThreadSafeInstance() {
        if (instance == null) {
            synchronized (WebDriverFactory.class) {
                if (instance == null) {
                    instance = new WebDriverFactory();
                }
            }
        }
        return instance;
    }

    public void initializeWebDriver(int timeoutInSeconds, int pageLoadTimeout) throws Exception {
        String driverType = ConfigurationManager.getTestConfigValue(
                TestInMemoryParameters.getInstance().getTestConfiguration(), "driverType");

        try {
            defaultWhenZero(timeoutInSeconds, 60);
            defaultWhenZero(pageLoadTimeout, 240);
            System.setProperty("webdriver.chrome.silentOutput","true");

            elementTimeout = Duration.ofSeconds(timeoutInSeconds);
            WebDriverFactory.pageLoadTimeout = Duration.ofSeconds(pageLoadTimeout);

            if (driverType.equals("Chrome")) {
                this.driver = Chrome.initialize();
            } else {
                throw new RuntimeException();
            }

            this.driver.manage().timeouts().pageLoadTimeout(WebDriverFactory.pageLoadTimeout);
            this.driver.manage().timeouts().implicitlyWait(30, TimeUnit.SECONDS);
            TestInMemoryParameters.getInstance().driver = this.driver;
        } catch (Exception exception) {
            throw new Exception(String.format("Unable to initialize WebDriver: %s - %s", driverType, exception.getMessage()));
        }
    }

    public void cleanUp() {
        if (driver != null) {
            driver.quit();
        }
    }
    // endregion
}
