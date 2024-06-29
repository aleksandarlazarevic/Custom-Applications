package engines.apiTesting;

import common.core.TestInMemoryParameters;
import common.utilities.LoggingManager;
import io.restassured.RestAssured;
import io.restassured.http.ContentType;
import io.restassured.response.Response;
import io.restassured.specification.RequestSpecification;

import java.io.*;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.HashMap;
import java.util.Map;

import static io.restassured.RestAssured.given;
import static java.net.URLEncoder.encode;

public class RestApiManager {
    // region methods
    // region GET requests
    public static Response getDataFullUrl(String fullUrl) {
        RestAssured.baseURI = fullUrl;
        Response response = given()
                .contentType(ContentType.JSON)
                .when()
                .get()
                .then()
                .extract().response();

        return response;
    }
    // endregion
    // endregion
}
