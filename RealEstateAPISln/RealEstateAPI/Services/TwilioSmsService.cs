using RealEstateAPI.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
namespace RealEstateAPI.Services
{
    public class TwilioSmsService : ISmsService
    {
        private readonly string _accountSid = "ACf5a9a2d946cef5088d494e7136ab1118";
        private readonly string _authToken = "945fb6c3f661f20d717387c3fe8b10b4";
        private readonly string _fromNumber = "+(888) 853-3993";

        public TwilioSmsService()
        {
            TwilioClient.Init(_accountSid, _authToken);
        }

        public void SendSms(string to, string message)
        {
            var messageOptions = new CreateMessageOptions(new PhoneNumber("+91" + to))
            {
                From = new PhoneNumber(_fromNumber),
                Body = message
            };

            var msg = MessageResource.Create(new PhoneNumber("+91" + to),
            from: new PhoneNumber("+18888533993"),
            body: message);
            
        }
    }
}

