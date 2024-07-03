package uiMappings.pages.woolSocks;

import engines.selenium.base.BasePageActions;
import engines.selenium.driverInitialization.WebDriverFactory;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;

public class WoolsocksHomePage extends BasePageActions {
    // region Fields and Properties
    @FindBy(xpath = "//button[@class='Button_root__zrJZ8 Button_variant-primary___lo_p Button_size-regular__DNDSB CookieConsentModal_button__EEA0Z']")
    public WebElement acceptAllCookiesButton;

    @FindBy(xpath = "//button[contains(@class,'LocaleConfirmSticker_button__QIa8N m-r-8')]")
    public WebElement confirmSelectedCountryButton;

    @FindBy(xpath = "(//button[(@data-testid='sign-up-in-button')])[5]")
    public WebElement signInButton;
    @FindBy(xpath = "//input[@id='emailAddressInput']")
    public WebElement yourEmailTextBox;

    @FindBy(xpath = "//button[@id='auth-options-continue-with-email']")
    public WebElement continueButton;

    @FindBy(xpath = "//button[contains(@class,'StepEmailVerified_button__WrrPC')]")
    public WebElement confirmEmailVerificationButton;

    @FindBy(xpath = "//button[contains(@class,'StepCountry_button__19wqY')]")
    public WebElement confirmCountryButton;

    @FindBy(xpath = "//button[@class='Title_base__E4y4s Title_variant-large__zm4kO Title_align-center__fsJCu']")
    public WebElement popupText;

    @FindBy(xpath = "//button[@class='Radio_radio__QjUtK']")
    public WebElement mrRadioButton;

    @FindBy(id = "firstNameInput")
    public WebElement firstNameInput;

    @FindBy(id = "lastNameInput")
    public WebElement lastNameInput;

    @FindBy(xpath = "//button[@class='Button_root__zrJZ8 Button_variant-primary___lo_p Button_size-wide__SjZ3w']")
    public WebElement continueUserDataButton;
    // endregion

    // region Methods
    public WoolsocksHomePage() {
        PageFactory.initElements(WebDriverFactory.getThreadSafeInstance().driver, this);
    }

    public void clickAcceptAllCookies() {
        clickEx(acceptAllCookiesButton, "acceptAllCookiesButton", false);
    }

    public void confirmSelectedCountry() {
        clickEx(confirmSelectedCountryButton, "confirmSelectedCountryButton", false);
    }

    public void clickSigninButton() {
        clickEx(signInButton, "signInButton", false);
    }

    public void enterEmail(String generatedEmail) {
        sendKeysEx(yourEmailTextBox, generatedEmail, "yourEmailTextBox", false, false);
    }

    public void clickContinueButton() {
        clickEx(continueButton, "continueButton", false);
    }

    public void clickContinueEmailVerificationButton() {
        clickEx(confirmEmailVerificationButton, "confirmEmailVerificationButton", false);
    }

    public void clickContinueCountryButton() {
        clickEx(confirmCountryButton, "confirmCountryButton", false);
    }

    public String getPopupMessage() {
        return getValueEx(popupText);
    }

    public void fillInUserData(String firstName, String lastName) {
        clickEx(mrRadioButton, "mrRadioButton", false);
        sendKeysEx(firstNameInput, firstName, "firstNameInput", false, false);
        sendKeysEx(lastNameInput, lastName, "lastNameInput", false, false);
        clickEx(continueUserDataButton, "continueUserDataButton", false);
    }

    public boolean isPageDisplayed() {
        boolean isPageDisplayed = isDisplayedEx(signInButton, "signInButton", false, false, 2);

        return isPageDisplayed;
    }
// endregion
}
