package base;

import io.appium.java_client.MobileElement;
import io.appium.java_client.AppiumDriver;
import io.appium.java_client.pagefactory.AppiumFieldDecorator;
import org.openqa.selenium.support.PageFactory;
import org.openqa.selenium.support.ui.WebDriverWait;

import java.rmi.activation.Activator;

public class BaseScreen {
    public AppiumDriver<MobileElement> appiumDriver;
    public WebDriverWait wait;

    public BaseScreen() {
        appiumDriver = DriverFactory.getInstance().driver;
    }

    public <TScreen extends BaseScreen> TScreen getInstance(Class<TScreen> screenClass) {
        try {
            return screenClass.newInstance();
        } catch (Exception ex) {
            ex.printStackTrace();
            return null;
        }
    }

    public <TScreen extends BaseScreen> TScreen As(Class<TScreen> screenToCast)
    {
        return screenToCast.isInstance(this) ? screenToCast.cast(this) : null;
    }
}
