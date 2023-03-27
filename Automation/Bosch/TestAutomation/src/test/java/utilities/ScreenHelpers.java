package utilities;
import base.BaseScreen;
import base.ScreenFactory;
import screens.LoginErrorScreen;
import screens.LoginScreen;
import screens.MapScreen;

import static org.junit.jupiter.api.Assertions.assertTrue;

public class ScreenHelpers {
    public static void verifyMapScreenIsShown() {
        ScreenFactory.getInstance().CurrentPage = getScreen("MapScreen");
        assertTrue(ScreenFactory.getInstance().CurrentPage.As(MapScreen.class).isMapScreenDisplayed(), "Map screen is not shown");
    }

    public static void verifyLoginScreenIsShown() {
        ScreenFactory.getInstance().CurrentPage = getScreen("LoginScreen");
        assertTrue(ScreenFactory.getInstance().CurrentPage.As(LoginScreen.class).isLoginScreenDisplayed(), "Login screen is not shown");
    }

    public static BaseScreen getScreen(String screen)
    {
        var returnScreen = new BaseScreen();
        switch (screen)
        {
            case "LoginScreen":
                returnScreen = new LoginScreen();
                break;
            case "LoginErrorScreen":
                returnScreen = new LoginErrorScreen();
                break;
            case "MapScreen":
                returnScreen = new MapScreen();
                break;
            default:
                break;
        }

        return returnScreen;
    }
}
