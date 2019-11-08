using IreckonuFileHandler.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Core.Repositories
{
    public interface IUserTokenRepository
    {
        Task AddAsync(UserToken userToken);
        Task<UserToken> FindByIdAsync(int id);

        Task<UserToken> FindByTokenAsync(string refreshtoken);

        void Update(UserToken userToken);

        Task<UserToken> FindByEmailAsync(string EmailAddress);

        Task<IEnumerable<UserToken>> ListAsync();


        void Remove(UserToken userToken);
    }
}
