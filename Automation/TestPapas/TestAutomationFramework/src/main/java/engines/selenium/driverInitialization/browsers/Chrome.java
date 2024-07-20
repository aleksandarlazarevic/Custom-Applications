package engines.selenium.driverInitialization.browsers;

import engines.selenium.driverInitialization.BrowserDriver;
import engines.selenium.driverInitialization.WebDriverFactory;
import org.openqa.selenium.PageLoadStrategy;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import org.openqa.selenium.remote.RemoteWebDriver;

import java.nio.file.Path;

public class Chrome implements BrowserDriver {
    public static RemoteWebDriver initialize() {
        ChromeOptions chromeOptions = new ChromeOptions();
        String driverLocation = Path.of("").toAbsolutePath() + "/src/main/java/engines/selenium/driverInitialization/browserDrivers/chrome/chromedriver.exe";
        System.setProperty("webdriver.chrome.driver" ,  driverLocation);

        chromeOptions.addArguments("--disable-popup-blocking",
                "--disable-extensions",
                "--start-maximized",
                "--disable-blink-features=AutomationControlled",
                "--safebrowsing-disable-download-protection",
                "--no-sandbox",
                "--disable-dev-shm-usage",
                "--whitelisted-ips",
                "--ignore-ssl-errors=yes",
                "--ignore-certificate-errors",
                "--disable-gpu");
        chromeOptions.setExperimentalOption("profile.default_content_setting_values.media_stream_mic", 1);
        chromeOptions.setExperimentalOption("profile.default_content_setting_values.media_stream_camera", 1);
        chromeOptions.setExperimentalOption("profile.default_content_setting_values.geolocation", 0);
        chromeOptions.setExperimentalOption("profile.default_content_setting_values.notifications", 1);

        WebDriverFactory.getThreadSafeInstance().driver = new ChromeDriver(chromeOptions);

        return WebDriverFactory.getThreadSafeInstance().driver;
    }
}
