package engines.selenium.base;

import engines.extensions.WebElementExtensions;
import engines.selenium.driverInitialization.WebDriverFactory;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.WebDriverWait;

public class BasePage extends WebElementExtensions {
    // region Fields and Properties
    public static WebDriver driver;
    public static WebDriverWait wait;
    public static BasePage currentPage;

    public BasePage() {
        driver = WebDriverFactory.getThreadSafeInstance().driver;
    }


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

    public boolean isPageDisplayed() {
        boolean isPageDisplayed = true;
        isPageDisplayed = currentPage.isPageDisplayed();

        return isPageDisplayed;
    }

    public boolean arePageElementsDisplayed() {
        boolean isPageDisplayed = true;
        isPageDisplayed = currentPage.arePageElementsDisplayed();

        return isPageDisplayed;
    }
}
