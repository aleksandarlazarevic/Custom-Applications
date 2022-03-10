using Automation.WebDriver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedSteps
{
    public class GlobalSteps
    {
        public static void OpenBrowser()
        {
            try
            {
                UIDriver.InitializeWebDriver();
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred: ", ex);
            }
        }

        public static void CloseBrowser()
        {
            UIDriver.DisposeWebDriver();
        }

        public static void GoToUrl(string url)
        {
            try
            {
                UIDriver.WebDriver.Navigate().GoToUrl(url);
            }
            catch
            {
                throw new Exception(string.Format("Cannot navigate to URL [{0}]", url));
            }
        }
    }
}
