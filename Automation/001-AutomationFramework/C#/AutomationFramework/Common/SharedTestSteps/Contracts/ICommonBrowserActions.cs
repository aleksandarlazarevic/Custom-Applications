using SeleniumEngine.DriverInitialization.Browsers;

namespace SharedTestSteps.Contracts
{
    public interface ICommonBrowserActions
    {
        void OpenUrl(string url);
        void OpenBrowserAndGoToDefaultUrl();
        void CloseBrowser();
        void GoToDefaultEmailService();
        void GoToSpecificEmailService(string emailService);
        void SwitchToSpecificBrowserInstance(BrowserInstance instance);
        void CloseCurrentTab();
        void OpenNewTab();
        void AcceptAlertPopUp();
        void SwitchToLastBrowserWindow();
        void SwitchToFirstBrowserWindow();
        void SwitchToSpecificBrowserWindow(int windowNumber);
    }
}
