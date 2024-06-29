package common.core;

import common.core.configuration.TestConfiguration;
import org.openqa.selenium.remote.RemoteWebDriver;

import java.util.ArrayList;
import java.util.List;

public class TestInMemoryParameters {
    // region Fields and Properties
    private static TestInMemoryParameters instance = null;
    public boolean multipleBrowserInstances;
    public String elementTimeout;
    public String pageLoadTimeout;
    public RemoteWebDriver driver;
    public String emailAddress;
    public String emailServiceName;
    public String emailServiceUrl;
    public List<EmailServiceParameters> emailServices = new ArrayList<EmailServiceParameters>();
    public String generatedEmailAddress;
    public String emailSender;
    public String emailText;
    public String verificationSenderAddress;
    public String verificationCode;
    public String url;

    private String testResultsDirectory = "";
    private String apiEndpoint;
    private TestConfiguration testConfiguration;
    private String testRunParametersLocation;
    private String rootTestDirectory;

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

    public String getApiEndpoint() {
        return apiEndpoint;
    }

    public void setApiEndpoint(String apiEndpoint) {
        this.apiEndpoint = apiEndpoint;
    }

    public TestConfiguration getTestConfiguration() {
        return testConfiguration;
    }

    public void setTestConfiguration(TestConfiguration testConfiguration) {
        this.testConfiguration = testConfiguration;
    }

    public String getTestRunParametersLocation() {
        return testRunParametersLocation;
    }

    public void setTestRunParametersLocation(String testRunParametersLocation) {
        this.testRunParametersLocation = testRunParametersLocation;
    }

    public String getRootTestDirectory() {
        return rootTestDirectory;
    }

    public void setRootTestDirectory(String rootTestDirectory) {
        this.rootTestDirectory = rootTestDirectory;
    }

    // endregion
}
