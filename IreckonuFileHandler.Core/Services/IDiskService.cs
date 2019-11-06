using IreckonuFileHandler.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Core.Services
{
    public interface IDiskService
    {
        Task<int> Save(List<ProductResource> product);
    }
}
