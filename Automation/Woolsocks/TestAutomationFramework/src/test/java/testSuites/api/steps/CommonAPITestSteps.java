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
import testSuites.api.models.GoogleTest;
import testSuites.api.models.ProcessInfo;
import testSuites.shared.BaseUtilities;

import java.sql.ResultSet;
import java.util.List;

public class CommonAPITestSteps extends BaseUtilities {
    private String newName;
    private String newDescription;

    @Given("Google testing API is used")
    public void googleTestingAPIIsUsed() {
        runStep(this::setGoogleTestingApiParameters);
    }

    private void setGoogleTestingApiParameters() {
        RestAssured.baseURI = "https://testing.googleapis.com";
        RestAssured.basePath = "/v1";
        TestInMemoryParameters.getInstance().setApiEndpoint(RestAssured.baseURI);

    }

    @When("Create a test with a name of {string} and description of {string}")
    public void createATest(String name, String description) {
        String endpoint = "/tests";
        GoogleTest test = new GoogleTest(name, description);
        String jsonStringBody = JsonManager.objectToJson(test);
        Response response = RestApiManager.postData(endpoint, jsonStringBody);
        TestInMemoryParameters.getInstance().setCurrentApiResponse(response);
    }

    @Then("Test is successfully created")
    public void testIsSuccessfullyCreated() {
        Response currentResponse = TestInMemoryParameters.getInstance().getCurrentApiResponse();
        Assert.assertEquals("Test was not successfully created", 201, currentResponse.getStatusCode());
    }

    @When("Get a test with an id of {string}")
    public void getATest(String id) {
        String endpoint = "/tests/" + id;
        Response response = RestApiManager.getData(endpoint);
        TestInMemoryParameters.getInstance().setCurrentApiResponse(response);
    }

    @Then("Test is successfully retrieved")
    public void testIsSuccessfullyRetrieved() {
        Response currentResponse = TestInMemoryParameters.getInstance().getCurrentApiResponse();
        Assert.assertEquals("Test was not successfully retrieved", 200, currentResponse.getStatusCode());
    }

    @When("Update a test with an id of {string} with name of {string} and description of {string}")
    public void updateATest(String id, String newName, String newDescription) {
        this.newName = newName;
        this.newDescription = newDescription;
        String endpoint = "/tests/" + id;
        GoogleTest test = new GoogleTest(newName, newDescription);
        String jsonStringBody = JsonManager.objectToJson(test);
        Response response = RestApiManager.putData(endpoint, jsonStringBody);
        TestInMemoryParameters.getInstance().setCurrentApiResponse(response);
    }

    @Then("Test is successfully updated")
    public void testIsSuccessfullyUpdated() {
        Response currentResponse = TestInMemoryParameters.getInstance().getCurrentApiResponse();
        Assert.assertEquals("Test was not successfully updated", 200, currentResponse.getStatusCode());
        ResponseBody responseBody = currentResponse.getBody();
        JsonObject jsonBody= JsonManager.stringToJson(responseBody.toString());
        GoogleTest updatedTest = JsonManager.jsonToObject(jsonBody, GoogleTest.class);
        Assert.assertEquals("Test name was not successfully updated", this.newName, updatedTest.getName());
        Assert.assertEquals("Test description was not successfully updated", this.newDescription, updatedTest.getDescription());
    }

    @When("Delete a test with an id of {string}")
    public void deleteATest(String id) {
        String endpoint = "/tests/" + id;
        Response response = RestApiManager.deleteData(endpoint);
        TestInMemoryParameters.getInstance().setCurrentApiResponse(response);
    }

    @Then("Test is successfully deleted")
    public void testIsSuccessfullyDeleted() {
        Response currentResponse = TestInMemoryParameters.getInstance().getCurrentApiResponse();
        Assert.assertEquals("Test was not successfully deleted", 204, currentResponse.getStatusCode());
    }

    @When("Get data from the database table {string}")
    public void getDataFromTheDatabaseTable(String tableName) {
        String mssqlServerInstance = "DESKTOP-FNUTOUI\\SQLEXPRESS";
        String connectionString = String.format("Server=%s;Database=%s;User Id=test;Password=test;", mssqlServerInstance, tableName);
        String query = "SELECT * FROM " + tableName;
        ResultSet response = SQLManager.executeQuery(connectionString, query);
        TestInMemoryParameters.getInstance().setCurrentSqlResponse(response);
    }

    @Then("Data is successfully retrieved")
    public void dataIsSuccessfullyRetrieved() {
        ResultSet currentSqlResponse = TestInMemoryParameters.getInstance().getCurrentSqlResponse();
        List<ProcessInfo> responseObjects = SQLManager.resultSetToObject(currentSqlResponse, ProcessInfo.class);
        for (ProcessInfo object: responseObjects) {
            Assert.assertEquals("CPU usage unexpected", object.getCpu_busy(), 30);

        }
    }
}
