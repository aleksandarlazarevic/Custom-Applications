package testSuites.web.steps;

import common.core.TestInMemoryParameters;
import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.junit.Assert;
import testSuites.shared.tests.EmailServiceTests;
import uiMappings.pages.woolSocks.WoolsocksHomePage;

public class WoolsockLoginSteps extends Utilities{
    @And("User enters temporary obtained email address")
    public void userEntersTemporaryObtainedEmailAddress() {
        String generatedEmail = TestInMemoryParameters.getInstance().generatedEmailAddress;
        getPage(WoolsocksHomePage.class).enterEmail(generatedEmail);
    }

    @When("Accept all cookies")
    public void acceptAllCookies() {
        getPage(WoolsocksHomePage.class).clickAcceptAllCookies();
    }

    @And("Confirm that the selected country is correct")
    public void confirmThatTheSelectedCountryIsCorrect() {
        getPage(WoolsocksHomePage.class).confirmSelectedCountry();
    }

    @When("Click {string} button")
    public void clickSignInButton(String buttonName) {
        switch (buttonName) {
            case "Sign In":
                getPage(WoolsocksHomePage.class).clickSigninButton();
                break;
            case "Continue":
                getPage(WoolsocksHomePage.class).clickContinueButton();
                break;
            default:
                throw new RuntimeException("Unknown button");
        }
    }

    @Then("{string} message is shown in a popup")
    public void loginLinkIsSentToMessageIsShownInAPopup() {
        String popupMessage = getPage(WoolsocksHomePage.class).getPopupMessage();
        Assert.assertEquals("Login link is sent to", popupMessage);
    }

    @When("Verification mail is received and the link is clicked")
    public void verificationMailIsReceivedAndTheLinkIsClicked() {
        TestInMemoryParameters.getInstance().emailSender = "no-reply@woolsocks.eu";
        TestInMemoryParameters.getInstance().emailText = "Verify your email";
        TestInMemoryParameters.getInstance().verificationLink = "Verify your email";
        EmailServiceTests.clickOnLoginVerificationLink();
    }

    @Then("Login succeeds")
    public void loginSucceeds() {
        String popupMessage = getPage(WoolsocksHomePage.class).getPopupMessage();
        Assert.assertEquals("Your email is verified", popupMessage);
        getPage(WoolsocksHomePage.class).clickContinueEmailVerificationButton();
        getPage(WoolsocksHomePage.class).clickContinueCountryButton();
        getPage(WoolsocksHomePage.class).fillInUserData("someName", "someLastname");
    }
}
