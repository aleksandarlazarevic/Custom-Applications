package common.core.tests;

import java.time.Duration;
import java.time.LocalDateTime;

public class TestStepInfo {
    // region Fields and Properties
    public String description;
    public boolean skipStep;
    public Priority level;
    public boolean skipStepOnFailure;
    public boolean failsIteration;
    public LocalDateTime startTime;
    public LocalDateTime endTime;
    public Duration duration;
    public Exception stepException;
    public boolean isMandatory;
    public TestState status;
    public String gherkinKeyword;
    // endregion

    public TestStepInfo() {
        stepInit();
    }

    public TestStepInfo(String description) {
        stepInit();
        this.description = description;
    }

    public TestStepInfo(String description, Priority level) {
        stepInit();
        this.description = description;
        this.level = level;
    }

    public TestStepInfo(String description, boolean skipStepOnFailure) {
        stepInit();
        this.description = description;
        this.skipStepOnFailure = skipStepOnFailure;
    }

    public TestStepInfo(String description, boolean skipStepOnFailure, Priority level) {
        stepInit();
        this.description = description;
        this.skipStepOnFailure = skipStepOnFailure;
        this.level = level;
    }

    public TestStepInfo(String description, boolean skipStepOnFailure, Priority level, boolean isMandatory) {
        stepInit();
        this.description = description;
        this.skipStepOnFailure = skipStepOnFailure;
        this.level = level;
        this.isMandatory = isMandatory;
    }

    public TestStepInfo(String description, boolean skipStepOnFailure, boolean skipStep) {
        stepInit();
        this.description = description;
        this.skipStepOnFailure = skipStepOnFailure;
        this.skipStep = skipStep;
    }

    public TestStepInfo(String description, boolean skipStepOnFailure, boolean skipStep, Priority level) {
        stepInit();
        this.description = description;
        this.skipStepOnFailure = skipStepOnFailure;
        this.skipStep = skipStep;
        this.level = level;
    }

    protected void stepInit() {
        description = "Description not defined";
        skipStep = false;
        skipStepOnFailure = true;
        failsIteration = true;
        level = Priority.HIGH;
        status = TestState.UNKNOWN;
    }
}
