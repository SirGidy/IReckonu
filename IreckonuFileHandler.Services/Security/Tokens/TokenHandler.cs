using IreckonuFileHandler.Core.Models;
using IreckonuFileHandler.Core.Repositories;
using IreckonuFileHandler.Core.Security.Hashing;
using IreckonuFileHandler.Core.Security.Tokens;
using IreckonuFileHandler.Core.Services;
using Microsoft.Extensions.Options;


using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Services.Security.Tokens
{
    public class TokenHandler : ITokenHandler
    {
        private readonly ISet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();

        private readonly TokenOptions _tokenOptions;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly IPasswordHasher _passwordHaser;


        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        private ILogServices _logService;

        public TokenHandler(IOptions<TokenOptions> tokenOptionsSnapshot, SigningConfigurations signingConfigurations, IPasswordHasher passwordHaser, IUserTokenRepository userTokenRepository, IUnitOfWork unitOfWork, ILogServices logService)
        {
            _passwordHaser = passwordHaser;
            _tokenOptions = tokenOptionsSnapshot.Value;
            _signingConfigurations = signingConfigurations;

            _userTokenRepository = userTokenRepository;
            _unitOfWork = unitOfWork;

            _logService = logService;

        }

        public async Task<AccessToken> CreateAccessTokenAsync(User user)
        {
            try
            {
                var refreshToken = BuildRefreshToken(user);

                var accessToken = BuildAccessToken(user, refreshToken);



                // _refreshTokens.Add(refreshToken);

                UserToken userToken = new UserToken
                {
                    RefreshToken = refreshToken.Token,

                    Email = user.Email,
                    Expiration = refreshToken.Expiration,

                    AccessToken = ""


                };

                await _userTokenRepository.AddAsync(userToken);



                await _unitOfWork.CompleteAsync();


                return accessToken;
            }
            catch (Exception ex)
            {

                _logService.LogException(ex, "CreateAccessTokenAsync");


                return null;
            }
        }

        public async Task<IEnumerable<UserToken>> ListRefreshTokenAsync()
        {
            return await _userTokenRepository.ListAsync();
        }

        public async Task<UserToken> TakeRefreshTokenAsync(string token)
        {



            var refreshToken = await _userTokenRepository.FindByTokenAsync(token);


            if (refreshToken != null)
            {

                _userTokenRepository.Remove(refreshToken);
                await _unitOfWork.CompleteAsync();
            }


            return refreshToken;





        }

        public void RevokeRefreshToken(string token)
        {
            TakeRefreshTokenAsync(token);
        }

        private RefreshToken BuildRefreshToken(User user)
        {
            var refreshToken = new RefreshToken
            (
                token: _passwordHaser.HashPassword(Guid.NewGuid().ToString()),
                expiration: DateTime.UtcNow.AddSeconds(_tokenOptions.RefreshTokenExpiration).Ticks
            );

            return refreshToken;
        }

        private AccessToken BuildAccessToken(User user, RefreshToken refreshToken)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddSeconds(_tokenOptions.AccessTokenExpiration);

            var securityToken = new JwtSecurityToken
            (
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: GetClaims(user),
                expires: accessTokenExpiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: _signingConfigurations.SigningCredentials
            );

            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.WriteToken(securityToken);

            return new AccessToken(accessToken, accessTokenExpiration.Ticks, refreshToken);
        }

        private IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email)
            };

            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }

            return claims;
        }
    }
}
