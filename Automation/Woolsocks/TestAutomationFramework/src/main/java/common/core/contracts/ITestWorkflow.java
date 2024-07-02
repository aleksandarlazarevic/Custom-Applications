package common.core.contracts;

import io.cucumber.java.Scenario;

public interface ITestWorkflow {
    public void testExecutionInitialization();

    public void testExecutionFinalization();

    public void testScenarioInitialization(Scenario scenario);

    public void testScenarioFinalization(Scenario scenario);

    public void testStepInitialization(Scenario scenario);

    public void testStepFinalization();
}
