using IreckonuFileHandler.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Core.Security.Tokens
{
    public interface ITokenHandler
    {
        Task<AccessToken> CreateAccessTokenAsync(User user);
        //RefreshToken TakeRefreshTokenAsync(string token);

        Task<UserToken> TakeRefreshTokenAsync(string token);
        void RevokeRefreshToken(string token);
        Task<IEnumerable<UserToken>> ListRefreshTokenAsync();
    }
}
