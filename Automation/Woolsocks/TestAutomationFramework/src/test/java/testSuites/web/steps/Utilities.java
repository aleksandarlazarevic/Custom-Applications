package testSuites.web.steps;

import com.google.common.reflect.ClassPath;
import common.utilities.LoggingManager;
import engines.selenium.base.BasePage;

public class Utilities {
    public <TPage extends BasePage> TPage getPage(Class<TPage> pageToCast) {
        TPage returnPage;

        try {
            BasePage.currentPage = pageToCast.newInstance();
        } catch (InstantiationException e) {
            throw new RuntimeException(e);
        } catch (IllegalAccessException e) {
            throw new RuntimeException(e);
        }

        returnPage = BasePage.currentPage.castAs(pageToCast);

        return returnPage;
    }

    public boolean isPageDisplayed(String pageToCheck) {
        boolean isDisplayed = false;
        Class<? extends BasePage> classFound = null;
        final ClassLoader loader = Thread.currentThread().getContextClassLoader();

        try {
            for (final ClassPath.ClassInfo info : ClassPath.from(loader).getTopLevelClasses()) {
                String className = info.getName();
                if (className.contains(pageToCheck)) {
                    classFound = (Class<? extends BasePage>) info.load();
                    break;
                }
            }

            isDisplayed = getPage(classFound).isPageDisplayed();
        } catch (Exception exception) {
            LoggingManager.Error("Unable to retrieve page: " + pageToCheck + "Error: " + exception.getMessage());
        }

        return isDisplayed;
    }
}
