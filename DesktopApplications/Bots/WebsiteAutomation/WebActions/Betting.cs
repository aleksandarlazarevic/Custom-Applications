using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace WebActions
{
    public class Betting
    {
        private static IWebDriver driver = new ChromeDriver(@"D:\001-AleksandarLazarevic\001-Documents\004-GitHub\Custom-Applications\DesktopApplications\Bots\WebsiteAutomation\WebDrivers\Chrome\90.0.4430.24");

   

        public static void PlaceBets()
        {
            driver.Navigate().GoToUrl("https://www.mozzartbet.com/sr#/");
            driver.Manage().Window.Maximize();

            ClosePopups();
            Login();
            SortMostRecentFootballMatches();
            FindTheMatchWithAcceptableOdds();
            PlaceABet();

            driver.Close();
        }

        private static void ClosePopups()
        {
            throw new NotImplementedException();
        }

        private static void PlaceABet()
        {
            throw new NotImplementedException();
        }

        private static void FindTheMatchWithAcceptableOdds()
        {
            throw new NotImplementedException();
        }

        private static void SortMostRecentFootballMatches()
        {
            throw new NotImplementedException();
        }

        private static void Login()
        {
            IWebElement usernameInput = driver.FindElement(By.CssSelector("input[placeholder='Korisničko ime']"));
            usernameInput.SendKeys("ffff");
            Thread.Sleep(2000);
            
            IWebElement passwordInput = driver.FindElement(By.CssSelector("input[placeholder='Lozinka']"));
            passwordInput.SendKeys("zzzz");
            Thread.Sleep(2000);

            IWebElement loginButton = driver.FindElement(By.XPath("//div[@id='pageWrapper']/div/header/section/article[3]/section/article/div/form/button/span"));
            loginButton.Click();
            Thread.Sleep(3000);

        }
    }
}
