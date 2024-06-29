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
                "--safebrowsing-disable-download-protection",
                "--no-sandbox",
                "--disable-dev-shm-usage",
                "--whitelisted-ips",
                "--ignore-ssl-errors=yes",
                "--ignore-certificate-errors",
                "--disable-gpu");

//        chromeOptions.addArguments("enable-automation");
////        chromeOptions.addArguments("--headless=new");
////        chromeOptions.addArguments("--window-size=1920,1080");
//        chromeOptions.addArguments("--no-sandbox");
//        chromeOptions.addArguments("--disable-extensions");
//        chromeOptions.addArguments("--dns-prefetch-disable");
//        chromeOptions.addArguments("--disable-gpu");
//        chromeOptions.setPageLoadStrategy(PageLoadStrategy.NORMAL);

        WebDriverFactory.getThreadSafeInstance().driver = new ChromeDriver(chromeOptions);

        return WebDriverFactory.getThreadSafeInstance().driver;
    }
}
