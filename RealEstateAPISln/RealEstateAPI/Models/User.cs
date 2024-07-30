using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.Models
{
    public class User
    {
        public string Name { get; set; }
        [Key]
        public string UserEmail { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string? Plan { get; set; } = "Basic";
    }
}
