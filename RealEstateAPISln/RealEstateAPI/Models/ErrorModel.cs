namespace RealEstateAPI.Models
{
    public class ErrorModel
    {
        int ErrorCode { get; set; }
        public string Message {  get; set; }

        public ErrorModel(int errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public ErrorModel() { }
    }
}
