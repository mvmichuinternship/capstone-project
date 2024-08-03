using RealEstateAPI.Interfaces;
using System.Diagnostics.CodeAnalysis;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
namespace RealEstateAPI.Services
{
    [ExcludeFromCodeCoverage]
    public class TwilioSmsService : ISmsService
    {
        private readonly string _accountSid = "";
        private readonly string _authToken = "";
        private readonly string _fromNumber = "";

        public TwilioSmsService(string accountSid,string authToken)
        {
            _accountSid = accountSid;
            _authToken = authToken;
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

