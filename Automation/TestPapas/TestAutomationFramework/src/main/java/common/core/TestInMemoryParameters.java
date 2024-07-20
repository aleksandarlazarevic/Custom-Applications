package common.core;

import common.core.configuration.TestConfiguration;
import io.restassured.response.Response;
import org.openqa.selenium.remote.RemoteWebDriver;

import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.List;

public class TestInMemoryParameters {
    // region Fields and Properties
    private static TestInMemoryParameters instance = null;
    public boolean multipleBrowserInstances;
    public String elementTimeout;
    public String pageLoadTimeout;
    public RemoteWebDriver driver;
    public String url;
    private String testResultsDirectory = "";
    private String apiEndpoint;
    private TestConfiguration testConfiguration;
    private String testRunParametersLocation;
    private String rootTestDirectory;
    private Response currentApiResponse;
    private ResultSet currentSqlResponse;

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

    public void setCurrentApiResponse(Response currentApiResponse) {
        this.currentApiResponse = currentApiResponse;
    }    
    
    public Response getCurrentApiResponse() {
        return currentApiResponse;
    }

    public ResultSet getCurrentSqlResponse() {
        return currentSqlResponse;
    }

    public void setCurrentSqlResponse(ResultSet currentSqlResponse) {
        this.currentSqlResponse = currentSqlResponse;
    }

    // endregion
}
