using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace RealEstateAPI.Models
{
    [ExcludeFromCodeCoverage]
    public class TokenData
    {
        [Key] 
        public int Tid { get; set; }

        [ForeignKey("User")]
        public string UserEmail { get; set; }
        
        public string Phone { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordKey { get; set; }
        public string Role { get; set; }
        public string? Plan { get; set; }
        public User? User { get; set; }
    }
}
