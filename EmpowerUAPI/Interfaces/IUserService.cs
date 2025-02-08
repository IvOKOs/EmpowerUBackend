using EmpowerUAPI.Dtos;
using EmpowerUAPI.Helpers;

namespace EmpowerUAPI.Interfaces
{
    public interface IUserService
    {
        Task<UserRegistrationResult> RegisterUser(RegisterUserDto regUser);
        Task<bool> ValidateUserCredentials(LoginUserDto loginUser);
    }
}
