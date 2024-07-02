package common.core.contracts;

import common.core.tests.Priority;
import common.core.tests.TestState;

import java.time.Duration;
import java.time.LocalDateTime;

public interface IStepInfo {
    String description = "";
    boolean skipStep = false;
    Priority level = null;
    boolean skipStepOnFailure = false;
    boolean failsIteration = false;
    TestState status = null;
    LocalDateTime startTime = null;
    LocalDateTime endTime = null;
    Duration duration = null;
    Exception stepException = null;
    boolean isMandatory = false;
}
