using IreckonuFileHandler.Core;
using IreckonuFileHandler.Core.Models;
using IreckonuFileHandler.Core.Repositories;
using IreckonuFileHandler.Core.Resource;
using IreckonuFileHandler.Core.Services;
using IreckonuFileHandler.Services.Helper;
using IreckonuFileHandler.Services.Repositories;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace IreckonuFileHandler.Services.Services
{
    public class DiskService : IDiskService
    {


        private readonly IProductPathRepository _productPathRepository;
       
        private readonly IUnitOfWork _unitOfWork;
        private ILogServices _logService;
        private readonly IOptions<AppSettings> _appSettings;

        public readonly IHostingEnvironment _hostingEnvironment;

        private string json;
        private string filename;
        public DiskService(IProductPathRepository productPathRepository, IHostingEnvironment hostingEnvironment, IUnitOfWork unitOfWork, ILogServices logService, IOptions<AppSettings> appSettings)
        {
            _productPathRepository = productPathRepository;

            _hostingEnvironment = hostingEnvironment;



            _unitOfWork = unitOfWork;
            _logService = logService;
            _appSettings = appSettings;

        }


        public async Task<int> Save(List<ProductResource> products)

        {
            int id = 0;
            try
            {

                
                 json = JsonConvert.SerializeObject(products.ToArray(), Formatting.Indented);
             
                filename = Path.Combine(_hostingEnvironment.ContentRootPath, _appSettings.Value.JsonSavePath, RandomString.GenerateRandomString(10) + ".json");
                //write string to file
                System.IO.File.WriteAllText(filename, json);

                ProductPath path = new ProductPath
                {
                    JsonFilePath = filename
                };


                await _productPathRepository.AddAsync(path);

                await _unitOfWork.CompleteAsync();

                id = path.Id;
            }
            catch (Exception ex)
            {

                _logService.LogException(ex, "Save File Path");
            }



            return id;
        }
    }
}
