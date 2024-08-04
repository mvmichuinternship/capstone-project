namespace RealEstateAPI.Models.DTOs.Email
{
    public class EmailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string HtmlContent { get; set; }
    }
}
