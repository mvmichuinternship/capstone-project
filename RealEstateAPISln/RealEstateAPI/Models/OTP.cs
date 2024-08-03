using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RealEstateAPI.Models
{
    [ExcludeFromCodeCoverage]
    public class OTP
    {
        [Key]
        public string Phone { get; set; }

        public string Otp {  get; set; }
        public DateTime Expiration { get; set; }
    }
}
