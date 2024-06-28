package engines.selenium.driverInitialization;

import java.time.Duration;

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

    public void initializeWebDriver() throws Exception {
        try {
            initializeWebDriver(0, 0);
        } catch (Exception exception) {
            throw new Exception("Unable to initialize WebDriver: %s" + exception.getMessage());
        }
    }

    public void initializeWebDriver(int timeoutInSeconds, int pageLoadTimeout) throws Exception {
        try {
            defaultWhenZero(timeoutInSeconds, 60);
            defaultWhenZero(pageLoadTimeout, 240);

            elementTimeout = Duration.ofSeconds(timeoutInSeconds);
            WebDriverFactory.pageLoadTimeout = Duration.ofSeconds(pageLoadTimeout);

            this.driver = Chrome.initialize();

            this.driver.manage().timeouts().pageLoadTimeout(WebDriverFactory.pageLoadTimeout);
            this.driver.manage().timeouts().implicitlyWait(elementTimeout);

            this.driver.manage().window().maximize();
        } catch (Exception exception) {
            throw new Exception("Unable to initialize WebDriver: %s" + exception.getMessage());
        }
    }

    public void cleanUp() {
        if (driver != null) {
            driver.quit();
        }
    }
    // endregion
}
