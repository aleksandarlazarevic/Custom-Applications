package engines.selenium.driverInitialization.browsers;

import engines.selenium.driverInitialization.BrowserDriver;
import engines.selenium.driverInitialization.WebDriverFactory;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.firefox.FirefoxOptions;
import org.openqa.selenium.remote.RemoteWebDriver;

import java.nio.file.Path;

public class Firefox implements BrowserDriver {
    public static RemoteWebDriver initialize() {
        FirefoxOptions firefoxOptions = new FirefoxOptions();
        String driverLocation = Path.of("").toAbsolutePath() + "/src/main/java/engines/selenium/driverInitialization/browserDrivers/firefox/geckodriver.exe";
        firefoxOptions.setBinary(driverLocation);

        WebDriverFactory.getThreadSafeInstance().driver = new FirefoxDriver(firefoxOptions);

        return WebDriverFactory.getThreadSafeInstance().driver;
    }
}
