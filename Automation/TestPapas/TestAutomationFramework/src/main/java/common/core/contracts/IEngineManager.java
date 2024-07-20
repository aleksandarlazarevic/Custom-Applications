package common.core.contracts;

public interface IEngineManager {
    void startUp();

    void shutDown();

    String collectData(String destinationPath, String eventName);
}
