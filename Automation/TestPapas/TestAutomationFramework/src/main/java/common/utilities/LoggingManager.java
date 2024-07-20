package common.utilities;

import common.core.TestInMemoryParameters;
import common.core.tests.TestData;
import common.core.tests.TestState;
import common.core.tests.TestStepInfo;
import org.apache.log4j.BasicConfigurator;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.apache.logging.log4j.core.LoggerContext;
import org.apache.logging.log4j.core.appender.FileAppender;
import org.apache.logging.log4j.core.config.Configuration;
import org.apache.logging.log4j.core.config.LoggerConfig;
import org.apache.logging.log4j.core.layout.PatternLayout;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.time.Duration;
import java.time.LocalDateTime;
import java.util.Date;
import java.util.List;

public class LoggingManager {
    private static Logger logger = LogManager.getLogger(LoggingManager.class);
    private static FileAppender fileAppender;

    public static void Info(String message) {
        logger.info(message);
    }

    public static void Error(String message) {
        logger.error(message);
    }

    public static void Warn(String message) {
        logger.warn(message);
    }

    public static void Debug(String message) {
        logger.debug(message);
    }

    public static void Fatal(String message) {
        logger.fatal(message);
    }

    public static void beginStep(TestStepInfo stepInfo, int stepCount) {
        logger.info("@");
        logger.info("-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->");
        logger.info(">>[Step]");
        logger.info(">>Step Number: " + stepCount);
        logger.info(">>Test Step Description: " + stepInfo.description);
        logger.info(">>Step Start Time: " + stepInfo.startTime.toString());
        logger.info("-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->-->");
        logger.info("@");
    }

    public static void endStep(TestStepInfo stepInfo) {
        stepInfo.endTime = LocalDateTime.now();
        long diff = Duration.between(stepInfo.startTime, stepInfo.endTime).toMillis();
        String timeDifference = getDisplayableTimeFormat(diff);
        logger.info("@");
        logger.info("--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<");

        if (stepInfo.status == TestState.FAILED) {
            logger.info("<<Test Step Status: " + stepInfo.status);
            logger.info("<<Impact Level: " + stepInfo.level);
        } else {
            logger.info("<<Test Step Status: " + stepInfo.status);
        }

        logger.info("<<Step Duration [s]: " + timeDifference);
        logger.info("--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<--<");
        logger.info("@");
    }

    public static void stepException(TestStepInfo stepInfo) {
        logger.info("[EXCEPTION]");
        logger.info("Message: " + stepInfo.stepException.getMessage());
        logger.info("Stack Trace: " + stepInfo.stepException.getStackTrace());
    }

    public static void beginTestExecution(TestData testData) {
        createNewLogFile();
        TestData.getInstance().testData.startTime = LocalDateTime.now();

        logger.info("========================================================================");
        logger.info("$$ [Test Case]");
        logger.info("$$ Test Case Name: " + testData.testDescription);
        logger.info("$$ Test Case ID: " + testData.testId);
        logger.info("========================================================================");
    }

    public static void endTestExecution(TestData testData) {
        testData.endTime = LocalDateTime.now();
        long diff = Duration.between(testData.startTime, testData.endTime).toMillis();
        String timeDifference = getDisplayableTimeFormat(diff);
        int numberOfTestSteps = (int) testData.testSteps.stream().count();

        if (testData.testException != null) {
            logger.info("[EXCEPTION]");
            logger.info("Message: " + testData.stepException.getMessage());
            logger.info("Stack Trace: " + testData.stepException.getStackTrace());
        }

        logger.info("========================================================================");
        logger.info("Number of executed test steps in the test scenario: " + numberOfTestSteps);
        logger.info("========================================================================");
        logger.info("Executed test steps:");
        for (int i = 0; i < numberOfTestSteps; i++) {
            List<TestStepInfo> testStep = testData.testSteps;
            logger.info(testStep.get(i).description);
        }
        logger.info("========================================================================");
        logger.info("** Test Case Duration [s]: " + timeDifference);
        logger.info("** Test Case Status: " + testData.status);
        logger.info("========================================================================");

        fileAppender.stop();
    }

    private static String getDisplayableTimeFormat(long diff) {
        var hours = diff / 3600000;
        var minutes = (diff % 3600000) / 60000;
        var seconds = ((diff % 3600000) % 60000) / 1000;
        var millis = ((diff % 3600000) % 60000) % 1000;
        return String.format("%d:%02d:%02d.%02d", hours, minutes, seconds, millis);
    }

    private static void createNewLogFile() {
        BasicConfigurator.resetConfiguration();
        System.setProperty("logFileLocation", TestInMemoryParameters.getInstance().getTestResultsDirectory());
        DateFormat dateFormat = new SimpleDateFormat("dd-MM-yyyy_hh_mm_ssaa");
        LoggerContext context = (LoggerContext) LogManager.getContext(false);
        Configuration cfg = context.getConfiguration();

        fileAppender = FileAppender.newBuilder()
                .withFileName(TestInMemoryParameters.getInstance().getTestResultsDirectory() + "testRun-" + dateFormat.format(new Date()) + ".log")
                .withAppend(false)
                .withLocking(false)
                .withLayout(PatternLayout.newBuilder().withPattern("%-5p %d  [%t] %C{2} (%F:%L) - %m%n").build())
                .setName("TestLog-" + System.currentTimeMillis())
                .setConfiguration(cfg)
                .build();

        fileAppender.start();
        cfg.addAppender(fileAppender);
        LoggerConfig loggerConfig = cfg.getLoggerConfig("LoggingManager");
        loggerConfig.addAppender(fileAppender, null, null);
        context.updateLoggers();
    }
}
