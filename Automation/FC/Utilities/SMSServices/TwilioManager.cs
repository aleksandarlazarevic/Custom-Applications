using System;
using System.Linq;
using System.Threading;
using Twilio;
using Twilio.Base;
using Twilio.Rest.Api.V2010.Account;

namespace Utilities.SMSServices
{
    public class TwilioManager
    {
        //string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
        //string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
        string accountSid = "ACb756253c6cd8f9869c1414b2dd47ad96";
        string authToken = "e0a2b03692098c2f39e7184567f045e9";
        string twilioPhoneNumber = "+18329812927";

        public TwilioManager()
        {
            TwilioClient.Init(accountSid, authToken);
        }

        public void SendSms(string messageBody, string toNumber)
        {
            MessageResource message = MessageResource.Create(
                body: messageBody,
                from: new Twilio.Types.PhoneNumber(twilioPhoneNumber),
                to: new Twilio.Types.PhoneNumber(toNumber)
            );
        }

        public ResourceSet<MessageResource> GetAllReceivedSmsMessages()
        {
            ResourceSet<MessageResource> messages = MessageResource.Read(
                to: new Twilio.Types.PhoneNumber(twilioPhoneNumber),
                limit: 20
            );

            return messages;
        }

        public ResourceSet<MessageResource> GetAllSentSmsMessages()
        {
            ResourceSet<MessageResource> messages = MessageResource.Read(
                from: new Twilio.Types.PhoneNumber(twilioPhoneNumber),
                limit: 20
            );

            return messages;
        }

        public ResourceSet<MessageResource> GetAllSmsMessagesReceivedOnASpecificDate(DateTime date)
        {
            // Date value example: new DateTime(2022, 11, 15, 0, 0, 0)
            ResourceSet<MessageResource> messages = MessageResource.Read(
                dateSent: date,
                to: new Twilio.Types.PhoneNumber(twilioPhoneNumber),
                limit: 20
            );

            return messages;
        }

        public ResourceSet<MessageResource> GetAllSmsMessagesReceivedAfter(DateTime date)
        {
            ResourceSet<MessageResource> messages = MessageResource.Read(
                //dateSentAfter: date,
                limit: 20
            );

            return messages;
        }

        public string GetSmsVerificationCode(DateTime date)
        {
            Thread.Sleep(20000);
            ResourceSet<MessageResource> messages = GetAllSmsMessagesReceivedAfter(date);
            string lastMessage = messages.Where(p => p.Body.Contains("Use verification code")).Last().Body;
            string verificationCode = lastMessage.Split(new string[] { "verification code " }, StringSplitOptions.None)[1].Substring(0, 5);

            return verificationCode;
        }
    }
}
