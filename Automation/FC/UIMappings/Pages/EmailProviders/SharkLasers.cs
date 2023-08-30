using NUnit.Framework;
using OpenQA.Selenium;
//using OpenQA.Selenium.Support.PageObjects;
using SeleniumCore.Base;
using SeleniumCore.Helpers.Extensions;
using SeleniumCore.Helpers.Utilities;
using SeleniumCore.WebDriver;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading;
using Utilities;
using FindsByAttribute = SeleniumExtras.PageObjects.FindsByAttribute;
using How = SeleniumExtras.PageObjects.How;

namespace UIMappings.Pages.EmailProviders
{
    public class SharkLasers : BasePage
    {
        #region Fields and Properties
        [FindsBy(How = How.XPath, Using = "//*[@id='use-alias']")]
        public IWebElement ScrambleAddress { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='inbox-id']")]
        public IWebElement Edit { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='inbox-id']/input")]
        public IWebElement EmailAddress { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='inbox-id']/button[1]")]
        public IWebElement Set { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='email-widget']")]
        public IWebElement GeneratedEmailAddress { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='email_body']")]
        public IWebElement EmailContent { get; set; }

        [FindsBy(How = How.Id, Using = "email_table")]
        public IWebElement Inbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='display_email']/div/div[2]/div/p")]
        public IWebElement NotificationEmailContent { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@class='button'][@value='Delete']")]
        public IWebElement DeleteButton { get; set; }
        #endregion
       
        #region Methods
        public SharkLasers ClickOnScrambleAddress(bool check = false)
        {
            this.ScrambleAddress = UIDriver.WebDriver.SafeFindElement(By.XPath("//input[@id='use-alias']"));
            this.ScrambleAddress.CheckboxWrapper(check, "ScrambleAddress", true);
            return this;
        }

        public SharkLasers ClickOnEdit()
        {
            this.Edit = UIDriver.WebDriver.SafeFindElement(By.XPath("//*[@id='inbox-id']"));
            this.Edit.ClickWrapper("Edit");
            return this;
        }

        public SharkLasers EnterEmailAddress(string emailAddress)
        {
            this.EmailAddress = UIDriver.WebDriver.SafeFindElement(By.XPath("//*[@id='inbox-id']/input"));
            this.EmailAddress.SendKeysWrapper(emailAddress.Split('@').First(), "EmailAddress");
            return this;
        }

        public SharkLasers ClickOnSet()
        {
            this.Set = UIDriver.WebDriver.SafeFindElement(By.XPath("//*[@id='inbox-id']/button[1]"));
            this.Set.ClickWrapper("Set");
            return this;
        }

        public SharkLasers ClickCommercialDownButton()
        {
            UIDriver.WebDriver.SafeFindElement(By.XPath("//*[@style='filter:url(#dropShadowTop)']"), 15)?.ClickWrapper("Commercial");
            return this;
        }

        public SharkLasers SaveGeneratedEmailAddress(string email)
        {
            this.GeneratedEmailAddress = UIDriver.WebDriver.SafeFindElement(By.XPath("//*[@id='email-widget']"));
            TestInMemoryParametersShared.Instance.GeneratedEmailAddress = this.GeneratedEmailAddress.Text;
            return this;
        }

        public SharkLasers ClickOnDelete()
        {
            this.DeleteButton = UIDriver.WebDriver.SafeFindElement(By.XPath("//input[@class='button'][@value='Delete']"));
            this.DeleteButton.ClickWrapper("Delete");
            return this;
        }

        public SharkLasers ClickOnEmailBySenderAddress(string senderAddress)
        {
            DataRow email = GetLastReceivedEmailBySenderAddress(senderAddress);

            if (email == null)
            {
                throw new NoSuchElementException($"Email from the sender {senderAddress} has not been found");
            }

            UIDriver.TakeScreenshot($"[SHARKLASERS] - Email - {senderAddress}");
            email.Field<IWebElement>("Link")?.ClickWrapper(senderAddress);
            return this;
        }

        public SharkLasers ClickOnEmailWithCertainSubject(string subject)
        {
            DataRow email = GetLastReceivedEmailBySubject(subject);

            if (email == null)
            {
                throw new NoSuchElementException($"Email with the subject {subject} has not been found");
            }

            UIDriver.TakeScreenshot($"[SHARKLASERS] - Email - {subject}");
            email.Field<IWebElement>("Link")?.ClickWrapper(subject);
            return this;
        }

        public SharkLasers GetVerificationCode(string text, out string verificationCodeText)
        {
            this.EmailContent = UIDriver.WebDriver.SafeFindElement(By.XPath("//div[@class='email_body']"));
            IWebElement textElement = this.EmailContent.SafeFindElement(By.XPath(string.Format("//span[contains(text(), '{0}')]", text)));
            //UIDriver.TakeScreenshot($"[SHARKLASERS] - EmailContent - {text}");

            if (textElement == null)
            {
                throw new NotFoundException($"Cannot find '{text}' in the content");
            }

            verificationCodeText = textElement.Text;

            return this;
        }

        public SharkLasers SelectAllEmailsFromInbox()
        {
            GetAllEmails().AsEnumerable()
                          .All(x =>
                          {
                              x.Field<IWebElement>("Checkbox").ClickWrapper("Email");
                              return true;
                          });

            return this;
        }

        public SharkLasers WaitForEmailFromCertainSender(string sender)
        {
            uint timeoutInSeconds = 300;
            int pollingIntervalInSeconds = 15;

            try
            {
                JavaScriptHelper.MoveScrollIntoView(Inbox);
                UIDriver.WebDriver.WaitUntil(_ => IsEmailReceivedFromCertainSender(sender), timeoutInSeconds: timeoutInSeconds, pollingIntervalInSeconds: pollingIntervalInSeconds);
            }
            catch (Exception ex)
            {
                throw new TimeoutException($"Email not received after [{TimeSpan.FromSeconds(timeoutInSeconds)}]", ex);
            }

            return this;
        }

        public SharkLasers VerifyEmailBodyContent(string text)
        {
            this.NotificationEmailContent = UIDriver.WebDriver.SafeFindElement(By.XPath("//*[@id='display_email']/div/div[2]/div/p"));
            string email = this.NotificationEmailContent.SafeValue();
            Assert.IsTrue(email.Contains(text), $"Email body does not contain expected text! \nExpected: [{text}] \nActual: [{email}]");
            return this;
        }

        private DataTable GetAllEmails()
        {
            DataTable inboxDt = new DataTable();

            try
            {
                inboxDt.Columns.Add("Checkbox", typeof(IWebElement));
                inboxDt.Columns.Add("SenderAddress", typeof(IWebElement));
                inboxDt.Columns.Add("Link", typeof(IWebElement));
                inboxDt.Columns.Add("Subject", typeof(string));
                inboxDt.Columns.Add("Time", typeof(string));

                ReadOnlyCollection<IWebElement> rows = Inbox.FindElements(By.XPath("./tbody/tr"), 60);

                foreach (IWebElement row in rows)
                {
                    if (row.SafeValue().Contains("There are no emails"))
                    {
                        break;
                    }

                    IWebElement checkBox = row.SafeFindElement(By.XPath(".//td[@class='td1']//input"), 2);
                    IWebElement senderAddress = row.SafeFindElement(By.XPath(".//td[@class='td2']"), 2);
                    IWebElement link = row.SafeFindElement(By.XPath(".//td[@class='td3']"), 2);
                    string subject = row.SafeFindElement(By.XPath(".//td[@class='td3']"), 2).SafeValue();
                    string time = row.SafeFindElement(By.XPath(".//td[@class='td4']"), 2).SafeValue();

                    inboxDt.Rows.Add(checkBox, senderAddress, link, subject, time);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Not able to read emails from sharklasers' inbox", ex);
            }

            return inboxDt;
        }

        private DataRow GetLastReceivedEmailBySubject(string subject)
        {
            return GetAllEmails().AsEnumerable()
                                 .FirstOrDefault(x => x.Field<string>("Subject") != null &&
                                                      x.Field<string>("Subject").ToUpper().Contains(subject.ToUpper()));
        }

        private DataRow GetLastReceivedEmailBySenderAddress(string senderAddress)
        {
            return GetAllEmails().AsEnumerable()
                                 .FirstOrDefault(x => x.Field<IWebElement>("SenderAddress") != null &&
                                                      x.Field<IWebElement>("SenderAddress").Text.ToUpper().Contains(senderAddress.ToUpper()));
        }        

        private bool IsEmailReceivedFromCertainSender(string senderAddress)
        {
            bool hasEmail = false;

            try
            {
                Thread.Sleep(2000);
                hasEmail = GetLastReceivedEmailBySenderAddress(senderAddress) != null;

                if (!hasEmail)
                {
                    this.Edit = UIDriver.WebDriver.SafeFindElement(By.XPath("//*[@id='inbox-id']"));
                    this.Edit.ClickWrapper("Edit");
                    Thread.Sleep(2000);
                    this.Set.ClickWrapper("Set");
                }
            }
            catch
            { }
            finally
            {
                UIDriver.TakeScreenshot("[SHARKLASERS] - Inbox");
            }

            return hasEmail;
        }

        private bool IsEmailReceivedWithCertainSubject(string subject)
        {
            bool hasEmail = false;

            try
            {
                Thread.Sleep(2000);
                hasEmail = GetLastReceivedEmailBySubject(subject) != null;

                if (!hasEmail)
                {
                    this.Edit = UIDriver.WebDriver.SafeFindElement(By.XPath("//*[@id='inbox-id']"));
                    this.Edit.ClickWrapper("Edit");
                    Thread.Sleep(2000);
                    this.Set.ClickWrapper("Set");
                }
            }
            catch
            { }
            finally
            {
                UIDriver.TakeScreenshot("[SHARKLASERS] - Inbox");
            }

            return hasEmail;
        }
        #endregion
    }
}
