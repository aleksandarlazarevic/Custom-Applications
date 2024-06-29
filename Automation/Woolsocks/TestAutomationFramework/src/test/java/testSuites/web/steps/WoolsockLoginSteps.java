package testSuites.web.steps;

import common.core.TestInMemoryParameters;
import io.cucumber.java.en.And;

public class WoolsockLoginSteps extends Utilities{
    @And("User enters temporary obtained email address")
    public void userEntersTemporaryObtainedEmailAddress() {
        String generatedEmail = TestInMemoryParameters.getInstance().generatedEmailAddress;
        var tt = 6;
    }



}
