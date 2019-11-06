using IreckonuFileHandler.Core.Models;
using IreckonuFileHandler.Core.Repositories;
using IreckonuFileHandler.Services.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Services.Repositories
{
 


    public class ProductPathRepository : BaseRepository, IProductPathRepository
    {

        public ProductPathRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProductPath>> ListAsync()
        {

            return await _context.ProductPaths.ToListAsync();

        }


        public IQueryable<ProductPath> Search()
        {

            return _context.ProductPaths.AsQueryable();

        }




        public async Task AddAsync(ProductPath product)
        {
            await _context.ProductPaths.AddAsync(product);


        }

        public async Task<ProductPath> FindByIdAsync(int id)
        {
            return await _context.ProductPaths.FindAsync(id);
        }

        public void Update(ProductPath product)
        {
            _context.ProductPaths.Update(product);
        }

        public void Remove(ProductPath product)
        {
            _context.ProductPaths.Remove(product);
        }

    }
}
