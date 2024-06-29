package testSuites.shared.tests;

import common.core.TestInMemoryParameters;
import testSuites.shared.steps.CommonBrowserActions;
import testSuites.shared.steps.emailServices.EmailServiceTestSteps;

public class EmailServiceTests {
    public static void obtainRandomEmailAddress() {
        EmailServiceTestSteps emailServiceTestSteps = new EmailServiceTestSteps();
        emailServiceTestSteps.checkAvailability(TestInMemoryParameters.getInstance().emailServices);
        emailServiceTestSteps.initialize();
        CommonBrowserActions.goToDefaultEmailService();
        emailServiceTestSteps.getEmailAddress();
        CommonBrowserActions.closeBrowser();
    }
}
