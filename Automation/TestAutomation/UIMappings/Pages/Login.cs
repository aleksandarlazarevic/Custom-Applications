using System;
using System.Threading;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumCore.Base;
using SeleniumCore.Helpers.Extensions;

namespace UIMappings.Pages
{
    public class Login : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//*[@automationid='Username']")]
        public IWebElement UserName { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@automationid='Password']")]
        public IWebElement UserPassword { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@automationid='SignIn']")]
        public IWebElement SignIn { get; set; }

        public Login EnterCredentials(string username, string password)
        {
            try
            {
                this.UserName.SendKeysWrapper(username, "Username");
                this.UserPassword.SendKeysWrapper(password, "Password");
                return this;
            }
            catch
            {
                this.UserName.SendKeysWithClear(username, "Username");
                this.UserPassword.SendKeysWithClear(password, "Password");
                return this;
            }
        }

        public Login ClickOnSignIn()
        {
            Thread.Sleep(3000);
            SignIn.ClickWrapper("SignIn");
            return this;
        }
    }
}
