package engines.selenium.driverInitialization.browsers;

import engines.selenium.driverInitialization.BrowserDriver;
import engines.selenium.driverInitialization.WebDriverFactory;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import org.openqa.selenium.remote.RemoteWebDriver;

import java.nio.file.Path;

public class Chrome implements BrowserDriver {
    public static RemoteWebDriver initialize() {
        ChromeOptions chromeOptions = new ChromeOptions();
        String driverLocation = Path.of("").toAbsolutePath() + "/src/main/java/Engines/Selenium/DriverInitialization/BrowserDrivers/Chrome/chromedriver";
        System.setProperty("webdriver.chrome.driver" ,  driverLocation);

        chromeOptions.addArguments("--disable-popup-blocking",
                "--disable-extensions",
                "--start-maximized",
                "--safebrowsing-disable-download-protection",
                "--no-sandbox",
                "--disable-dev-shm-usage",
                "--whitelisted-ips",
                "--ignore-ssl-errors=yes",
                "--ignore-certificate-errors",
                "--disable-gpu");
        WebDriverFactory.getThreadSafeInstance().driver = new ChromeDriver(chromeOptions);

        return WebDriverFactory.getThreadSafeInstance().driver;
    }
}
