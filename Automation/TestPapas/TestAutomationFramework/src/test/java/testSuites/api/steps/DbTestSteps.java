package testSuites.api.steps;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.mongodb.MongoClient;
import com.mongodb.client.FindIterable;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;
import common.core.TestInMemoryParameters;
import common.utilities.JsonManager;
import common.utilities.LoggingManager;
import common.utilities.SQLManager;
import engines.apiTesting.RestApiManager;
import io.cucumber.java.en.And;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import io.restassured.response.Response;
import io.restassured.response.ResponseBody;
import org.bson.Document;
import org.junit.Assert;
import testSuites.api.models.ActivityObject;
import testSuites.web.steps.Utilities;
import uiMappings.pages.boredApi.BoredApiPage;

import java.util.*;
import java.util.stream.Collectors;

public class DbTestSteps extends Utilities {
    MongoClient dbClient;
    MongoDatabase mongoDb;
    MongoCollection dbCollection;
    List<ActivityObject> dbActivities = new ArrayList<ActivityObject>();

    private List<ActivityObject> listOfActivityObjectsFromApi;

    @Given("{string} database is used")
    public void databaseIsUsed(String databaseType) {
        runStep(this::setDatabase, databaseType);
    }

    private void setDatabase(String databaseType) {
        switch (databaseType) {
            case "MongoDB":
                dbClient = SQLManager.connectToMongoDb();
                break;
            default:
                throw new RuntimeException("Unknown database type: " + databaseType);
        }
    }

    @And("{string} database exists")
    public void databaseExists(String dbName) {
        runStep(this::checkIfDatabaseExists, dbName);
    }

    private void checkIfDatabaseExists(String dbName) {
        try {
            mongoDb = dbClient.getDatabase(dbName);
        } catch (Exception exception) {
            throw new RuntimeException(String.format("Unable to find database: %s - %s", dbName, exception.getMessage()));
        }
    }

    @Given("{string} database has required {string} collection data")
    public void databaseHasRequiredCollectionData(String databaseType, String collectionName) {
        runStep(this::ensureThatDbHasRequiredData, databaseType, collectionName);
    }

    private void ensureThatDbHasRequiredData(String databaseType, String collectionName) {
        ensureThatDbHasCollection(databaseType, collectionName);
        ensureThatCollectionHasData();
    }

    private void ensureThatDbHasCollection(String databaseType, String collectionName) {
        if (databaseType.equals("MongoDB")) {
            try {
                dbCollection = mongoDb.getCollection(collectionName);
            } catch (Exception exception) {
                LoggingManager.Info(String.format("Collection %s does not exist", collectionName));
                LoggingManager.Info(String.format("Creating collection: ", collectionName));
                mongoDb.createCollection(collectionName);
                LoggingManager.Info(String.format("Collection %s created successfully", collectionName));
                dbCollection = mongoDb.getCollection(collectionName);
            }
        }
    }

    private void ensureThatCollectionHasData() {
        try {
            FindIterable<Document> findIterable = dbCollection.find();
            Iterator<Document> iterator = findIterable.iterator();

            while (iterator.hasNext()) {
                Document document = iterator.next();
                LoggingManager.Info(document.toString());
                LoggingManager.Info("Participant Name = " + document.get("Participant"));
            }
        } catch (Exception exception) {
            LoggingManager.Info("Given collection has no data, inserting new data now");
            Random r = new Random();
            List<ActivityObject> activityList = new ArrayList<ActivityObject>();
            int[] fiveRandomNumbers = r.ints(5, 0, 5).toArray();

            for (int number : fiveRandomNumbers) {
                float randomPrice = r.nextFloat(0, 1);
                float randomAccessibility = r.nextFloat(0, 1);
                ActivityObject activity = new ActivityObject(number, randomPrice, randomAccessibility);
                activityList.add(activity);
            }

            dbCollection.insertMany(activityList);
            LoggingManager.Info("Data inserted successfully");
        }
    }

    @Given("Data is retrieved from {string} collection")
    public void dataIsRetrievedFromCollection(String collectionName) {
        runStep(this::retrieveDataFromCollection, collectionName);
    }

    private void retrieveDataFromCollection(String collectionName) {
        FindIterable<ActivityObject> findIterable = dbCollection.find();
        Iterator<ActivityObject> iterator = findIterable.iterator();

        while (iterator.hasNext()) {
            ActivityObject activity = iterator.next();
            LoggingManager.Info(activity.toString());
            LoggingManager.Info("Participants = " + activity.getParticipants());
            dbActivities.add(activity);
        }
    }

    @When("Get data from {string}")
    public void getDataFromAPI(String apiName) {
        if (apiName.equals("BoredAPI"))

            switch (apiName) {
                case "BoredAPI":
                    runStep(this::getDataFromBoredAPI);
                    break;
                default:
                    throw new RuntimeException("Unknown api: " + apiName);
            }
    }

    private void getDataFromBoredAPI() {
        String endpoint = "/activity/";
        Response response = RestApiManager.getData(endpoint);
        Assert.assertEquals("Data is NOT successfully retrieved", 200, response.getStatusCode());
        TestInMemoryParameters.getInstance().setCurrentApiResponse(response);
        ResponseBody responseBody = response.getBody();
        Gson gson = new Gson();
        listOfActivityObjectsFromApi = gson.fromJson(responseBody.toString(), ArrayList.class);
    }

    @Then("Compare api and database data")
    public void compareApiAndDatabaseData() {
        runStep(this::compareApiAndDbData);
    }

    private void compareApiAndDbData() {
        LoggingManager.Info("Started comparing api and db data");
        List<ActivityObject> matchedActivities = listOfActivityObjectsFromApi.stream().filter(x -> dbActivities.stream().
                anyMatch(y -> y.getKey().equals(x.getKey()))).collect(Collectors.toList());
        List<ActivityObject> unmatchedApiActivities = getUnmatchedElements(listOfActivityObjectsFromApi, dbActivities);
        List<ActivityObject> unmatchedDbActivities = getUnmatchedElements(dbActivities, listOfActivityObjectsFromApi);

        LoggingManager.Info("--Listing items present in db and api data");
        for (ActivityObject item : matchedActivities) {
            LoggingManager.Info(String.format("Item: %s is present in both db and api data", item));
            LoggingManager.Info(String.format("Item activity: %s", item.getActivity()));
            LoggingManager.Info(String.format("Item key: %s", item.getKey()));
        }

        LoggingManager.Info("--Listing items present in api data only");
        for (ActivityObject item : unmatchedApiActivities) {
            LoggingManager.Info(String.format("Item: %s is present api data only", item));
            LoggingManager.Info(String.format("Item activity: %s", item.getActivity()));
            LoggingManager.Info(String.format("Item key: %s", item.getKey()));
        }

        LoggingManager.Info("--Listing items present in db only");
        for (ActivityObject item : unmatchedDbActivities) {
            LoggingManager.Info(String.format("Item: %s is present in db only", item));
            LoggingManager.Info(String.format("Item activity: %s", item.getActivity()));
            LoggingManager.Info(String.format("Item key: %s", item.getKey()));
        }
    }

    private List<ActivityObject> getUnmatchedElements(List<ActivityObject> listOfActivityObjectsFromApi, List<ActivityObject> dbActivities) {
        Set<ActivityObject> set1 = new HashSet<>(listOfActivityObjectsFromApi);
        Set<ActivityObject> set2 = new HashSet<>(dbActivities);

        set1.removeAll(set2);
        set2.removeAll(set1);
        set1.addAll(set2);

        return set1.stream().toList();
    }

    @Then("Compare value from Activity object element with value in Response")
    public void compareValueFromActivityObjectElementWithValueInResponse() {
        runStep(this::compareUiAndDbValues);
    }

    private void compareUiAndDbValues() {
        String uiActivityValue = getPage(BoredApiPage.class).getActivityElementData();
        JsonObject jsonBody = JsonManager.stringToJson(uiActivityValue);
        ActivityObject uiActivity = JsonManager.jsonToObject(jsonBody, ActivityObject.class);

        LoggingManager.Info("Started comparing ui and db data");
        boolean isUiActivityinTheDb = dbActivities.stream().map(p -> p.getKey().equals(uiActivity.getKey())).findAny().get();

        if (isUiActivityinTheDb) {
            LoggingManager.Info(String.format("Item: %s is present in both db and ui data", uiActivity));
            LoggingManager.Info(String.format("Item activity: %s", uiActivity.getActivity()));
            LoggingManager.Info(String.format("Item key: %s", uiActivity.getKey()));
        } else {
            LoggingManager.Info(String.format("Item: %s is NOT present in both db and ui data", uiActivity));
            LoggingManager.Info(String.format("Item activity: %s", uiActivity.getActivity()));
            LoggingManager.Info(String.format("Item key: %s", uiActivity.getKey()));
        }
    }
}
