using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RealEstateAPI.Models
{
    public class TokenData
    {
        [Key] 
        public int Tid { get; set; }

        [ForeignKey("User")]
        public string UserEmail { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordKey { get; set; }
        public string Role { get; set; }

        public User? User { get; set; }
    }
}
