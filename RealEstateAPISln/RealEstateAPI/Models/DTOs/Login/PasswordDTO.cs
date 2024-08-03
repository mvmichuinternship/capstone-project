using System.Diagnostics.CodeAnalysis;

namespace RealEstateAPI.Models.DTOs.Login
{
    [ExcludeFromCodeCoverage]
    public class PasswordDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
