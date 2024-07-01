package common.core.tests;

import common.core.TestInMemoryParameters;
import common.core.contracts.IRunnableAction;
import common.core.contracts.IRunnableActionWithArgument;
import common.core.contracts.IRunnableActionWithArguments;
import common.utilities.LoggingManager;
import org.openqa.selenium.OutputType;
import org.openqa.selenium.remote.RemoteWebDriver;
import io.cucumber.java.Scenario;
import reporting.ReportManager;

import java.io.File;
import java.nio.file.Files;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.time.Duration;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Date;
import java.util.List;

public class TestData {
    // region Fields and Properties
    private static TestData instance = null;
    public static Scenario scenario;
    public Collection<String> testTags;
    public String testName;
    public String testId;
    public String testResultsDirectory;
    public int stepCount;
    public LocalDateTime testRunStartTime;
    private boolean stepContinuation = false;
    private boolean isHighImportanceFailed = false;
    public TestStepInfo currentTestStep;
    public List<TestStepInfo> testSteps = new ArrayList<TestStepInfo>();
    public static TestData testData;
    public String testDescription;
    public TestState status;
    public static LocalDateTime startTime;
    public LocalDateTime endTime;
    public Duration duration;
    public Exception testException;
    public Exception stepException;
    public String currentScreenshotLocation;
    public String currentScreenshotName;
    // endregion

    public TestData(Scenario scenario) {
        this.scenario = scenario;
        this.stepCount = 0;
        this.status = TestState.UNKNOWN;
        this.stepContinuation = true;
        this.testId = scenario.getId();
        this.testName = scenario.getName();
        this.testDescription = scenario.getName().toUpperCase();
        this.testTags = scenario.getSourceTagNames();
    }

    // Get thread safe instance
    public static TestData getInstance() {
        if (instance == null) {
            synchronized (TestData.class) {
                if (instance == null) {
                    instance = new TestData(scenario);
                }
            }
        }
        return instance;
    }

    // region Methods
    public TestState runStep(IRunnableAction action, TestStepInfo stepInfo) {
        try {
            if (!shouldTheStepBeExecuted(stepInfo)) {
                action.performOperation();
                stepInfo.status = TestState.PASSED;
                ReportManager.setTestStepOutcome(stepInfo);
            }
        } catch (Exception ex) {
            handleException(ex, stepInfo);
        } finally {
            finalizeStep(stepInfo);
        }

        return stepInfo.status;
    }

    public TestState runStep(IRunnableActionWithArgument action, String argument, TestStepInfo stepInfo) {
        try {
            if (!shouldTheStepBeExecuted(stepInfo)) {
                action.performOperation(argument);
                stepInfo.status = TestState.PASSED;
                ReportManager.setTestStepOutcome(stepInfo);
            }
        } catch (Exception ex) {
            handleException(ex, stepInfo);
        } finally {
            finalizeStep(stepInfo);
        }

        return stepInfo.status;
    }

    public TestState runStep(IRunnableActionWithArguments action, String argument1, String argument2, TestStepInfo stepInfo) {
        try {
            if (!shouldTheStepBeExecuted(stepInfo)) {
                action.performOperation(argument1, argument2);
                stepInfo.status = TestState.PASSED;
                ReportManager.setTestStepOutcome(stepInfo);
            }
        } catch (Exception ex) {
            handleException(ex, stepInfo);
        } finally {
            finalizeStep(stepInfo);
        }

        return stepInfo.status;
    }

    public static void initialize(Scenario scenario) {
        if (testData == null) {
            testData = new TestData(scenario);
        }
    }

    public void close() {
        TestState testStatusIn = TestState.PASSED;
        if (this.scenario.isFailed()) {
            testStatusIn = TestState.FAILED;
        }

        TestState testStatusOut = TestState.PASSED;
        if (haveAnyOfTheStepsFailed(testSteps) || somethingOutsideTestStepHasFailed()) {
            testStatusOut = TestState.FAILED;
        }

        if (testStatusIn == TestState.PASSED && testStatusOut == TestState.PASSED) {
            status = TestState.PASSED;
        } else {
            status = TestState.FAILED;
        }

        testData = null;

        if (status != TestState.PASSED) {
            LoggingManager.Fatal("Test case failed: " + this.scenario.getName());
        }
    }

    private boolean shouldTheStepBeExecuted(TestStepInfo stepInfo) {
        boolean skip = false;

        if (this.stepCount == 0) {
            LoggingManager.beginTestExecution(TestData.getInstance().testData);
        }

        this.stepCount++;
        LoggingManager.beginStep(stepInfo, this.stepCount);

        if (stepInfo.isMandatory) {
            LoggingManager.Info("This is a mandatory step and will always be executed");
        } else if (isHighImportanceFailed) {
            LoggingManager.Info("Skipping remaining test steps because important step failed!");
        } else if (shouldContinueOnFailure(stepInfo)) {
            skip = false;
        } else if (this.haveAnyOfTheStepsFailed(testSteps) && stepInfo.skipStepOnFailure) {
            LoggingManager.Info("Skipping test execution because one of the previous steps has failed!");
            skip = true;
        } else if (stepInfo.skipStep) {
            LoggingManager.Info("Skipping test execution because step skip execution flag has been set to: true");
            skip = true;
        }

        return skip;
    }

    public void setExternalException(Exception exception) {
        status = TestState.FAILED;
        testException = exception;
    }

    private void handleException(Exception exception, TestStepInfo stepInfo) {
        stepInfo.status = TestState.FAILED;
        stepInfo.stepException = exception;
        stepException = exception;
        LoggingManager.stepException(stepInfo);
        stepContinuation = false;
        takeAScreenshot(stepInfo);

        if (!shouldContinueOnFailure(stepInfo)) {
            throw new AssertionError(exception);
        }
    }

    private static void takeAScreenshot(TestStepInfo stepInfo) {
        LoggingManager.Info("Taking a screenshot of a failed test step: " + stepInfo.description);

        RemoteWebDriver drv = TestInMemoryParameters.getInstance().driver;
        File localScreenshot = drv.getScreenshotAs(OutputType.FILE);
        DateFormat dateFormat = new SimpleDateFormat("dd-MM-yyyy__hh_mm_ssaa");
        String destinationLocation = TestInMemoryParameters.getInstance().getTestResultsDirectory();
        String screenshotName = stepInfo.description + "-" + dateFormat.format(new Date()) + ".png";
        String screenshotLocation = destinationLocation + "/" + screenshotName;

        try {
            Files.copy(localScreenshot.toPath(), new File(screenshotLocation).toPath());
            TestData.getInstance().currentScreenshotLocation = screenshotLocation;
            TestData.getInstance().currentScreenshotName = screenshotName;
        } catch (Exception exception) {
            exception.printStackTrace();
        }
    }

    private void finalizeStep(TestStepInfo stepInfo) {
        LoggingManager.endStep(stepInfo);
    }

    private boolean haveAnyOfTheStepsFailed(List<TestStepInfo> stepCollection) {
        if (stepCollection.stream().anyMatch(x -> x.status == TestState.FAILED)) {
            status = TestState.FAILED;
            return true;
        }

        status = TestState.PASSED;
        return false;
    }

    private boolean somethingOutsideTestStepHasFailed() {
        if (testException != null) {
            status = TestState.FAILED;
            return true;
        }

        status = TestState.PASSED;
        return false;
    }

    private boolean shouldContinueOnFailure(TestStepInfo stepInfo) {
        if (!stepInfo.skipStepOnFailure) {
            return stepContinuation = true;
        } else if (stepInfo.skipStepOnFailure && stepContinuation) {
            return true;
        }

        return false;
    }

    public String getTestResultsDirectory() {
        return testResultsDirectory;
    }

    public void setTestResultsDirectory(String testResultsDirectory) {
        this.testResultsDirectory = testResultsDirectory;
    }
    // endregion
}
