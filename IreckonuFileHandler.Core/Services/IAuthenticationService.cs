using IreckonuFileHandler.Core.Models;
using IreckonuFileHandler.Core.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Core.Services
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> CreateAccessTokenAsync(string email, string password);
        Task<TokenResponse> RefreshTokenAsync(string refreshToken, string userEmail);
        void RevokeRefreshToken(string refreshToken);


        Task<IEnumerable<UserToken>> List();
    }
}
