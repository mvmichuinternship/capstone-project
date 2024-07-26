using RealEstateAPI.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
namespace RealEstateAPI.Services
{
    public class TwilioSmsService : ISmsService
    {
        private readonly string _accountSid = "ACe57bd2d138c96febdcc6607c4518de30";
        private readonly string _authToken = "8edaafcb94f8447d8df795a2d2cb193d";
        private readonly string _fromNumber = "+10987654321";

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
    from: new PhoneNumber("+15005550006"),
    body: message);
            // Optionally handle the response or log
        }
    }
}


//using FirebaseAdmin;
//using FirebaseAdmin.Auth;
//using Google.Apis.Auth.OAuth2;
//using Microsoft.AspNetCore.Builder.Extensions;
//using RealEstateAPI.Interfaces;

//namespace RealEstateAPI.Services
//{

//    public class TwilioSmsService: ISmsService
//    {
//        public TwilioSmsService()
//        {
//            //FirebaseApp.Create(new AppOptions()
//            //{
//            //    Credential = GoogleCredential.FromFile("C:\\Users\\VC\\Desktop\\Presidio\\67acres\\RealEstateAPISln\\RealEstateAPI\\serviceAccountKey.json"),
//            //});
//        }

//        public async Task<string> SendOtpAsync(string phoneNumber)
//        {
//            var message = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(phoneNumber);
//            return message; // Use the returned token as OTP
//        }


//    }
//}