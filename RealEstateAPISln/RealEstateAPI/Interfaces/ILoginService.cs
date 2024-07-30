using RealEstateAPI.Models;
using RealEstateAPI.Models.DTOs.Login;
using RealEstateAPI.Models.DTOs.Register;

namespace RealEstateAPI.Interfaces
{
    public interface ILoginService
    {
        Task<User> Register(UserDTO userDTO);
        Task<string> GenerateOTP(string phoneNumber);
        Task<LoginTokenDTO> VerifyOTP(string phoneNumber, string otp);
        Task<LoginTokenDTO> LoginPassword(PasswordDTO passwordDTO);
        Task<LoginTokenDTO> SwitchRole(LoginTokenDTO loginTokenDTO); 
        Task<User> UpgradePlan(string email, bool upradePlan);
    }
}
