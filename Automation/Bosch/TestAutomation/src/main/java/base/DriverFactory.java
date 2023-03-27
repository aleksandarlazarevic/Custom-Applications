package base;

import io.appium.java_client.AppiumDriver;
import io.appium.java_client.MobileElement;
import io.appium.java_client.service.local.AppiumDriverLocalService;
import io.appium.java_client.service.local.AppiumServerHasNotBeenStartedLocallyException;
import io.appium.java_client.service.local.AppiumServiceBuilder;
import org.openqa.selenium.remote.DesiredCapabilities;

import java.io.File;
import java.net.MalformedURLException;
import java.nio.file.Path;
import java.nio.file.Paths;

public class DriverFactory {
    private static DriverFactory instance = null;
    public AppiumDriver<MobileElement> driver;
    private AppiumDriverLocalService appiumDriverLocalService;

    public static DriverFactory getInstance() {
        if (instance == null) {
            instance = new DriverFactory();
        }

        return instance;
    }

    private DriverFactory() {
    }

    public void initializeAppiumDriver() throws MalformedURLException {
        try {
            String appPath = Paths.get("src","test","resources").toFile().getAbsolutePath();
            String platformName = ConfigurationReader.getValue("platformName");
            String deviceName = ConfigurationReader.getValue("deviceName");
            String appiumJs = ConfigurationReader.getValue("appiumJs");
            String nodeJs = ConfigurationReader.getValue("nodeJs");
            String ipAddress = ConfigurationReader.getValue("ipAddress");

            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.setCapability("deviceName", deviceName);
            capabilities.setCapability("platformName", platformName);
            capabilities.setCapability("app", appPath + "\\app-debug.apk");

            appiumDriverLocalService = AppiumDriverLocalService
                    .buildService(new AppiumServiceBuilder()
                            .withIPAddress(ipAddress)
                            .usingAnyFreePort()
                            .usingDriverExecutable(new File(nodeJs))
                            .withAppiumJS(new File(appiumJs)));
            appiumDriverLocalService.start();

            if (appiumDriverLocalService == null) {
                throw new AppiumServerHasNotBeenStartedLocallyException("Appium driver local service has not started");
            }

            driver = new AppiumDriver<MobileElement>(appiumDriverLocalService.getUrl(), capabilities);
        } catch (Exception ex) {
            throw new RuntimeException("Unable to initialize Appium");
        }
    }

    public void closeAppiumContext() {
        driver.closeApp();
        driver.quit();
    }
}