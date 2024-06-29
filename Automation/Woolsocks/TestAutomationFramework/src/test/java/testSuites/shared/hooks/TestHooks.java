package testSuites.shared.hooks;

import common.core.TestingWorkflowActions;
import io.cucumber.java.*;

public class TestHooks {
    private static TestingWorkflowActions testWorkflowData = new TestingWorkflowActions();

    @BeforeAll
    public static void initializeTestRun() {
        testWorkflowData.testExecutionInitialization();
    }

    @Before
    public static void initializeScenario(Scenario scenario) {
        testWorkflowData.testScenarioInitialization(scenario);
    }

    @BeforeStep
    public static void beforeStep(Scenario scenario) {
        testWorkflowData.testStepInitialization(scenario);
    }

    @AfterStep
    public static void afterStep() {
        testWorkflowData.testStepFinalization();
    }

    @After
    public static void scenarioCleanUp(Scenario scenario) {
        testWorkflowData.testScenarioFinalization(scenario);
    }

    @AfterAll
    public static void testRunCleanUp() {
        testWorkflowData.testExecutionFinalization();
    }
}
