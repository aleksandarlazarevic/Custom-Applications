using SeleniumCore.Enums;

namespace CommonTestSteps.Contracts
{
    public interface IGlobalTestSteps
    {
        void GoToUrl(string url);
        void OpenBrowser();
        void CloseBrowser();
        void SwitchToBrowserInstance(BrowserInstance instance);
        void CloseCurrentTab();
        void OpenNewTab();
        void SwitchToLastBrowserWindow();
        void SwitchToFirstBrowserWindow();
        void SwitchToBrowserWindowByOrder(int windowNumber);
    }
}
