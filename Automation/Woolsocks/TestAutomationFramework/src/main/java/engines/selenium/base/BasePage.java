package engines.selenium.base;

public class BasePage {
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
