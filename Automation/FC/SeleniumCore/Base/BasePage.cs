using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumCore.Helpers.Extensions;
using SeleniumCore.WebDriver;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace SeleniumCore.Base
{
    public abstract class BasePage
    {
        public virtual By GetElementBy(Type pageType, string propertyName)
        {
            By getter = null;

            How how = (How)int.Parse((from attribData in pageType.GetProperty(propertyName).GetCustomAttributesData()
                                      where attribData != null
                                      from a in attribData.NamedArguments
                                      where a.MemberName.Equals("How")
                                      select a.TypedValue).FirstOrDefault().Value.ToString());


            string location = (from attribData in pageType.GetProperty(propertyName).GetCustomAttributesData()
                               where attribData != null
                               from a in attribData.NamedArguments
                               where a.MemberName.Equals("Using")
                               select a.TypedValue).FirstOrDefault().ToString();

            location = location.Remove(0, 1);
            location = location.Remove(location.Length - 1, 1);

            switch (how)
            {
                case How.Id:
                    getter = By.Id(location);
                    break;
                case How.XPath:
                    getter = By.XPath(location);
                    break;
                case How.CssSelector:
                    getter = By.CssSelector(location);
                    break;
                case How.ClassName:
                    getter = By.ClassName(location);
                    break;
                case How.Name:
                    getter = By.Name(location);
                    break;
                case How.TagName:
                    getter = By.TagName(location);
                    break;
                case How.PartialLinkText:
                    getter = By.PartialLinkText(location);
                    break;
                case How.LinkText:
                    getter = By.LinkText(location);
                    break;
            }

            return getter;
        }

        public T ImplicitlyWaitForPageToBeReady<T>(int seconds = 1) where T : BasePage
        {
            Thread.Sleep(seconds * 1000);
            return UIDriver.WebDriver.GetPage<T>();
        }

        public T WaitForPageToBeReady<T>(By locator, string description, uint timeout = 60, bool isProcessing = false, bool shouldPass = false) where T : BasePage
        {
            if (HasBecameVisible(locator, description, isProcessing, timeout))
            {
                try
                {
                    if (!isProcessing)
                    {
                        return UIDriver.WebDriver.GetPage<T>();
                    }
                    else if (HasBecameInvisible(locator, description, timeout))
                    {
                    }
                }
                catch (Exception e)
                {
                }
            }
            else if (!shouldPass)
            {
                throw new ElementNotVisibleException($"[{description}] never showed up");
            }
            else
            {
            }

            return UIDriver.WebDriver.GetPage<T>();
        }

        public T WaitForProcessing<T>(By locator, string description, uint timeout = 60) where T : BasePage
        {
            return WaitForPageToBeReady<T>(locator, description, timeout, isProcessing: true, shouldPass: true);
        }

        private bool HasBecameVisible(By locator, string description, bool isProcessing, uint timeout)
        {
            bool isVisible = false;
            Stopwatch time = Stopwatch.StartNew();

            while (time.Elapsed < TimeSpan.FromSeconds(timeout))
            {
                IWebElement element = UIDriver.WebDriver.SafeFindElement(locator, 0);

                if (element.IsControlDisplayed(0))
                {
                    isVisible = true;
                    break;
                }
                else
                {
                    if (isProcessing && time.Elapsed > TimeSpan.FromSeconds(3))
                    {
                        isVisible = false;
                        break;
                    }
                }
            }

            return isVisible;
        }

        private bool HasBecameInvisible(By locator, string description, uint timeout)
        {
            bool isInvisible = false;
            Stopwatch time = Stopwatch.StartNew();

            while (time.Elapsed < TimeSpan.FromSeconds(timeout))
            {
                if (UIDriver.WebDriver.IsControlDisplayed(locator, 0))
                {
                    Thread.Sleep(250);
                    continue;
                }

                isInvisible = true;
                break;
            }

            if (!isInvisible)
            {
            }

            return isInvisible;
        }
    }
}