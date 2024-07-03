package testSuites.web.steps.common;

import common.core.EmailServiceParameters;
import common.core.TestInMemoryParameters;
import engines.selenium.driverInitialization.WebDriverFactory;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import org.junit.Assert;
import testSuites.shared.tests.EmailServiceTests;
import testSuites.web.steps.Utilities;

import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;

public class CommonTestSteps extends Utilities {
    @Given("Obtain a temporary email address")
    public void obtainATemporaryEmailAddress() {
        runStep(this::obtainRandomEmailAddress);
    }

    private void obtainRandomEmailAddress() {
        EmailServiceTests.obtainRandomEmailAddress();
    }

    @Then("{string} page is displayed")
    public void pageIsDisplayed(String pageName) {
        runStep(this::verifyThePageIsDisplayed, pageName);
    }

    private void verifyThePageIsDisplayed(String pageName) {
        boolean isPageDisplayed = isPageDisplayed(pageName);
        Assert.assertTrue("Page is not displayed: " + pageName, isPageDisplayed);
    }

    @Given("{string} website is opened")
    public void websiteIsOpened(String websiteName) {
        runStep(this::openAWebsite, websiteName);
    }

    private void openAWebsite(String websiteName) {
        switch (websiteName) {
            case "Woolsocks":
                TestInMemoryParameters.getInstance().url = "https://woolsocks.eu/en-DE";
                break;
            case "YouTube":
                TestInMemoryParameters.getInstance().url = "https://www.youtube.com/";
                break;
            default:
                throw new RuntimeException("Unknown website: " + websiteName);
        }

        WebDriverFactory.getThreadSafeInstance().driver.get(TestInMemoryParameters.getInstance().url);
    }

    @Given("{string} is used as online email service")
    public void onlineEmailServiceSetup(String chosenEmailService) {
        runStep(this::setupOnlineEmailService, chosenEmailService);
    }

    private void setupOnlineEmailService(String chosenEmailService) {
        switch (chosenEmailService) {
            case "Mailinator":
                TestInMemoryParameters.getInstance().emailServices.add(new EmailServiceParameters("Mailinator", "https://www.mailinator.com/"));
                break;
            default:
                throw new RuntimeException("Unknown online email service: " + chosenEmailService);
        }
    }

    @Then("Temporary email is set")
    public void temporaryEmailIsSet() {
        runStep(this::verifyThatTemporaryEmailIsSet);
    }

    private void verifyThatTemporaryEmailIsSet() {
        String generatedEmail = TestInMemoryParameters.getInstance().generatedEmailAddress;
        FileWriter writer = null;
        try {
            writer = new FileWriter("testParameters.txt");
            writer.write(generatedEmail);
            writer.close();
        } catch (IOException e) {
            throw new RuntimeException(e);
        }

        Assert.assertNotEquals("Temporary email has not been generated properly", "", generatedEmail);
    }
}
