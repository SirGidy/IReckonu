using IreckonuFileHandler.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
}
