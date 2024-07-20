package uiMappings.pages.boredApi;

import engines.selenium.base.BasePageActions;
import engines.selenium.driverInitialization.WebDriverFactory;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;

public class BoredApiPage extends BasePageActions {
  // region Elements
    @FindBy(xpath = "//button[1]")
    public WebElement submitButton;

    @FindBy(xpath = "//pre[1]")
    public WebElement activityElement;
    // endregion

    // region Methods
    public BoredApiPage() {
        PageFactory.initElements(WebDriverFactory.getThreadSafeInstance().driver, this);
    }

    public BoredApiPage clickSubmitButton() {
        clickEx(this.submitButton, "submitButton", false);

        return this;
    }

    public String getActivityElementData() {
        String retVal = getValueEx(this.activityElement);

        return retVal;
    }

    public void validateColor(String colorValue) {
        validateAttribute(submitButton, "color", colorValue,"submitButton", false );
    }
    // endregion
}
