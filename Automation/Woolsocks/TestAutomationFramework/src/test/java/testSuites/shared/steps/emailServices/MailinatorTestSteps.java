package testSuites.shared.steps.emailServices;

import common.core.TestInMemoryParameters;
import common.utilities.LoggingManager;
import testSuites.shared.steps.contracts.EmailServiceOperator;
import uiMappings.pages.emailProviders.Mailinator;

import java.time.LocalDate;
import java.time.LocalDateTime;

public class MailinatorTestSteps extends EmailServiceOperator {
    private String getEmailPrefix() {
        LocalDateTime currentTime = LocalDateTime.now();
        LocalDate date = currentTime.toLocalDate();
        String timeString = String.format("%s_%s_%s", currentTime.getHour(), currentTime.getMinute(), currentTime.getSecond());

        return String.format("TestEmail_%s_%s", date, timeString);
    }

    public void saveGeneratedEmail() {
        String temporaryEmail = String.format("%s@%s", getEmailPrefix(), "mailinator.com");
        TestInMemoryParameters.getInstance().generatedEmailAddress = temporaryEmail;
        LoggingManager.Info("Temporary email created: " + temporaryEmail);
    }

    public void getEmailAddress() {
        try {
            String emailPrefix = getEmailPrefix();

            getPage(Mailinator.class).
                    clikOnPublicInbox().
                    enterEmail(emailPrefix).
                    clickOnGo().
                    ImplicitlyWaitForPageToBeReady(Mailinator.class, 3);

            saveGeneratedEmail();
        } catch (Exception ex) {

        }
    }

    public void verifyEmailNotificationContent() {
        try {
            getPage(Mailinator.class).
                    enterEmail(TestInMemoryParameters.getInstance().generatedEmailAddress).
                    clickOnGo().
                    waitForEmail(TestInMemoryParameters.getInstance().emailSender).
                    clickOnEmail(TestInMemoryParameters.getInstance().emailSender).
                    ImplicitlyWaitForPageToBeReady(Mailinator.class, 3).
                    verifyEmailBodyContent(TestInMemoryParameters.getInstance().emailText).
                    ImplicitlyWaitForPageToBeReady(Mailinator.class, 3);
        }
        catch (Exception exception) { }
    }
}
