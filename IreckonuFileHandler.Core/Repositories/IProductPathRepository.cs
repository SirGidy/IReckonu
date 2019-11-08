using IreckonuFileHandler.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Core.Repositories
{
    public interface IProductPathRepository
    {

        Task<IEnumerable<ProductPath>> ListAsync();

        IQueryable<ProductPath> Search();
        Task AddAsync(ProductPath productpath);


        Task<ProductPath> FindByIdAsync(int id);
        void Update(ProductPath productpath);
        void Remove(ProductPath productpath);
    }



    //public class SigningConfigurations
    //{
    //    public SecurityKey Key { get; }
    //    public SigningCredentials SigningCredentials { get; }

    //    public SigningConfigurations()
    //    {
    //        using (var provider = new RSACryptoServiceProvider(2048))
    //        {
    //            Key = new RsaSecurityKey(provider.ExportParameters(true));
    //        }

    //        SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
    //    }
    //}
}
