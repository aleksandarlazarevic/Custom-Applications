package testSuites.api.steps;

import com.google.gson.JsonObject;
import common.core.TestInMemoryParameters;
import common.utilities.JsonManager;
import common.utilities.SQLManager;
import engines.apiTesting.RestApiManager;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import io.restassured.RestAssured;
import io.restassured.response.Response;
import io.restassured.response.ResponseBody;
import org.junit.Assert;
import testSuites.api.models.DogImage;
import testSuites.api.models.ImageData;
import testSuites.shared.BaseUtilities;

import java.sql.ResultSet;
import java.util.List;

public class CommonAPITestSteps extends BaseUtilities {
    private String imageId;

    @Given("{string} testing API is used")
    public void givenTestingAPIIsUsed(String apiName) {
        runStep(this::setTestingApiParameters, apiName);
    }

    private void setTestingApiParameters(String apiName) {
        String baseURI = "";
        String basePath = "";

        switch (apiName) {
            case "TheDogAPI":
                baseURI = "https://api.thecatapi.com";
                basePath = "/v1";
                break;
            case "BoredAPI":
                baseURI = "https://bored.api.lewagon.com";
                basePath = "/api";
                break;
            default:
                throw new RuntimeException(String.format("API named %s is unknown", apiName));
        }
        RestAssured.baseURI = baseURI;
        RestAssured.basePath = basePath;
        TestInMemoryParameters.getInstance().setApiEndpoint(RestAssured.baseURI);
    }

    @When("Obtain random image id")
    public void getRandomImageId() {
        runStep(this::getImageId);
    }

    private void getImageId() {
        String endpoint = "/images/search/";
        Response response = RestApiManager.getData(endpoint);
        Assert.assertEquals("Image ID is NOT successfully retrieved", 200, response.getStatusCode());
        TestInMemoryParameters.getInstance().setCurrentApiResponse(response);
        ResponseBody responseBody = response.getBody();
        JsonObject jsonBody = JsonManager.stringToJson(responseBody.toString());
        DogImage imageData = JsonManager.jsonToObject(jsonBody, DogImage.class);
        imageId = imageData.getId();
    }

    @When("Save obtained image as favorite")
    public void saveObtainedImageAsFavorite() {
        runStep(this::saveAsFavorite);
    }

    private void saveAsFavorite() {
        String endpoint = "/favourites";
        ImageData imageData = new ImageData(imageId, "user-123");
        String jsonStringBody = JsonManager.objectToJson(imageData);
        Response response = RestApiManager.postDataWithAuth(endpoint, jsonStringBody);
        TestInMemoryParameters.getInstance().setCurrentApiResponse(response);
    }

    @Then("Image is successfully saved as favorite")
    public void imageIsSuccessfullySavedAsFavorite() {
        Response currentResponse = TestInMemoryParameters.getInstance().getCurrentApiResponse();
        Assert.assertEquals("Image is NOT successfully saved as favorite", 200, currentResponse.getStatusCode());
    }


    @When("Get data from the database table {string}")
    public void getDataFromTheDatabaseTable(String tableName) {
        String mssqlServerInstance = "DESKTOP-FNUTOUI\\SQLEXPRESS";
//        "mongodb://localhost:27017"
        String connectionString = String.format("Server=%s;Database=%s;User Id=test;Password=test;", mssqlServerInstance, tableName);
        String query = "SELECT * FROM " + tableName;
        ResultSet response = SQLManager.executeQuery(connectionString, query);
        TestInMemoryParameters.getInstance().setCurrentSqlResponse(response);
    }
}
