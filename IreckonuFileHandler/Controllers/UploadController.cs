using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IreckonuFileHandler.Core;
using IreckonuFileHandler.Core.Models;
using IreckonuFileHandler.Core.Repositories;
using IreckonuFileHandler.Core.Resource;
using IreckonuFileHandler.Core.Services;
using IreckonuFileHandler.Core.Services.Communication;
using IreckonuFileHandler.Services.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IreckonuFileHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        private readonly IProductService _productService;
     
        private ILogServices _logService;
        private readonly IOptions<AppSettings> _appSettings;

        public UploadController(IProductService productService, ILogServices logService, IOptions<AppSettings> appSettings)
        {
            _productService = productService;

          
            _logService = logService;
            _appSettings = appSettings;
        }


        /// <summary>
        /// This method is used to process file upload </summary>

       

        /// <returns> Initiate File Upload  </returns>
        /// <response code="200">Returns if the operation was successful.</response>
        /// <response code="400">Returns if file uploaded is invalid.</response>            
        /// <response code="500">Returns if file upload failed.</response>
        /// <remarks>
        ///</remarks>
        ///

        
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(200, Type = typeof(ProductResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize]

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadAsync()
        {

            ProductResponse result = new ProductResponse();
            try
            {

                var file = Request.Form.Files[0];
                //var folderName = Path.Combine("Resources", "Images");
                //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    //var fullPath = Path.Combine(pathToSave, fileName);
                    //var dbPath = Path.Combine(folderName, fileName);



                    string fileExtension = Path.GetExtension(fileName);

                    if (fileExtension != ".csv")
                    {
                        return BadRequest("File type not allowed");
                    }





                    var products = new List<ProductResource>();

                    using (var stream = file.OpenReadStream())
                    {
                        try
                        {
                            products = stream.CsvToList<ProductResource>();



                           result =  await _productService.ProcessFileUpload(products);

                        }
                        catch (Exception ex)
                        {
                            return BadRequest(ex.Message);
                        }
                    }

                    
                    if(result.Success)
                     return Ok( result);
                    



                    
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logService.LogException(ex, "Product Upload");
                return StatusCode(500, "Internal server error");
            }


            return BadRequest();
        }




        /// <summary>
        /// This method is used to fetch the details of all products .</summary>




        /// <returns>  </returns>
        /// <response code="200">Returns if the operation was successful.</response>

        /// <response code="204">Returns if there are no products.</response>

        /// <remarks>
        ///</remarks>



        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]

        [ProducesResponseType(204)]
        // GET: api/<controller>
        [HttpGet]
        //[Route("List")]
        public async Task<IActionResult> List()
        {


            try
            {
                var recipes = await _productService.ListAsync();

                if (recipes == null)
                    return NoContent();

                return Ok(recipes);

            }
            catch (Exception ex)
            {

                _logService.LogException(ex, "Products");
                return NoContent();
            }





        }

    }
}
