package engines.selenium;

import engines.selenium.driverInitialization.WebDriverFactory;

public class SeleniumManager {
    public void startUp() {
        try {
            WebDriverFactory.getThreadSafeInstance().initializeWebDriver();
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }

    public void shutDown() {
        WebDriverFactory.getThreadSafeInstance().cleanUp();
    }

    public String collectData(String destinationPath, String eventName) {

        return "";
    }
}
