package engines.selenium;

import common.core.contracts.IEngineManager;
import engines.selenium.driverInitialization.WebDriverFactory;

public class SeleniumManager implements IEngineManager {
    public void startUp() {
        try {
            WebDriverFactory.getThreadSafeInstance().initializeWebDriver(30,30);
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
