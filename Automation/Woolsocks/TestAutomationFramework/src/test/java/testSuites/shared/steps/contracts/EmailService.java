package testSuites.shared.steps.contracts;

import common.core.EmailServiceParameters;

import java.util.List;

public abstract class EmailService extends EmailServiceOperator{
    public static EmailServiceOperator emailService = null;
    static void initialize() {

    }

    static void checkAvailability(List<EmailServiceParameters> services) {

    }
}
