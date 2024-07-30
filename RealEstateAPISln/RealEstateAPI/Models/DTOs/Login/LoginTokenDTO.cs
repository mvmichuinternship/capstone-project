using System.Diagnostics.CodeAnalysis;

namespace RealEstateAPI.Models.DTOs.Login
{
    public class LoginTokenDTO
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string? Plan { get; set; }
    }
}
