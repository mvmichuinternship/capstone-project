using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.Models
{
    public class OTP
    {
        [Key]
        public string Phone { get; set; }

        public string Otp {  get; set; }
        public DateTime Expiration { get; set; }
    }
}
