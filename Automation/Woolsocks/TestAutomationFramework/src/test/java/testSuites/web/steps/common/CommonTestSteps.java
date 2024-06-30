package testSuites.web.steps.common;

import common.core.EmailServiceParameters;
import common.core.TestInMemoryParameters;
import engines.selenium.driverInitialization.WebDriverFactory;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import org.junit.Assert;
import testSuites.shared.tests.EmailServiceTests;
import testSuites.web.steps.Utilities;

public class CommonTestSteps extends Utilities {
    @Given("Obtain a temporary email address")
    public void obtainATemporaryEmailAddress() {
        EmailServiceTests.obtainRandomEmailAddress();
    }

    @Then("{string} page is displayed")
    public void pagePageIsDisplayed(String pageName) {
        boolean isPageDisplayed = isPageDisplayed(pageName);
        Assert.assertTrue("Page is not displayed: " + pageName, isPageDisplayed);
    }

    @Given("{string} website is opened")
    public void websiteIsOpened(String websiteName) {
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
        String generatedEmail = TestInMemoryParameters.getInstance().generatedEmailAddress;
        Assert.assertNotEquals("Temporary email has not been generated properly", "", generatedEmail);
    }
}
