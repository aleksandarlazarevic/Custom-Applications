package reporting;

import common.core.TestInMemoryParameters;
import common.core.tests.TestData;
import common.core.tests.TestState;
import common.core.tests.TestStepInfo;
import com.aventstack.extentreports.ExtentReports;
import com.aventstack.extentreports.ExtentTest;
import com.aventstack.extentreports.GherkinKeyword;
import com.aventstack.extentreports.reporter.ExtentHtmlReporter;
import com.aventstack.extentreports.reporter.configuration.Protocol;
import com.aventstack.extentreports.reporter.configuration.Theme;
import org.apache.commons.io.FilenameUtils;

import java.io.IOException;
import java.util.Arrays;
import java.util.Collection;

public class ReportManager {
    private static ExtentHtmlReporter htmlReporter;
    private static ExtentReports extentReports;
    private static ExtentTest extentStep;
    private static ExtentTest extentTest;
    private static ExtentTest extentFeature;
    private static ExtentTest extentScenario;

    public ReportManager(String reportPath) {
        htmlReporter = new ExtentHtmlReporter(reportPath);
        htmlReporter.config().setCSS("css-string");
        htmlReporter.config().setDocumentTitle("Test Execution Summary");
        htmlReporter.config().setCSS(".r-img { width: 30%; }");
        htmlReporter.config().setEncoding("utf-8");
        htmlReporter.config().setJS("js-string");
        htmlReporter.config().setProtocol(Protocol.HTTPS);
        htmlReporter.config().setReportName("Test Automation Report");
        htmlReporter.config().setTheme(Theme.DARK);
        htmlReporter.config().setTimeStampFormat("MMM dd, yyyy HH:mm:ss");

        extentReports = new ExtentReports();
        extentReports.attachReporter(htmlReporter);
    }

    public void createReport() {
        extentReports.flush();
    }

    public void createFeature(String name) {
        try {
            extentFeature = extentReports.createTest(new GherkinKeyword("Feature"), name);
        } catch (ClassNotFoundException e) {
            throw new RuntimeException(e);
        }
    }

    public void createScenario(String name) {
        try {
            extentScenario = extentFeature.createNode(new GherkinKeyword("Scenario"), name);
        } catch (ClassNotFoundException e) {
            throw new RuntimeException(e);
        }
    }

    public void removeFeature(ExtentTest extentFeature) {
        extentReports.removeTest(extentFeature);
    }

    public void createTestCase(TestData testData) {
        String scenarioName = testData.scenario.getName();
        String featureName = FilenameUtils.getBaseName(testData.scenario.getUri().toString());

        Collection<String> tags = testData.testTags;

        if (extentFeature == null || extentFeature.getModel().getName() == featureName) {
            this.createFeature(featureName);
        }

        this.createScenario(scenarioName);

        for (var tag : tags) {
            extentScenario.assignCategory(tag);
        }
    }

    public void createTestStep() {
        TestStepInfo testStep = TestData.getInstance().currentTestStep;
        try {
            extentStep = extentScenario.createNode(new GherkinKeyword(testStep.gherkinKeyword), testStep.description);
        } catch (ClassNotFoundException e) {
            throw new RuntimeException(e);
        }
    }

    public static void takeAScreenshot(String screenshotPath) {
        try {
            extentStep.addScreenCaptureFromPath(screenshotPath);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    public static void setTestStepOutcome(TestStepInfo testStepInfo) {
        if (testStepInfo.status.equals(TestState.PASSED)) {
            extentStep.pass("==> Passed");
        } else if (testStepInfo.status.equals(TestState.FAILED)) {
            extentStep.fail("==> Failed: " + testStepInfo.stepException);
            takeAScreenshot(TestData.getInstance().currentScreenshotLocation);
        } else {
            extentStep.skip("Skipping this step because its status is: " + testStepInfo.status);
        }
    }
}