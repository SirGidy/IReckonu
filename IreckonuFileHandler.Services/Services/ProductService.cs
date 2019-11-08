using IreckonuFileHandler.Core;
using IreckonuFileHandler.Core.Models;
using IreckonuFileHandler.Core.Repositories;
using IreckonuFileHandler.Core.Resource;
using IreckonuFileHandler.Core.Services;
using IreckonuFileHandler.Core.Services.Communication;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Services.Services
{
    public  class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IDiskService _diskServices;
        private readonly IUnitOfWork _unitOfWork;
        private ILogServices _logService;
        private readonly IOptions<AppSettings> _appSettings;

        public ProductService(IProductRepository productRepository, IDiskService diskServices, IUnitOfWork unitOfWork, ILogServices logService, IOptions<AppSettings> appSettings)
        {
            _productRepository = productRepository;

            _diskServices = diskServices;


            _unitOfWork = unitOfWork;
            _logService = logService;
            _appSettings = appSettings;
        }



        public async Task<IEnumerable<Product>> ListAsync()
        {
            try
            {
                return await _productRepository.ListAsync();
            }
            catch (Exception ex)
            {
                
                _logService.LogException($"An error occurred when retrieving products: { ex.Message}", "Fetch_Products");
                return null;
            }

        }


    


        public async Task<ProductResponse> ProcessFileUpload(List<ProductResource> products)

        {

            ProductResponse result = new ProductResponse();
            try
            {



                if (products.Count() <= 0)
                {

                    result.Success = false;
                    result.Message = "No product to  process";

                }

                else

                {



                    //save json to file path
                    int location = await _diskServices.Save(products);

                    if (location > 0)
                    {



                        //save to database
                        foreach (var aproduct in products)
                        {

                            Product newProduct = new Product
                            {
                                Code = aproduct.ArtikelCode,
                                Color = aproduct.Color,
                                ColorCode = aproduct.ColorCode,
                                Description = aproduct.Description,
                                DiscountPrice = aproduct.DiscountPrice,
                                ExpectedDelivery = aproduct.DeliveredIn,
                                PathId = location,
                                Key = aproduct.Key,
                                Price = aproduct.Price,
                                Q1 = aproduct.Q1,
                                Size = aproduct.Size

                            };


                            await _productRepository.AddAsync(newProduct);

                           

                        }

                        await _unitOfWork.CompleteAsync();

                        result.Success = true;
                        result.Message = "Successfully processed upload";

                    }
                    else
                    {
                        // save to path failed

                        result.Success = false;
                        result.Message = "Save to filepath failed";
                    }

                        


                

                }

                   


               




               

            
            }
            catch (Exception ex)
            {
                // Do some logging stuff

                _logService.LogException($"An error occurred when saving recipe: { ex.Message}", "Save_Upload");

                result.Success = false;
               result.Message = "An error occurred when processing request";
            }



            return result;
        }

    
    }
}
