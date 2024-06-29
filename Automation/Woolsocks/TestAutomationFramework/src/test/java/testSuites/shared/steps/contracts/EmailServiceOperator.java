package testSuites.shared.steps.contracts;

import engines.selenium.base.BasePageActions;

public abstract class EmailServiceOperator extends BasePageActions {
    public abstract void getEmailAddress();

    public abstract void saveGeneratedEmail();

    public abstract void getVerificationCodeFromEmail();

    public abstract void verifyEmailNotificationContent();

    public abstract void deleteAllEmailsFromInbox();
}
