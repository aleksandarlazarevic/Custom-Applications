package testSuites.shared.steps.contracts;

import engines.selenium.base.BasePageActions;

public abstract class EmailServiceOperator extends BasePageActions {
    public abstract void getEmailAddress();

    public abstract void saveGeneratedEmail();

    public abstract void verifyEmailNotificationContent();
}
