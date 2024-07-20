package common.core.configuration;

import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;

import common.utilities.JsonManager;
import com.google.gson.JsonObject;
import org.apache.commons.beanutils.BeanUtils;

public class ConfigurationManager {
    public static <T> T readConfigurationFile(String configFilePath, Class<T> configType) {
        T configuration;
        String configurationFile = null;

        try {
            configurationFile = new String(Files.readAllBytes(Paths.get(configFilePath)), StandardCharsets.UTF_8);
        } catch (IOException e) {
            throw new RuntimeException(
                    String.format("Unable to read configuration file: %s - %s", configFilePath, e.getMessage()));
        }

        JsonObject jsonConfig = JsonManager.stringToJson(configurationFile);
        configuration = JsonManager.jsonToObject(jsonConfig, configType);
        return configuration;
    }

    public static <T> T getTestConfigValue(TestConfiguration configuration, String propertyName) {
        T propertyValue = (T) new Object();
        for (DriverConfiguration capability : configuration.systemConfiguration.capabilities) {
            try {
                propertyValue = (T) BeanUtils.getProperty(capability, propertyName);
            } catch (Exception e) {
                throw new RuntimeException(
                        String.format("Unable to get configuration value for property: %s - %s",
                                propertyName, e.getMessage()));
            }
        }

        return propertyValue;
    }
}
