using IreckonuFileHandler.Services.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IreckonuFileHandler.Services.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
