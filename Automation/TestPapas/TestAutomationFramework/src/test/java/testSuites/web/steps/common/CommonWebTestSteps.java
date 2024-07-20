package testSuites.web.steps.common;

import common.core.TestInMemoryParameters;
import engines.selenium.driverInitialization.WebDriverFactory;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.junit.Assert;
import testSuites.web.steps.Utilities;
import uiMappings.pages.boredApi.BoredApiPage;
import uiMappings.pages.webgoat.WebgoatLoginPage;
import uiMappings.pages.webgoat.WebgoatRegistrationPage;

import java.io.FileWriter;
import java.io.IOException;

public class CommonWebTestSteps extends Utilities {
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
            case "Boredapi":
                TestInMemoryParameters.getInstance().url = "https://bored.api.lewagon.com/";
                break;
            default:
                throw new RuntimeException("Unknown website: " + websiteName);
        }

        WebDriverFactory.getThreadSafeInstance().driver.get(TestInMemoryParameters.getInstance().url);
    }

    @Then("Validate {string} button color")
    public void validateButtonColor(String buttonName) {
        runStep(this::validateColor, buttonName);
    }

    private void validateColor(String elementName) {
        switch (elementName) {
            case "Submit":
                getPage(BoredApiPage.class).validateColor("blue");
                break;
            default:
                throw new RuntimeException("Unknown element: " + elementName);
        }
    }

    @When("Press {string} button")
    public void pressButton(String elementName) {
        runStep(this::pressSpecificButton, elementName);
    }

    private void pressSpecificButton(String elementName) {
        switch (elementName) {
            case "Submit":
                getPage(BoredApiPage.class).clickSubmitButton();
                break;
            case "Sign in":
                getPage(WebgoatLoginPage.class).clickLoginButton();
                break;
            case "Sign up":
                getPage(WebgoatRegistrationPage.class).clickSignupButton();
                break;
            case "Register":
                getPage(WebgoatLoginPage.class).clickRegistrationLink();
                break;
            default:
                throw new RuntimeException("Unknown element: " + elementName);
        }
    }
}
