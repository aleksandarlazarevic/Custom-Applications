package common.utilities;

import engines.apiTesting.RestApiManager;
import io.restassured.response.Response;
import org.apache.http.HttpStatus;
public class WebsiteManager {
    public static boolean isWebsiteAvailable(String url) {
        try {
            Response response = RestApiManager.getDataFullUrl(url);
            int statusCode = response.getStatusCode();

            return statusCode == HttpStatus.SC_OK;
        } catch (Exception exception) {
            return false;
        }
    }
}
