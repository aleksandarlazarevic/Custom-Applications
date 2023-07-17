﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumEngine.Base;
using System.Drawing;
using System.Drawing.Imaging;
using SeleniumExtras.PageObjects;
using SeleniumEngine.Instantiators;
using OpenQA.Selenium.Remote;
using System.Collections.Specialized;
using System;
using System.Configuration;
using CommonCore.Tests;
using Utilities;

namespace SeleniumEngine.DriverInitialization
{
    public static class WebDriverFactory
    {
        #region Fields and Properties
        private static TimeSpan _elementTimeout;
        private static TimeSpan _pageLoadTimeout;
        private static int _screenShot = 0;
        public static IWebDriver WebDriver { get; set; }
        public static IWebDriver PopupWebDriver { get; set; }
        public static string MainWindowHandle { get; set; }
        public static string PopupWindowHandle { get; set; }
        public static List<IWebDriver> webDrivers = new List<IWebDriver>();
        #endregion

        #region Methods
        public static void InitializeWebDriver(string driverType, uint elementTimeout = 60, uint pageLoadTime = 240)
        {
            try
            {
                _elementTimeout = TimeSpan.FromSeconds(elementTimeout);
                _pageLoadTimeout = TimeSpan.FromSeconds(pageLoadTime);
                //NameValueCollection caps = ConfigurationManager.GetSection("capabilities/" + profile) as NameValueCollection;
                //NameValueCollection settings = ConfigurationManager.GetSection("environments/" + environment) as NameValueCollection;
                //Console.WriteLine(caps);
                //DesiredCapabilities capability = new DesiredCapabilities();

                //foreach (string key in caps.AllKeys)
                //{
                //    capability.SetCapability(key, caps[key]);
                //}

                //foreach (string key in settings.AllKeys)
                //{
                //    capability.SetCapability(key, settings[key]);
                //}
                WebDriver = ObjectInstantiator.Container.Resolve<IBrowserDriver>(driverType).Initialize();
                WebDriver.Manage().Window.Maximize();
                webDrivers.Add(WebDriver);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Unable to initialize {0} WebDriver: {1}", driverType, ex.Message));
            }
        }

        public static void CleanUp()
        {
            if (WebDriver != null)
            {
                WebDriver.Quit();
            }

            WindowsProcessManager.KillProcess(TestInMemoryParameters.Instance.DriverType);
        }

        public static void InitializePopupDriver(IWebElement element)
        {
            try
            {
                MainWindowHandle = WebDriver.CurrentWindowHandle;
                PopupWindowFinder finder = new PopupWindowFinder(WebDriver, _elementTimeout);
                PopupWindowHandle = finder.Click(element);
                Thread.Sleep(7000);
                PopupWebDriver = WebDriver.SwitchTo().Window(PopupWindowHandle);
            }
            catch (Exception ex)
            {
                throw new Exception("Not able to initialize PopUp Driver", ex);
            }
        }

        public static T GetPage<T>(this IWebDriver webDriver, bool takeNewTimeouts = false, uint timeoutInSeconds = 120, uint pageLoadTimeout = 300) where T : BasePage
        {
            T? page = (T)Activator.CreateInstance(typeof(T));

            PageFactory.InitElements(webDriver, page);

            if (!takeNewTimeouts)
            {
                WebDriver.Manage().Timeouts().PageLoad = _pageLoadTimeout;
                WebDriver.Manage().Timeouts().ImplicitWait = _elementTimeout;
            }
            else
            {
                WebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(pageLoadTimeout);
                WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            }

            return page;
        }

        public static void TakeScreenshot()
        {
            try
            {
                string screenshotFolderName = "Screenshots";

                if (!Directory.Exists(screenshotFolderName))
                {
                    Directory.CreateDirectory(screenshotFolderName);
                }

                string fileName = string.Format("{0}.{1}", ++_screenShot, ScreenshotImageFormat.Jpeg);
                string path = Path.Combine(screenshotFolderName, fileName);

                if (WebDriver != null)
                {
                    WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                }

                using var bitmap = new Bitmap(1920, 1080);
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(0, 0, 0, 0,
                    bitmap.Size, CopyPixelOperation.SourceCopy);
                }
                bitmap.Save(path, ImageFormat.Jpeg);

                if (File.Exists(path))
                {
                    //TestDriver.Instance.TestContext.AddResultFile(path);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Not able to take a screenshot", ex);
            }
        }

        public static void TakeScreenshot(string screenShotName)
        {
            try
            {
                int maxScreenshotNameLength = 59;
                string screenshotFolderName = "Screenshots";

                if (!Directory.Exists(screenshotFolderName))
                {
                    Directory.CreateDirectory(screenshotFolderName);
                }

                string screenShotIncName = string.Format("{0}_{1}", ++_screenShot, screenShotName);

                if (screenShotIncName.Length > maxScreenshotNameLength)
                {
                    screenShotIncName = screenShotIncName.Substring(0, maxScreenshotNameLength);
                }

                string fileName = string.Format("{0}.{1}", screenShotIncName, ScreenshotImageFormat.Jpeg);
                string filePath = Path.Combine(screenshotFolderName, fileName);

                if (WebDriver != null)
                {
                    WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                }

                using var bitmap = new Bitmap(1920, 1080);
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(0, 0, 0, 0,
                    bitmap.Size, CopyPixelOperation.SourceCopy);
                }
                bitmap.Save(filePath, ImageFormat.Jpeg);

                if (File.Exists(filePath))
                {
                    //TestDriver.Instance.TestContext.AddResultFile(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Not able to take a screenshot", ex);
            }
        }

        #endregion
    }
}