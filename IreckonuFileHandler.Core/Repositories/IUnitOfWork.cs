using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Core.Repositories
{
    public interface IUnitOfWork
    {

        Task CompleteAsync();
    }
}
