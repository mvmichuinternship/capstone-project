using System.Diagnostics.CodeAnalysis;

namespace RealEstateAPI.Models.DTOs.Register
{
    public class UserDTO : User
    {
        [ExcludeFromCodeCoverage]
        public string Password {  get; set; }
    }
}
