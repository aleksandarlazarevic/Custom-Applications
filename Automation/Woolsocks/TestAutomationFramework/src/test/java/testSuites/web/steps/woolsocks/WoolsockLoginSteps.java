package testSuites.web.steps.woolsocks;

import common.core.TestInMemoryParameters;
import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.junit.Assert;
import testSuites.shared.tests.EmailServiceTests;
import testSuites.web.steps.Utilities;
import uiMappings.pages.woolSocks.WoolsocksHomePage;

import java.io.FileReader;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;

public class WoolsockLoginSteps extends Utilities {
    @And("User enters temporary obtained email address")
    public void userEntersTemporaryObtainedEmailAddress() {
        runStep(this::enterTemporaryEmailAddress);
    }

    private void enterTemporaryEmailAddress() {
        String generatedEmail = null;
        try {
            generatedEmail = Files.readString(Path.of("testParameters.txt"));
        } catch (IOException e) {
            throw new RuntimeException(e);
        }

        getPage(WoolsocksHomePage.class).enterEmail(generatedEmail);
    }

    @When("Accept all cookies")
    public void acceptAllCookies() {
        runStep(this::acceptCookies);
    }

    private void acceptCookies() {
        getPage(WoolsocksHomePage.class).clickAcceptAllCookies();
    }

    @And("Confirm that the selected country is correct")
    public void confirmThatTheSelectedCountryIsCorrect() {
        runStep(this::confirmCountry);
    }

    private void confirmCountry() {
        getPage(WoolsocksHomePage.class).confirmSelectedCountry();
    }

    @When("Click {string} button")
    public void clickSignInButton(String buttonName) {
        runStep(this::clickSigniIn, buttonName);
    }

    private void clickSigniIn(String buttonName) {
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
    public void messageIsShownInAPopup(String expectedMessage) {
        runStep(this::verifyPopupMessage, expectedMessage);
    }

    private void verifyPopupMessage(String expectedMessage) {
        String popupMessage = getPage(WoolsocksHomePage.class).getPopupMessage();
        Assert.assertEquals(expectedMessage, popupMessage);
    }

    @When("Verification mail is received and the link is clicked")
    public void verificationMailIsReceivedAndTheLinkIsClicked() {
        runStep(this::clickReceivedVerificationLink);
    }

    private void clickReceivedVerificationLink() {
        TestInMemoryParameters.getInstance().emailSender = "no-reply@woolsocks.eu";
        TestInMemoryParameters.getInstance().emailText = "Verify your email";
        TestInMemoryParameters.getInstance().verificationLink = "Verify your email";
        EmailServiceTests.clickOnLoginVerificationLink();
    }

    @Then("Login succeeds")
    public void loginSucceeds() {
        runStep(this::verifyLogin);
    }

    private void verifyLogin() {
        String popupMessage = getPage(WoolsocksHomePage.class).getPopupMessage();
        Assert.assertEquals("Your email is verified", popupMessage);
        getPage(WoolsocksHomePage.class).clickContinueEmailVerificationButton();
        getPage(WoolsocksHomePage.class).clickContinueCountryButton();
        getPage(WoolsocksHomePage.class).fillInUserData("someName", "someLastname");
    }
}
