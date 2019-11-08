using IreckonuFileHandler.Core.Models;
using IreckonuFileHandler.Core.Repositories;
using IreckonuFileHandler.Services.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Services.Repositories
{
    public class UserTokenRepository : IUserTokenRepository
    {

        private readonly AppDbContext _context;

        public UserTokenRepository(AppDbContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<UserToken>> ListAsync()
        {


            return await _context.UserTokens.ToListAsync();



        }


        public async Task AddAsync(UserToken userToken)
        {
            await _context.UserTokens.AddAsync(userToken);
        }

        public async Task<UserToken> FindByIdAsync(int id)
        {
            return await _context.UserTokens.FindAsync(id);
        }

        public async Task<UserToken> FindByEmailAsync(string EmailAddress)
        {
            return await _context.UserTokens.SingleOrDefaultAsync(u => u.Email == EmailAddress && u.Expiration > DateTime.UtcNow.Ticks);
        }

        public async Task<UserToken> FindByTokenAsync(string refreshtoken)
        {


            return await _context.UserTokens.SingleOrDefaultAsync(u => u.RefreshToken == refreshtoken);
        }


        public void Update(UserToken userToken)
        {
            _context.UserTokens.Update(userToken);
        }



        public void Remove(UserToken userToken)
        {
            _context.UserTokens.Remove(userToken);
        }
    }
}
