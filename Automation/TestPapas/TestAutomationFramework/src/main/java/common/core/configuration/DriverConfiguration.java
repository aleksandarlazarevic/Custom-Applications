package common.core.configuration;

import java.net.URL;

public class DriverConfiguration {
    // region Fields
    public String name;
    public URL baseUrl;
    public String timeout;
    public String appVersion;
    public String commandTimeOutInMinutes;
    public String driverPath;
    public String driverType;
    public String testEnvironment;
    public String apiKey;
    // endregion
    // region Getters and Setters
    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public URL getBaseUrl() {
        return baseUrl;
    }

    public void setBaseUrl(URL baseUrl) {
        this.baseUrl = baseUrl;
    }

    public String getTimeout() {
        return timeout;
    }

    public void setTimeout(String timeout) {
        this.timeout = timeout;
    }

    public String getCommandTimeOutInMinutes() {
        return commandTimeOutInMinutes;
    }

    public void setCommandTimeOutInMinutes(String commandTimeOutInMinutes) {
        this.commandTimeOutInMinutes = commandTimeOutInMinutes;
    }
    public String getDriverPath() {
        return driverPath;
    }

    public void setDriverPath(String driverPath) {
        this.driverPath = driverPath;
    }

    public String getDriverType() {
        return driverType;
    }

    public void setDriverType(String driverType) {
        this.driverType = driverType;
    }

    public String getAppVersion() {
        return appVersion;
    }

    public void setAppVersion(String appVersion) {
        this.appVersion = appVersion;
    }

    public String getTestEnvironment() { return testEnvironment; }

    public void setTestEnvironment(String testEnvironment) { this.testEnvironment = testEnvironment; }

    public String getApiKey() {
        return apiKey;
    }
    // endregion
}
