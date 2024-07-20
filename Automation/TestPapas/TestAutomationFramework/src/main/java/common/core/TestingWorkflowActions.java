package common.core;

import common.core.configuration.ConfigurationManager;
import common.core.configuration.TestConfiguration;
import common.core.contracts.IEngineManager;
import common.core.contracts.ITestWorkflow;
import common.core.tests.TestData;
import common.core.tests.TestStepInfo;
import common.utilities.FileManager;
import common.utilities.LoggingManager;
import engines.apiTesting.ApiTestManager;
import engines.selenium.SeleniumManager;
import io.cucumber.core.backend.TestCaseState;
import io.cucumber.java.Scenario;
import io.cucumber.plugin.event.PickleStepTestStep;
import io.cucumber.plugin.event.TestCase;
import reporting.ReportManager;

import java.io.File;
import java.lang.reflect.Field;
import java.nio.file.Path;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Date;
import java.util.List;
import java.util.stream.Collectors;

public class TestingWorkflowActions implements ITestWorkflow {
    public ArrayList<IEngineManager> engines;
    public TestStepInfo testStepInfo;
    public ReportManager reportingManager;

    // region Test run setup
    public void testExecutionInitialization() {
        setUpTestConfiguration();
        setUpRootTestingDirectory();
        setUpReporting();
    }

    public void testExecutionFinalization() {
        createTestReport();
    }

    // endregion
    // region Test scenario setup
    public void testScenarioInitialization(Scenario scenario) {
        startTestEngine();
        setUpTestData(scenario);
        reportingManager.createTestCase(TestData.getInstance());
    }

    public void testScenarioFinalization(Scenario scenario) {
        setTestStatus();
        createLogFile();
        emptyTheListOfCurrentSteps();
        stopTestEngine();
    }

    // endregion
    // region Test Step setup
    @Override
    public void testStepInitialization(Scenario scenario) {
        getTestStepInfo(scenario);
        createTestStepInTheReportFile();
    }

    @Override
    public void testStepFinalization() {
    }

    // endregion
    // region Helper Methods
    // region Engine actions
    private void startTestEngine() {
        engines = getTestingEngines();

        for (var engine : engines) {
            engine.startUp();
        }
    }

    private void stopTestEngine() {
        for (var engine : engines) {
            engine.shutDown();
        }
    }

    protected ArrayList<IEngineManager> getTestingEngines() {
        ArrayList<IEngineManager> engines = new ArrayList();
        String systemTag = TestInMemoryParameters.getInstance().getTestConfiguration().systemConfiguration.tag;

        if (systemTag.equals("API")) {
            engines.add(new ApiTestManager());
        } else if (systemTag.equals("Web")) {
            engines.add(new SeleniumManager());
        }

        return engines;
    }

    // endregion
    // region Configuration actions
    protected TestConfiguration getTestingConfigurationFromFile(String configLocation) {
        TestConfiguration testConfiguration;

        try {
            testConfiguration = ConfigurationManager.readConfigurationFile(configLocation, TestConfiguration.class);
        } catch (Exception exception) {
            LoggingManager.Error(String.format("Failed to read out the test configuration file: {0} - exception: {1}",
                    configLocation, exception.getMessage()));
            throw new RuntimeException(exception);
        }

        return testConfiguration;
    }

    public void setUpTestConfiguration() {
        String configLocation = getCurrentTestRunConfiguration();
        TestConfiguration testingConfiguration = null;

        try {
            testingConfiguration = getTestingConfigurationFromFile(configLocation);
        } catch (Exception e) {
            throw new RuntimeException(e);
        }

        TestInMemoryParameters.getInstance().setTestConfiguration(testingConfiguration);
    }

    static String getCurrentTestRunConfiguration() {
        String targetDir = Path.of("").toAbsolutePath() + "/TestRunParameters/";
        TestInMemoryParameters.getInstance().setTestRunParametersLocation(targetDir);
        String filePath = findJsonConfig(targetDir);

        return filePath;
    }

    private static String findJsonConfig(String targetDir) {
        String filePath;
        String fileExtension = ".json";
        File[] listOfFiles = FileManager.getFilesWithExtension(targetDir, fileExtension);

        try {
            filePath = Arrays.stream(listOfFiles).findFirst().get().getAbsolutePath();

        } catch (Exception exception) {
            throw new RuntimeException("There is no test run configuration in the TestRunParameters folder: "
                    + exception.getMessage());
        }
        return filePath;
    }

    public static String getConfigurationProperty(String propertyName) {
        TestConfiguration configurationFilePath = TestInMemoryParameters.getInstance().getTestConfiguration();
        String propertyValue = "";

        try {
            propertyValue = ConfigurationManager.getTestConfigValue(configurationFilePath, propertyName);
        } catch (Exception exception) {
            throw new RuntimeException(exception);
        }

        return propertyValue;
    }
    // endregion
    // region Test data actions
    private static void setTestStatus() {
        TestData.getInstance().close();
    }

    public void setUpTestData(Scenario scenario) {
        TestData.initialize(scenario);
        TestData.getInstance().testData.testRunStartTime = LocalDateTime.now();
        createTestResultsDirectory();
    }

    private void setUpRootTestingDirectory() {
        DateFormat dateFormat = new SimpleDateFormat("dd-MM-yyyy_hh_mm_ssaa");
        String rootTestOutputDirectory = Path.of("").toAbsolutePath() +
                TestInMemoryParameters.getInstance().getTestConfiguration().testResultPath +
                dateFormat.format(new Date()) + "/";
        TestInMemoryParameters.getInstance().setRootTestDirectory(rootTestOutputDirectory);
    }

    private String createTestResultsDirectory() {
        String testName = TestData.getInstance().scenario.getName();
        String testResultsDirectory = TestInMemoryParameters.getInstance().getRootTestDirectory() + testName + "/";

        new File(testResultsDirectory).mkdirs();
        TestInMemoryParameters.getInstance().setTestResultsDirectory(testResultsDirectory);

        return testResultsDirectory;
    }

    private void getTestStepInfo(Scenario scenario) {
        PickleStepTestStep currentStepDef = getStepInfo(scenario);

        testStepInfo = new TestStepInfo();
        testStepInfo.description = currentStepDef.getStep().getText();
        testStepInfo.gherkinKeyword = currentStepDef.getStep().getKeyword();
        testStepInfo.startTime = LocalDateTime.now();

        TestData.getInstance().currentTestStep = testStepInfo;
        TestData.getInstance().testSteps.add(testStepInfo);
    }

    private static PickleStepTestStep getStepInfo(Scenario scenario) {
        List<PickleStepTestStep> stepDefs = new ArrayList<PickleStepTestStep>();

        try {
            Field delegateField = scenario.getClass().getDeclaredField("delegate");
            delegateField.setAccessible(true);
            TestCaseState testCaseState = (TestCaseState) delegateField.get(scenario);
            Field testCaseField = testCaseState.getClass().getDeclaredField("testCase");
            testCaseField.setAccessible(true);
            TestCase r = (TestCase) testCaseField.get(testCaseState);

            stepDefs = r.getTestSteps()
                    .stream()
                    .filter(x -> x instanceof PickleStepTestStep)
                    .map(x -> (PickleStepTestStep) x)
                    .collect(Collectors.toList());
        } catch (Exception exception) {
            LoggingManager.Error("Failed to retrieve test step information: " + exception.getMessage());
        }

        int currentNumberOfSteps = TestData.getInstance().stepCount;
        if (currentNumberOfSteps != 0) {
            currentNumberOfSteps -= 1;
        }

        return stepDefs.get(currentNumberOfSteps);
    }

    private void emptyTheListOfCurrentSteps() {
        TestData.getInstance().testSteps = new ArrayList<TestStepInfo>();
        TestData.getInstance().stepCount = 0;
    }

    // endregion
    // region Logging actions
    private static void createLogFile() {
        LoggingManager.endTestExecution(TestData.getInstance());
    }
    // endregion
    // region Reporting actions
    private void setUpReporting() {
        String reportPath = TestInMemoryParameters.getInstance().getRootTestDirectory() + "/TestResults.html";
        reportingManager = new ReportManager(reportPath);
    }

    private void createTestStepInTheReportFile() {
        reportingManager.createTestStep();
    }

    private void createTestReport() {
        reportingManager.createReport();
    }

    // endregion
    // endregion
}
