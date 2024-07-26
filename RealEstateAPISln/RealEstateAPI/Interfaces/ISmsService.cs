namespace RealEstateAPI.Interfaces
{
    public interface ISmsService
    {
        //Task<string> SendOtpAsync(string to);
        public void SendSms(string to, string msg);
    }
}
