package uiMappings.pages.webgoat;

import engines.selenium.base.BasePageActions;
import engines.selenium.driverInitialization.WebDriverFactory;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;

public class WebgoatRegistrationPage extends BasePageActions {
    // region Elements
    @FindBy(id = "username")
    public WebElement usernameTextbox;

    @FindBy(id = "password")
    public WebElement passwordTextbox;

    @FindBy(id = "matchingPassword")
    public WebElement confirmPasswordTextbox;

    @FindBy(xpath = "//button[@class='btn btn-primary btn-block']")
    public WebElement signupButton;

    @FindBy(xpath = "//input[@type='checkbox']")
    public WebElement agreeTermsAndConditions;

    @FindBy(id = "lesson-title")
    public WebElement homePageTitle;
    // endregion

    // region Methods
    public WebgoatRegistrationPage() {
        PageFactory.initElements(WebDriverFactory.getThreadSafeInstance().driver, this);
    }

    public WebgoatRegistrationPage clickSignupButton() {
        clickEx(this.signupButton, "signupButton", false);

        return this;
    }

    public WebgoatRegistrationPage clickAgreeTermsAndConditions() {
        clickEx(this.agreeTermsAndConditions, "agreeTermsAndConditions", false);

        return this;
    }

    public WebgoatRegistrationPage enterRegistrationUsername(String username) {
        sendKeysEx(this.usernameTextbox, username, "usernameTextbox", false, false);

        return this;
    }

    public WebgoatRegistrationPage enterRegistrationPassword(String password) {
        sendKeysEx(this.passwordTextbox, password, "passwordTextbox", false, false);

        return this;
    }

    public WebgoatRegistrationPage confirmRegistrationPassword(String password) {
        sendKeysEx(this.confirmPasswordTextbox, password, "confirmPasswordTextbox", false, false);

        return this;
    }

    public WebgoatRegistrationPage acceptTermsAndConditions() {
        tickCheckboxEx(this.agreeTermsAndConditions, "false", "confirmPasswordTextbox");

        return this;
    }

    public boolean verifyRegistration() {
        return isDisplayedEx(this.homePageTitle, "homePageTitle", false, false, 5);
    }

    // endregion
}
