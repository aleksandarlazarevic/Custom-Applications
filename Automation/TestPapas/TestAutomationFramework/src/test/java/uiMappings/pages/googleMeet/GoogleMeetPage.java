package uiMappings.pages.googleMeet;

import engines.selenium.base.BasePageActions;
import engines.selenium.driverInitialization.WebDriverFactory;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;

public class GoogleMeetPage extends BasePageActions {
    // region Elements
    @FindBy(xpath = "//span[@class='VfPpkd-vQzf8d']")
    public WebElement newMeetingButton;

    @FindBy(xpath = "//span[@class='VfPpkd-StrnGf-rymPhb-b9t22c']")
    public WebElement startInstantMeeting;

    @FindBy(xpath = "//div[@class='VYBDae-Bz112c-RLmnJb']")
    public WebElement leaveCallButton;

    @FindBy(xpath = "//div[@class='okqcNc']")
    public WebElement meetingLinkInvite;

    @FindBy(xpath = "//video[@class='Gv1mTb-aTv5jf Gv1mTb-PVLJEc']")
    public WebElement videoElement;
    // endregion

    // region Methods
    public GoogleMeetPage() {
        PageFactory.initElements(WebDriverFactory.getThreadSafeInstance().driver, this);
    }

    public GoogleMeetPage clickNewMeetingButton() {
        clickEx(this.newMeetingButton, "newMeetingButton", false);

        return this;
    }

    public GoogleMeetPage clickStartInstantMeetingButton() {
        clickEx(this.startInstantMeeting, "startInstantMeeting", false);

        return this;
    }

    public void validateLeaveCallButtonColor(String colorValue) {
        validateAttribute(leaveCallButton, "color", colorValue, "leaveCallButton", false);
    }

    public boolean isLeaveCallButtonVisible() {
        return isDisplayed(leaveCallButton, 5);
    }

    public String getMeetingLinkInvite() {
        String retVal = getValueEx(meetingLinkInvite);

        return retVal;
    }

    public boolean isVideoElementVisible() {
        return isDisplayed(videoElement, 5);
    }

    public boolean isVideoPlaying() {
        double currentTime = Double.parseDouble(videoElement.getAttribute("currentTime"));
        boolean isVideoPlaying = currentTime > 0.0;

        return isVideoPlaying;
    }
    // endregion
}
