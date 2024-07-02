package common.utilities;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import io.restassured.path.json.JsonPath;

import java.io.BufferedReader;
import java.io.IOException;
import java.util.LinkedHashMap;
import java.util.List;

public class JsonManager {
    private static final Gson gson = new Gson();

    public static JsonObject stringToJson(String jsonString) {
        JsonObject jsonObj;

        if (!isValidJson(jsonString)) {
            throw new RuntimeException("Not a valid JSON");
        }

        try {
            jsonObj = JsonParser.parseString(jsonString).getAsJsonObject();
        } catch (Exception exception) {
            throw new RuntimeException("Unable to parse string to json: " + exception.getMessage());
        }

        return jsonObj;
    }

    public static <T> T jsonToObject(JsonObject json, Class<T> objectType) {
        T returnObject;

        try {
            returnObject = gson.fromJson(json, objectType);
        } catch (Exception exception) {
            throw new RuntimeException("Unable to parse json to object: " + exception.getMessage());
        }

        return returnObject;
    }

    public static String objectToJson(Object object) {
        String returnObject;

        try {
            returnObject = new Gson().toJson(object);
        } catch (Exception exception) {
            throw new RuntimeException("Unable to parse object to json: " + exception.getMessage());
        }

        return returnObject;
    }

    public static boolean isValidJson(String stringToCheck) {
        try {
            gson.fromJson(stringToCheck, Object.class);
            return true;
        } catch (com.google.gson.JsonSyntaxException ex) {
            return false;
        }
    }
}
