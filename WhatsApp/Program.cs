using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
namespace WhatsApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
            accountSid = "ACbca110ef58b6869330d1d61daab905e1";
            authToken = "80a3476be411770d6250c07abd4dc39f";
            TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                body: "Hello World https://www.motilaloswalmf.com/",
                from: new Twilio.Types.PhoneNumber("whatsapp:+14155238886"),
                to: new Twilio.Types.PhoneNumber("whatsapp:+91")
            );

            Console.WriteLine(message.Sid);
        }
    }
}
