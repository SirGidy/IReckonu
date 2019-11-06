using IreckonuFileHandler.Core.Repositories;
using IreckonuFileHandler.Services.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork


    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
