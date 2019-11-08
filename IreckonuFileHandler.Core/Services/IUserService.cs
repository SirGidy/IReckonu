using IreckonuFileHandler.Core.Models;
using IreckonuFileHandler.Core.Resource;
using IreckonuFileHandler.Core.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Core.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateUserAsync(UserCredentialsResource user, params ERole[] userRoles);
        Task<User> FindByEmailAsync(string email);
    }
}
