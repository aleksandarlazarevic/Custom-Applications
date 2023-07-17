namespace CommonCore.Contracts
{
    public interface IEngineManager
    {
        void StartUp();
        void ShutDown();
        string CollectData(string destinationPath, string eventName);
    }
}
