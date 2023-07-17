using CommonCore.Tests;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumEngine.Base;
using SeleniumEngine.DriverInitialization;
using SeleniumEngine.Extensions;

namespace UIMappings.Pages.EmailProviders
{
    public class Mailinator : BasePage
    {
        #region Fields and Properties
        public IWebElement Email => WebDriverFactory.WebDriver.FindElement(By.XPath("//input[@id='search'] | //input[@id='inbox_field']"));
        public IWebElement Go => WebDriverFactory.WebDriver.FindElement(By.XPath("//button[normalize-space(text())='GO']"));
        public IWebElement EmailListContainer => WebDriverFactory.WebDriver.FindElement(By.XPath("//*[@class='table-striped jambo_table']"));
        public IWebElement EmailContent => WebDriverFactory.WebDriver.FindElement(By.TagName("body"));
        public IWebElement EmailName => WebDriverFactory.WebDriver.FindElement(By.XPath("//*[@id='inbox_field']"));
        public IWebElement DeleteButton => WebDriverFactory.WebDriver.FindElement(By.XPath("//button[@aria-label='Delete Button']"));
        public IWebElement NotificationEmailContent => WebDriverFactory.WebDriver.FindElement(By.XPath("//*[@id='secureLinkSalutationId']//..//..//p[1]"));
        #endregion

        #region Methods
        public Mailinator EnterEmail(string emailAddress)
        {
            this.Email.SendKeysEx(emailAddress, "EmailAddress");
            return this;
        }

        public Mailinator ClickOnGo()
        {
            this.Go.ClickEx("Go");
            return this;
        }

        public Mailinator SaveGeneratedEmailAddress(string email)
        {
            TestInMemoryParameters.Instance.EmailAddress = email;
            return this;
        }

        public string GetGeneratedEmail()
        {
            return string.Format("{0}@{1}", this.EmailName.GetValueEx(), "mailinator.com");
        }

        public Mailinator ClickOnEmail(string subject)
        {
            this.EmailListContainer.FindElements(By.XPath("./tbody/tr"))
                              .FirstOrDefault(x => x.FindElementEx(By.XPath("./td[3]")).GetValueEx().Contains(subject))
                              .ClickEx(subject);
            return this;
        }

        private bool IsEmailReceived(string subject)
        {
            IWebElement emailElement = this.EmailListContainer.FindElements(By.XPath("./tbody/tr"))
                                                         .FirstOrDefault(x => x.FindElementEx(By.XPath("./td[3]")).GetValueEx().Contains(subject));

            return emailElement != null;
        }

        public Mailinator GetVerificationCode(string text, out string verificationCodeText)
        {
            WebDriverFactory.WebDriver.SwitchTo().Frame("html_msg_body");

            IWebElement textElement = WebDriverFactory.WebDriver.FindElementEx(By.XPath(string.Format("//*[contains(text(), '{0}')]", text)));

            if (textElement == null)
            {
                throw new NotFoundException(string.Format("Failed to locate '{0}' in the content", text));
            }

            verificationCodeText = textElement.Text;

            return this;
        }

        public Mailinator WaitForEmail(string subject)
        {
            uint timeoutInSeconds = 240;
            int pollingIntervalInSeconds = 15;

            try
            {
                WebDriverFactory.WebDriver.WaitUntil(_ => IsEmailReceived(subject), timeoutInSeconds: timeoutInSeconds, pollingIntervalInSeconds: pollingIntervalInSeconds);
            }
            catch (Exception ex)
            {
                throw new TimeoutException(string.Format("Email not received after {0} seconds", timeoutInSeconds), ex);
            }

            return this;
        }

        public Mailinator VerifyNumberOfEmailsWithSpecificSubject(string subjectEmail, string expectedNumberOfEmails)
        {
            string emailXpath = string.Format("//div[@id='inboxpane']//a[contains(text(), '{0}')]", subjectEmail);
            var emailElements = WebDriverFactory.WebDriver.FindElementsEx(By.XPath(emailXpath), 10);

            Assert.AreEqual(expectedNumberOfEmails, emailElements.Count.ToString(), string.Format("Expected number of {0} emails not not matching the actual number {1}"), expectedNumberOfEmails, emailElements.Count);

            WebDriverFactory.TakeScreenshot("Mailinator MAIL - Verify number of emails");

            return this;
        }

        public Mailinator SelectAllEmailsFromInbox()
        {
            var emailItems = WebDriverFactory.WebDriver.FindElements(By.XPath("//*[@type='checkbox']"));

            foreach (var item in emailItems)
                item.ClickEx("emailItem");

            return this;
        }

        public Mailinator ClickOnDelete()
        {
            this.DeleteButton.ClickEx("Delete");
            return this;
        }

        public Mailinator VerifyEmailBodyContent(string text)
        {
            WebDriverFactory.WebDriver.SwitchTo().Frame("html_msg_body");
            string email = this.NotificationEmailContent.GetValueEx();
            Assert.IsTrue(email.Contains(text), string.Format("Email body does not contain expected text! \nExpected: {0} \nActual: {1}", text, email));
            return this;
        }
        #endregion
    }
}