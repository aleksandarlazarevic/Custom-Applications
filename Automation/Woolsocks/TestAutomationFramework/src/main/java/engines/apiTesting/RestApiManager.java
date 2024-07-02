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

    public static Response getData(String endpoint) {
        RestAssured.baseURI = TestInMemoryParameters.getInstance().getApiEndpoint();
        Response response = given()
                .contentType(ContentType.JSON)
                .when()
                .get(endpoint)
                .then()
                .extract().response();

        return response;
    }
    // endregion
    // region POST requests
    public static Response postData(String endpoint, String requestBody) {
        RestAssured.baseURI = TestInMemoryParameters.getInstance().getApiEndpoint();

        Response response = given()
                .contentType(ContentType.JSON)
                .and()
                .body(requestBody)
                .when()
                .post(endpoint)
                .then()
                .extract().response();

        return response;
    }
    // endregion
    // region PUT requests
    public static Response putData(String endpoint, String newBody) {
        RestAssured.baseURI = TestInMemoryParameters.getInstance().getApiEndpoint();
        Response response = given()
                .contentType(ContentType.JSON)
                .body(newBody)
                .when()
                .put(endpoint)
                .then()
                .extract().response();

        return response;
    }
    // endregion
    // region DELETE requests
    public static Response deleteData(String endpoint) {
        RestAssured.baseURI = TestInMemoryParameters.getInstance().getApiEndpoint();
        Response response = given()
                .when()
                .delete(endpoint)
                .then()
                .extract().response();

        return response;
    }
    // endregion
    // endregion
}
