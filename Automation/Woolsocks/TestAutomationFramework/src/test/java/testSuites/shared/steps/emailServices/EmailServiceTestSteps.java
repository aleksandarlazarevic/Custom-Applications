package testSuites.shared.steps.emailServices;

import common.core.EmailServiceParameters;
import common.core.TestInMemoryParameters;
import common.utilities.LoggingManager;
import common.utilities.WebsiteManager;
import testSuites.shared.steps.contracts.EmailService;

import java.util.List;
import java.util.stream.Collectors;
public class EmailServiceTestSteps extends EmailService {
    public void initialize() throws IllegalStateException {
        switch (TestInMemoryParameters.getInstance().emailServiceName) {
            case "Mailinator":
                emailService = new MailinatorTestSteps();
                LoggingManager.Info("Initialized Mailinator");
                break;
            default:
                LoggingManager.Error("Email service not initialized");
        }
    }
    public void saveGeneratedEmail() {
        emailService.saveGeneratedEmail();
    }

    public void getEmailAddress() {
        emailService.getEmailAddress();
    }

    public void checkAvailability(List<EmailServiceParameters> services) {
        var mailServices = services.stream()
                .filter(x -> WebsiteManager.isWebsiteAvailable(x.getUrl())).collect(Collectors.toList());

        if (mailServices == null) {
            LoggingManager.Error("None of the email providers is currently available");
        }

        var firstAvailableEmailService = services.stream().findFirst();
        TestInMemoryParameters.getInstance().emailServiceUrl = firstAvailableEmailService.get().url;
        TestInMemoryParameters.getInstance().emailServiceName = firstAvailableEmailService.get().name;

        LoggingManager.Info("Chosen email service provider name: " + firstAvailableEmailService.get().name);
        LoggingManager.Info("Chosen email service provider url: " + firstAvailableEmailService.get().url);
    }

    public void verifyEmailNotificationContent() {
        emailService.verifyEmailNotificationContent();
    }
}
