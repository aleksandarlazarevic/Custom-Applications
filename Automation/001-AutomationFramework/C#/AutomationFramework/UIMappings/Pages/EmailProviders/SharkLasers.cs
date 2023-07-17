using CommonCore.Tests;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumEngine.Base;
using SeleniumEngine.DriverInitialization;
using SeleniumEngine.Extensions;
using SeleniumEngine.Helpers;
using System.Collections.ObjectModel;
using System.Data;

namespace UIMappings.Pages.EmailProviders
{
    public class SharkLasers : BasePage
    {
        #region Fields and Properties
        public IWebElement ScrambleAddress => WebDriverFactory.WebDriver.FindElement(By.XPath("//*[@id='use-alias']"));
        public IWebElement Edit => WebDriverFactory.WebDriver.FindElement(By.XPath("//*[@id='inbox-id']"));
        public IWebElement EmailAddress => WebDriverFactory.WebDriver.FindElement(By.XPath("//*[@id='inbox-id']/input"));
        public IWebElement Set => WebDriverFactory.WebDriver.FindElement(By.XPath("//*[@id='inbox-id']/button[1]"));
        public IWebElement GeneratedEmailAddress => WebDriverFactory.WebDriver.FindElement(By.XPath("//*[@id='email-widget']"));
        public IWebElement EmailContent => WebDriverFactory.WebDriver.FindElement(By.XPath("//div[@class='email_body']"));
        public IWebElement Inbox => WebDriverFactory.WebDriver.FindElement(By.Id("email_table"));
        public IWebElement NotificationEmailContent => WebDriverFactory.WebDriver.FindElement(By.XPath("//*[@id='display_email']/div/div[2]/div/p"));
        public IWebElement DeleteButton => WebDriverFactory.WebDriver.FindElement(By.XPath("//input[@class='button'][@value='Delete']"));
        #endregion

        #region Methods
        public SharkLasers ClickOnScrambleAddress(bool check = false)
        {
            this.ScrambleAddress.TickCheckboxEx(check, "ScrambleAddress", true);
            return this;
        }

        public SharkLasers ClickOnEdit()
        {
            this.Edit.ClickEx("Edit");
            return this;
        }

        public SharkLasers EnterEmailAddress(string emailAddress)
        {
            this.EmailAddress.SendKeysEx(emailAddress.Split('@').First(), "EmailAddress");
            return this;
        }

        public SharkLasers ClickOnSet()
        {
            this.Set.ClickEx("Set");
            return this;
        }

        public SharkLasers ClickCommercialDownButton()
        {
            WebDriverFactory.WebDriver.FindElementEx(By.XPath("//*[@style='filter:url(#dropShadowTop)']"), 15)?.ClickEx("Commercial");
            return this;
        }

        public SharkLasers SaveGeneratedEmailAddress(string email)
        {
            TestInMemoryParameters.Instance.GeneratedEmailAddress = this.GeneratedEmailAddress.Text;
            return this;
        }

        public SharkLasers ClickOnDelete()
        {
            this.DeleteButton.ClickEx("Delete");
            return this;
        }

        public SharkLasers ClickOnEmailFromSpecificSender(string senderAddress)
        {
            DataRow email = GetLastReceivedEmailFromSpecificSender(senderAddress);

            if (email == null)
            {
                throw new NoSuchElementException(string.Format("No emails from the: {0} have been received", senderAddress));
            }

            WebDriverFactory.TakeScreenshot($"[SHARKLASERS] - Email - {senderAddress}");
            email.Field<IWebElement>("Link")?.ClickEx(senderAddress);
            return this;
        }

        public SharkLasers ClickOnEmailWithSpecificSubject(string subject)
        {
            DataRow email = GetLastReceivedEmailWithSpecificSubject(subject);

            if (email == null)
            {
                throw new NoSuchElementException(string.Format("There are no emails with subject: {0}", subject));
            }

            WebDriverFactory.TakeScreenshot($"[SHARKLASERS] - Email - {subject}");
            email.Field<IWebElement>("Link")?.ClickEx(subject);
            return this;
        }

        public SharkLasers GetVerificationCode(string text, out string verificationCodeText)
        {
            IWebElement textElement = this.EmailContent.FindElementEx(By.XPath(string.Format("//span[contains(text(), '{0}')]", text)));

            if (textElement == null)
            {
                throw new NotFoundException(string.Format("Failed to locate '{0}' in the content", text));
            }

            verificationCodeText = textElement.Text;

            return this;
        }

        public SharkLasers SelectAllEmailsFromInbox()
        {
            GetAllEmails().AsEnumerable()
                          .All(x =>
                          {
                              x.Field<IWebElement>("Checkbox").ClickEx("Email");
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
                JavaScriptHelper.ScrollIntoView(Inbox);
                WebDriverFactory.WebDriver.WaitUntil(_ => IsEmailFromSpecificSenderReceived(sender), timeoutInSeconds: timeoutInSeconds, pollingIntervalInSeconds: pollingIntervalInSeconds);
            }
            catch (Exception ex)
            {
                throw new TimeoutException(string.Format("Email not received after {0} seconds", timeoutInSeconds), ex);
            }

            return this;
        }

        public SharkLasers VerifyEmailBodyContent(string text)
        {
            string email = this.NotificationEmailContent.GetValueEx();
            Assert.IsTrue(email.Contains(text), string.Format("Email body does not contain expected text! \nExpected: {0} \nActual: {1}", text, email));
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

                ReadOnlyCollection<IWebElement> rows = Inbox.FindElementsEx(By.XPath("./tbody/tr"), 60);

                foreach (IWebElement row in rows)
                {
                    if (row.GetValueEx().Contains("There are no emails"))
                    {
                        break;
                    }

                    IWebElement checkBox = row.FindElementEx(By.XPath(".//td[@class='td1']//input"), 2);
                    IWebElement senderAddress = row.FindElementEx(By.XPath(".//td[@class='td2']"), 2);
                    IWebElement link = row.FindElementEx(By.XPath(".//td[@class='td3']"), 2);
                    string subject = row.FindElementEx(By.XPath(".//td[@class='td3']"), 2).GetValueEx();
                    string time = row.FindElementEx(By.XPath(".//td[@class='td4']"), 2).GetValueEx();

                    inboxDt.Rows.Add(checkBox, senderAddress, link, subject, time);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to read emails from the inbox", ex);
            }

            return inboxDt;
        }

        private DataRow GetLastReceivedEmailWithSpecificSubject(string subject)
        {
            return GetAllEmails().AsEnumerable()
                                 .FirstOrDefault(x => x.Field<string>("Subject") != null &&
                                                      x.Field<string>("Subject").ToUpper().Contains(subject.ToUpper()));
        }

        private DataRow GetLastReceivedEmailFromSpecificSender(string senderAddress)
        {
            return GetAllEmails().AsEnumerable()
                                 .FirstOrDefault(x => x.Field<IWebElement>("SenderAddress") != null &&
                                                      x.Field<IWebElement>("SenderAddress").Text.ToUpper().Contains(senderAddress.ToUpper()));
        }

        private bool IsEmailFromSpecificSenderReceived(string senderAddress)
        {
            bool hasEmail = false;

            try
            {
                Thread.Sleep(2000);
                hasEmail = GetLastReceivedEmailFromSpecificSender(senderAddress) != null;

                if (!hasEmail)
                {
                    this.Edit.ClickEx("Edit");
                    Thread.Sleep(2000);
                    this.Set.ClickEx("Set");
                }
            }
            catch
            { }
            finally
            {
                WebDriverFactory.TakeScreenshot("[SHARKLASERS] - Inbox");
            }

            return hasEmail;
        }

        private bool IsEmailWithSpecificSubjectReceived(string subject)
        {
            bool hasEmail = false;

            try
            {
                Thread.Sleep(2000);
                hasEmail = GetLastReceivedEmailWithSpecificSubject(subject) != null;

                if (!hasEmail)
                {
                    this.Edit.ClickEx("Edit");
                    Thread.Sleep(2000);
                    this.Set.ClickEx("Set");
                }
            }
            catch
            { }
            finally
            {
                WebDriverFactory.TakeScreenshot("[SHARKLASERS] - Inbox");
            }

            return hasEmail;
        }
        #endregion
    }
}