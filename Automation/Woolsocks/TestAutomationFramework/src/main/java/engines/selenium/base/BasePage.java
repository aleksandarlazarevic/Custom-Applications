package engines.selenium.base;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.WebDriverWait;

import java.time.Duration;

public class BasePage {
    // region Fields and Properties
    public static WebDriver driver;
    public static WebDriverWait wait;
    // endregion
    public static <TPage extends BasePage> TPage getPage(Class<TPage> pageClass) {
        TPage page;
        try {
            page = pageClass.newInstance();
        } catch (Exception exception) {
            exception.printStackTrace();
            return null;
        }

        return page;
    }

    public <TPage extends BasePage> TPage castAs(Class<TPage> pageToCast) {
        return pageToCast.isInstance(this) ? pageToCast.cast(this) : null;
    }
}
