package base;

import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

public class ConfigurationReader {
    public static String getValue(String propertyName) {
        String configurationPath = "";
        try (InputStream input = new FileInputStream("src/test/resources/configurations/Configuration.properties")) {
            Properties properties = new Properties();
            properties.load(input);
            configurationPath = properties.getProperty(propertyName);
        } catch (IOException ex) {
            ex.printStackTrace();
        }

        return configurationPath;
    }
}
