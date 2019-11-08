using IreckonuFileHandler.Core.Models;
using IreckonuFileHandler.Core.Repositories;
using IreckonuFileHandler.Core.Resource;
using IreckonuFileHandler.Core.Security.Hashing;
using IreckonuFileHandler.Core.Services;
using IreckonuFileHandler.Core.Services.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private ILogServices _logService;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ILogServices logService)
        {
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _logService = logService;
        }

        public async Task<CreateUserResponse> CreateUserAsync(UserCredentialsResource user, params ERole[] userRoles)
        {
            try
            {
                var existingUser = await _userRepository.FindByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return new CreateUserResponse(false, "Email already in use.", null);
                }



                User newUser = new User
                {

                    Email = user.Email,
                    Password = _passwordHasher.HashPassword(user.Password),


                };



                await _userRepository.AddAsync(newUser, userRoles);


                await _unitOfWork.CompleteAsync();




                newUser.Password = "";


                return new CreateUserResponse(true, null, newUser);
            }
            catch (Exception ex)
            {

                _logService.LogException($"An error occurred when creating user : { ex.Message}", "Create_User");
                return new CreateUserResponse(false, "Error creating user.", null);
            }
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            try
            {
                return await _userRepository.FindByEmailAsync(email);
            }
            catch (Exception ex)
            {

                _logService.LogException($"An error occurred when retrieving user: { ex.Message}", "Fetch_User");
                return null;
            }
        }
    }
}
