package uiMappings.pages.webgoat;

import engines.selenium.base.BasePageActions;
import engines.selenium.driverInitialization.WebDriverFactory;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;

public class WebgoatLoginPage extends BasePageActions {
    // region Elements
    @FindBy(id = "exampleInputEmail1")
    public WebElement usernameTextbox;

    @FindBy(id = "exampleInputPassword1")
    public WebElement passwordTextbox;

    @FindBy(xpath = "//button[@class='btn btn-primary btn-block']")
    public WebElement loginButton;

    @FindBy(xpath = "//a[text()='or register yourself as a new user']")
    public WebElement registrationLink;
    // endregion

    // region Methods
    public WebgoatLoginPage() {
        PageFactory.initElements(WebDriverFactory.getThreadSafeInstance().driver, this);
    }

    public WebgoatLoginPage clickLoginButton() {
        clickEx(this.loginButton, "loginButton", false);

        return this;
    }

    public WebgoatLoginPage clickRegistrationLink() {
        clickEx(this.registrationLink, "registrationLink", false);

        return this;
    }
    // endregion
}
