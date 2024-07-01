package testSuites.shared;

import common.core.contracts.IRunnableAction;
import common.core.contracts.IRunnableActionWithArgument;
import common.core.contracts.IRunnableActionWithArguments;
import common.core.tests.TestData;

public class BaseUtilities {
    // region Test Execution
    public void runStep(IRunnableAction action) {
        TestData.getInstance().runStep(action, TestData.getInstance().currentTestStep);
    }
    public void runStep(IRunnableActionWithArgument action, String argument) {
        TestData.getInstance().runStep(action, argument, TestData.getInstance().currentTestStep);
    }

    public void runStep(IRunnableActionWithArguments action, String argument1, String argument2) {
        TestData.getInstance().runStep(action, argument1, argument2, TestData.getInstance().currentTestStep);
    }
    // endregion
}
