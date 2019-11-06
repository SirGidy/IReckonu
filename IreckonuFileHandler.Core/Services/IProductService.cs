using IreckonuFileHandler.Core.Models;
using IreckonuFileHandler.Core.Resource;
using IreckonuFileHandler.Core.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Core.Services
{
    public interface IProductService
    {

    


             Task<ProductResponse> ProcessFileUpload(List<ProductResource> request);

        Task<IEnumerable<Product>> ListAsync();

    }
}
