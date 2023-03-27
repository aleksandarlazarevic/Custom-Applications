package com.framework;

import base.DriverFactory;
import base.ScreenFactory;
import io.cucumber.java.After;
import io.cucumber.java.Before;
import io.cucumber.java.en.And;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.jetbrains.annotations.NotNull;
import screens.LoginScreen;
import screens.MapScreen;
import utilities.ScreenHelpers;
import screens.LoginErrorScreen;

import java.net.MalformedURLException;

import static org.junit.jupiter.api.Assertions.assertTrue;

public class SmokeTestStepDefinitions {


    @Given("{string} screen is shown")
    public void loginScreenIsShown(@NotNull String screenName) {
        switch (screenName) {
            case "Login":
                ScreenHelpers.verifyLoginScreenIsShown();
                break;
            case "Map":
                ScreenHelpers.verifyMapScreenIsShown();
                break;
            default:
                throw new RuntimeException(String.format("Screen %s not found", screenName));
        }
    }

    @When("Enter username {string} and password {string}")
    public void enterUsernameAndPassword(String username, String password) {
        ScreenFactory.getInstance().CurrentPage.As(LoginScreen.class)
                                               .enterUsername(username)
                                               .enterPassword(password)
                                               .clickLoginButton();
    }

    @Then("Login error message is displayed")
    public void loginErrorMessageIsDisplayed() {
        ScreenFactory.getInstance().CurrentPage = ScreenHelpers.getScreen("LoginErrorScreen");
        assertTrue(ScreenFactory.getInstance().CurrentPage.As(LoginErrorScreen.class).isErrorMessageDisplayed(), "Incorrect error message");

    }

    @And("Return to the Login screen")
    public void returnToTheLoginScreen() {
        ScreenFactory.getInstance().CurrentPage.As(LoginErrorScreen.class).clickBackArrow();
    }

    @When("Find the charging station on the map")
    public void findTheChargingStationOnTheMap() {
        ScreenFactory.getInstance().CurrentPage.As(MapScreen.class).findChargingStation();
    }

    @Then("Verify charging station data is properly displayed")
    public void verifyChargingStationDataIsProperlyDisplayed() {
        ScreenFactory.getInstance().CurrentPage.As(MapScreen.class).verifyChargingStationData();
    }

    @Before
    public static void setup() throws MalformedURLException {
        DriverFactory.getInstance().initializeAppiumDriver();
    }

    @After
    public static void cleanup(){
        DriverFactory.getInstance().closeAppiumContext();
    }
}
