using NUnit.Framework;
using OpenQA.Selenium;
using FindsByAttribute = SeleniumExtras.PageObjects.FindsByAttribute;
using How = SeleniumExtras.PageObjects.How;
using SeleniumCore.Base;
using SeleniumCore.Helpers.Extensions;
using SeleniumCore.WebDriver;
using System;
using System.Linq;
using Utilities;

namespace UIMappings.Pages.EmailProviders
{
    public class Mailinator : BasePage
    {
        #region Fields and Properties
        [FindsBy(How = How.XPath, Using = "//input[@id='search'] | //input[@id='inbox_field']")]
        public IWebElement Email { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[normalize-space(text())='GO']")]
        public IWebElement Go { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='table-striped jambo_table']")]
        public IWebElement EmailListContainer { get; set; }

        [FindsBy(How = How.TagName, Using = "body")]
        public IWebElement EmailContent { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='inbox_field']")]
        public IWebElement EmailName { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Delete Button']")]
        public IWebElement DeleteButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='secureLinkSalutationId']//..//..//p[1]")]
        public IWebElement NotificationEmailContent { get; set; }
        #endregion

        #region Methods
        public Mailinator EnterEmail(string emailAddress)
        {
            this.Email = UIDriver.WebDriver.SafeFindElement(By.XPath("//input[@id='search'] | //input[@id='inbox_field']"));
            this.Email.SendKeysWrapper(emailAddress, "EmailAddress");
            return this;
        }

        public Mailinator ClickOnGo()
        {
            this.Email = UIDriver.WebDriver.SafeFindElement(By.XPath("//input[@id='search'] | //input[@id='inbox_field']"));
            this.Go.ClickWrapper("Go");
            return this;
        }

        public Mailinator SaveGeneratedEmailAddress(string email)
        {
            TestInMemoryParametersShared.Instance.EmailAddress = email;
            return this;
        }

        public string GetGeneratedEmail()
        {
            this.Email = UIDriver.WebDriver.SafeFindElement(By.XPath("//input[@id='search'] | //input[@id='inbox_field']"));
            this.EmailName = UIDriver.WebDriver.SafeFindElement(By.XPath("//*[@id='inbox_field']"));
            return string.Format("{0}@{1}", this.EmailName.SafeValue(), "mailinator.com");
        }

        public Mailinator ClickOnEmail(string subject)
        {
            this.EmailListContainer = UIDriver.WebDriver.SafeFindElement(By.XPath("//*[@class='table-striped jambo_table']"));
            this.EmailListContainer.FindElements(By.XPath("./tbody/tr"))
                              .FirstOrDefault(x => x.SafeFindElement(By.XPath("./td[3]")).SafeValue().Contains(subject))
                              .ClickWrapper(subject);
            return this;
        }

        private bool IsEmailReceived(string subject)
        {
            IWebElement emailElement = this.EmailListContainer.FindElements(By.XPath("./tbody/tr"))
                                                         .FirstOrDefault(x => x.SafeFindElement(By.XPath("./td[3]")).SafeValue().Contains(subject));

            return emailElement != null;
        }

        public Mailinator GetVerificationCode(string keyword, out string verificationCodeText)
        {
            UIDriver.WebDriver.SwitchTo().Frame("html_msg_body");

            IWebElement textElement = UIDriver.WebDriver.SafeFindElement(By.XPath(string.Format("//*[contains(text(), '{0}')]", keyword)));

            if (textElement == null)
            {
                throw new NotFoundException($"Cannot find keyword '{keyword}' in the content");
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
                UIDriver.WebDriver.WaitUntil(_ => IsEmailReceived(subject), timeoutInSeconds: timeoutInSeconds, pollingIntervalInSeconds: pollingIntervalInSeconds);
            }
            catch (Exception ex)
            {
                throw new TimeoutException($"Email not received after [{timeoutInSeconds}] seconds", ex);
            }

            return this;
        }

        public Mailinator VerifyEmailsWithSpecificSubject(string subjectEmail, string expectedEmailNumber)
        {
            string emailXpath = String.Format("//div[@id='inboxpane']//a[contains(text(), '{0}')]", subjectEmail);
            var emailElements = UIDriver.WebDriver.FindElements(By.XPath(emailXpath), 10);

            Assert.AreEqual(expectedEmailNumber, emailElements.Count.ToString(), "Correct number of emails not found, expected: [{0}], found: [{1}]", expectedEmailNumber, emailElements.Count);

            UIDriver.TakeScreenshot("Mailinator MAIL - Verify number of emails");

            return this;
        }

        public Mailinator SelectAllEmailsFromInbox()
        {
            var emailItems = UIDriver.WebDriver.FindElements(By.XPath("//*[@type='checkbox']"));

            foreach (var item in emailItems)
                item.ClickWrapper("emailItem");

            return this;
        }

        public Mailinator ClickOnDelete()
        {
            this.DeleteButton = UIDriver.WebDriver.SafeFindElement(By.XPath("//button[@aria-label='Delete Button']"));
            this.DeleteButton.ClickWrapper("Delete");
            return this;
        }

        public Mailinator VerifyEmailBodyContent(string text)
        {
            this.NotificationEmailContent = UIDriver.WebDriver.SafeFindElement(By.XPath("//*[@id='secureLinkSalutationId']//..//..//p[1]"));
            UIDriver.WebDriver.SwitchTo().Frame("html_msg_body");
            string email = this.NotificationEmailContent.SafeValue();
            Assert.IsTrue(email.Contains(text), $"Email body does not contain expected text! \nExpected: [{text}] \nActual: [{email}]");
            return this;
        }
        #endregion
    }
}
