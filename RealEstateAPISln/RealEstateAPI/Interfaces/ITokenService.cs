using RealEstateAPI.Models;

namespace RealEstateAPI.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(TokenData tokenData);
    }
}
