package testSuites.web.steps.webGoat;

import common.core.tests.JMeterTest;
import common.utilities.JMeterManager;
import io.cucumber.datatable.DataTable;
import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.junit.Assert;
import testSuites.web.steps.Utilities;
import uiMappings.pages.webgoat.WebgoatRegistrationPage;

import java.util.List;

public class WebgoatTestSteps extends Utilities {
    @When("Enter registration username {string}")
    public void enterRegistrationUsername(String username) {
        runStep(this::registerWithUsername, username);
    }

    private void registerWithUsername(String username) {
        getPage(WebgoatRegistrationPage.class).enterRegistrationUsername(username);
    }

    @And("Enter registration password {string}")
    public void enterRegistrationPassword(String password) {
        runStep(this::registerWithPassword, password);
    }

    private void registerWithPassword(String password) {
        getPage(WebgoatRegistrationPage.class).enterRegistrationPassword(password);
    }

    @And("Enter registration confirmation password {string}")
    public void enterRegistrationConfirmationPassword(String password) {
        runStep(this::confirmRegisterWithPassword, password);
    }

    private void confirmRegisterWithPassword(String password) {
        getPage(WebgoatRegistrationPage.class).confirmRegistrationPassword(password);
    }

    @And("Accept Terms and conditions")
    public void acceptTermsAndConditions() {
        runStep(this::acceptTerms);
    }

    private void acceptTerms() {
        getPage(WebgoatRegistrationPage.class).acceptTermsAndConditions();
    }

    @Then("Registration is successfull")
    public void registrationIsSuccessfull() {
        runStep(this::verifyRegistrationSuccess);
    }

    private void verifyRegistrationSuccess() {
        boolean isSuccessfull = getPage(WebgoatRegistrationPage.class).verifyRegistration();
        Assert.assertTrue("Registration did not succeed", isSuccessfull);
    }

    @When("Register multiple users")
    public void registerMultipleUsers(DataTable datatable) {
        List<String> numberOfUsers = datatable.asList();
        for (String number : numberOfUsers) {
            runStep(this::registerMultiUsers, number);
        }
    }

    private void registerMultiUsers(String numberOfUsers) {
        JMeterManager.setupJMeter();
        JMeterTest testParameters = new JMeterTest("Sign in performance test",
                "http://127.0.0.1:8080/WebGoat/registration",
                "/submit/firefox-desktop/messaging-system/1/b12f2687-2691-4294-9cdd-ff4b128c9750",
                "POST",
                Integer.parseInt(numberOfUsers),
                5,
                50);

        JMeterManager.setJMeterParametersAndRun(testParameters);
    }

    @When("Login multiple users")
    public void loginMultipleUsers(DataTable datatable) {
        List<String> numberOfUsers = datatable.asList();
        for (String number : numberOfUsers) {
            runStep(this::loginMultiUsers, number);
        }
    }

    private void loginMultiUsers(String numberOfUsers) {
        JMeterManager.setupJMeter();
        JMeterTest testParameters = new JMeterTest("Sign in performance test",
                "http://127.0.0.1:8080/WebGoat/login",
                "/submit/firefox-desktop/baseline/1/f70b138e-9a9a-4fc9-8bf8-4edd042a4953",
                "POST",
                Integer.parseInt(numberOfUsers),
                5,
                50);

        JMeterManager.setJMeterParametersAndRun(testParameters);
    }




}
