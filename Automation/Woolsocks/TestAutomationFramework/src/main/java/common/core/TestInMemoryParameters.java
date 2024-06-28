package common.core;

import org.openqa.selenium.remote.RemoteWebDriver;

public class TestInMemoryParameters {
    // region Fields and Properties
    private static TestInMemoryParameters instance = null;
    public boolean multipleBrowserInstances;
    public String elementTimeout;
    public String pageLoadTimeout;
    public RemoteWebDriver driver;

    private String testResultsDirectory = "";

    // endregion
    // region Methods
    // Get thread safe instance
    public static TestInMemoryParameters getInstance() {
        if (instance == null) {
            synchronized (TestInMemoryParameters.class) {
                if (instance == null) {
                    instance = new TestInMemoryParameters();
                }
            }
        }
        return instance;
    }

    TestInMemoryParameters() {
        this.multipleBrowserInstances = false;
        this.elementTimeout = "60";
        this.pageLoadTimeout = "60";
    }

    public String getTestResultsDirectory() {
        return this.testResultsDirectory;
    }

    public void setTestResultsDirectory(String testResultsDirectory) {
        this.testResultsDirectory = testResultsDirectory;
    }
    // endregion
}
