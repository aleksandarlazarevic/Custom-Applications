package screens;

import base.BaseScreen;
import io.appium.java_client.MobileElement;
import org.openqa.selenium.By;
import utilities.Helpers;

public class LoginErrorScreen extends BaseScreen {
    public MobileElement ScreenTitle = Helpers.getScreenElement(By.xpath("//android.view.ViewGroup/android.widget.LinearLayout//android.widget.TextView"));
    public MobileElement BackArrow = Helpers.getScreenElement(By.xpath("//android.widget.ImageButton"));
    public MobileElement ErrorMessage = Helpers.getScreenElement(By.xpath("//android.widget.TextView[@text='Login error']"));
    public MobileElement SettingsMenu = Helpers.getScreenElement(By.xpath("//*[(@class='android.widget.ImageView')]"));

    public LoginErrorScreen clickBackArrow()
    {
        BackArrow.click();
        return getInstance(LoginErrorScreen.class);
    }

    public boolean isErrorMessageDisplayed()
    {
        return ErrorMessage.isDisplayed();
    }
}
