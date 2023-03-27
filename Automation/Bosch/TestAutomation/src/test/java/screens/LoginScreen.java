package screens;

import base.BaseScreen;
import io.appium.java_client.MobileElement;
import org.openqa.selenium.By;
import utilities.Helpers;

public class LoginScreen extends BaseScreen {
    public MobileElement ScreenTitle = Helpers.getScreenElement(By.xpath("//*[(@class='android.widget.TextView')]"));
    public MobileElement Username = Helpers.getScreenElement(By.id("editUsername"));
    public MobileElement Password = Helpers.getScreenElement(By.id("editPassword"));
    public MobileElement LoginButton = Helpers.getScreenElement(By.id("buttonLogin"));
    public MobileElement SettingsMenu = Helpers.getScreenElement(By.xpath("//*[(@class='android.widget.ImageView')]"));

    public LoginScreen enterUsername(String username)
    {
        Username.click();
        Username.clear();
        Username.sendKeys(username);
        return getInstance(LoginScreen.class);
    }

    public LoginScreen enterPassword(String password)
    {
        Password.click();
        Password.clear();
        Password.sendKeys(password);
        return getInstance(LoginScreen.class);
    }

    public LoginScreen clickLoginButton()
    {
        LoginButton.click();
        return getInstance(LoginScreen.class);
    }

    public boolean isLoginScreenDisplayed()
    {
        return LoginButton.isDisplayed();
    }
}
