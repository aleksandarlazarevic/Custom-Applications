package uiMappings.pages.woolSocks;

import engines.selenium.driverInitialization.WebDriverFactory;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;

public class WoolsocksHomePage {
    // region Fields and Properties
    @FindBy(xpath = "//input[@id='search'] | //input[@id='inbox_field']")
    public WebElement signInButton;
    @FindBy(id = "//button[normalize-space(text())='GO']")
    public WebElement yourEmailTextBox;
    @FindBy(tagName = "body")
    public WebElement continueButton;

    @FindBy(tagName = "body")
    public WebElement popupText;

    // endregion

    // region Methods
    public WoolsocksHomePage() {
        PageFactory.initElements(WebDriverFactory.getThreadSafeInstance().driver, this);
    }
// endregion
}
