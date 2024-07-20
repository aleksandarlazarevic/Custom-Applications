package testSuites.web.steps.googleMeet;

import common.core.TestInMemoryParameters;
import engines.selenium.driverInitialization.WebDriverFactory;
import io.cucumber.java.en.And;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.junit.Assert;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.remote.RemoteWebDriver;
import testSuites.shared.steps.CommonBrowserActions;
import testSuites.web.steps.Utilities;
import uiMappings.pages.googleMeet.GoogleMeetPage;

public class GoogleMeetTestSteps extends Utilities {
    private RemoteWebDriver driver;
    private String meetingLinkInvite;
    @When("Open {string} browser")
    public void openGivenBrowser(String browserName) {
        runStep(this::openBrowser, browserName);
    }

    private void openBrowser(String browserName) {
        switch (browserName) {
            case "Firefox":
                driver = new FirefoxDriver();
                driver.get("https://www.google.com/");
                break;
            default:
                throw new RuntimeException("Unknown browser: " + browserName);
        }
    }


    @Given("Google meet call is started")
    public void googleMeetCallIsStarted() {
        runStep(this::startGoogleMeetCall);
    }

    private void startGoogleMeetCall() {
        String googleMeetUrl = "https://meet.google.com/landing?hs=197&authuser=0";
        TestInMemoryParameters.getInstance().url = googleMeetUrl;
        CommonBrowserActions.goToDefaultUrl();

        getPage(GoogleMeetPage.class).clickNewMeetingButton();
        getPage(GoogleMeetPage.class).clickStartInstantMeetingButton();
        getPage(GoogleMeetPage.class).validateLeaveCallButtonColor("red");
        meetingLinkInvite = getPage(GoogleMeetPage.class).getMeetingLinkInvite();
    }

    @And("Join Google meet call")
    public void joinGoogleMeetCallFromFirefox() {
        runStep(this::joinGoogleMeetCall);
    }

    private void joinGoogleMeetCall() {
        driver.get(meetingLinkInvite);
    }

    @Then("There is a call stream")
    public void thereIsACallStream() {
        runStep(this::verifyTheCallIsEstablished);
    }

    private void verifyTheCallIsEstablished() {
        TestInMemoryParameters.getInstance().driver = this.driver;
        boolean isCallStarted = getPage(GoogleMeetPage.class).isLeaveCallButtonVisible();
        isCallStarted &= getPage(GoogleMeetPage.class).isVideoElementVisible();
        isCallStarted &= getPage(GoogleMeetPage.class).isVideoPlaying();
        Assert.assertTrue("The call has not started", isCallStarted);
    }

    @And("Buttons are visible in both browsers")
    public void buttonsAreVisibleInBothBrowsers() {
        runStep(this::verifyThatButtonsAreVisibleInBothBrowsers);
    }

    private void verifyThatButtonsAreVisibleInBothBrowsers() {
        verifyThatButtonsAreVisibleInFirefoxBrowsers();
        verifyThatButtonsAreVisibleInChromeBrowsers();
    }
    private void verifyThatButtonsAreVisibleInFirefoxBrowsers() {
        boolean isCallStarted = getPage(GoogleMeetPage.class).isLeaveCallButtonVisible();
        Assert.assertTrue("The leave call button is not visible in Firefox", isCallStarted);
    }
    private void verifyThatButtonsAreVisibleInChromeBrowsers() {
        TestInMemoryParameters.getInstance().driver = WebDriverFactory.getThreadSafeInstance().driver;
        boolean isCallStarted = getPage(GoogleMeetPage.class).isLeaveCallButtonVisible();
        Assert.assertTrue("The leave call button is not visible in Chrome", isCallStarted);
    }

    @And("{string} button has {string} color")
    public void leavecallButtonHasRedColor(String color) {
        runStep(this::verifyLeavecallButtonColor, color);
    }

    private void verifyLeavecallButtonColor(String color) {
        getPage(GoogleMeetPage.class).validateLeaveCallButtonColor("red");
        driver.close();
    }
}
