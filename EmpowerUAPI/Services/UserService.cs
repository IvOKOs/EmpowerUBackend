using AutoMapper;
using EmpowerEFCore.Domain;
using EmpowerUAPI.Dtos;
using EmpowerUAPI.Helpers;
using EmpowerUAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmpowerUAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _hasher;

        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher hasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _hasher = hasher;
        }
        public async Task<UserRegistrationResult> RegisterUser(RegisterUserDto regUser)
        {
            bool userExists = await _userRepository.UserExistsByEmail(regUser.Email);
            if (userExists)
            {
                return UserRegistrationResult.Failure("This email already exists.");
            }

            var user = _mapper.Map<User>(regUser);

            string hashedPassword = _hasher.Hash(regUser.Password);
            if (string.IsNullOrWhiteSpace(hashedPassword))
            {
                return UserRegistrationResult.Failure("Something went wrong with the password.");
            }
            user.HashedPassword = hashedPassword;

            await _userRepository.AddAsync(user);
            return UserRegistrationResult.Success();
        }
        
        public async Task<bool> ValidateUserCredentials(LoginUserDto loginUser)
        {
            var user = await _userRepository.GetUserByEmail(loginUser.Email);
            if (user is null)
            {
                return false;
            }
            return _hasher.Verify(loginUser.Password, user.HashedPassword);
        }
    }
}
