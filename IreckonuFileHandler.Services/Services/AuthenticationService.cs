using IreckonuFileHandler.Core.Models;
using IreckonuFileHandler.Core.Security.Hashing;
using IreckonuFileHandler.Core.Security.Tokens;
using IreckonuFileHandler.Core.Services;
using IreckonuFileHandler.Core.Services.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenHandler _tokenHandler;

        public AuthenticationService(IUserService userService, IPasswordHasher passwordHasher, ITokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
            _passwordHasher = passwordHasher;
            _userService = userService;
        }

        public async Task<IEnumerable<UserToken>> List()
        {
            return await _tokenHandler.ListRefreshTokenAsync();
        }

        public async Task<TokenResponse> CreateAccessTokenAsync(string email, string password)
        {
            var user = await _userService.FindByEmailAsync(email);

            if (user == null || !_passwordHasher.PasswordMatches(password, user.Password))
            {
                return new TokenResponse(false, "Invalid credentials.", null);
            }

            var token = await _tokenHandler.CreateAccessTokenAsync(user);

            if (token == null)
                return new TokenResponse(false, "error creating token", null);
            return new TokenResponse(true, null, token);
        }

        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken, string userEmail)
        {



            var token = await _tokenHandler.TakeRefreshTokenAsync(refreshToken);

            if (token == null)
            {
                return new TokenResponse(false, "Invalid refresh token.", null);
            }

            if (DateTime.UtcNow.Ticks > token.Expiration)
            {
                return new TokenResponse(false, "Expired refresh token.", null);
            }

            var user = await _userService.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return new TokenResponse(false, "Invalid refresh token.", null);
            }


            var accessToken = await _tokenHandler.CreateAccessTokenAsync(user);
            if (token == null)
                return new TokenResponse(false, "error creating token", null);
            return new TokenResponse(true, null, accessToken);


        }

        public void RevokeRefreshToken(string refreshToken)
        {
            _tokenHandler.RevokeRefreshToken(refreshToken);
        }
    }
}
