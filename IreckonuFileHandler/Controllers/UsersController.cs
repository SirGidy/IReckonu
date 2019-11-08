using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IreckonuFileHandler.Core.Models;
using IreckonuFileHandler.Core.Resource;
using IreckonuFileHandler.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IreckonuFileHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {


        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;

        }


        /// <summary>
        /// This method is used to register user to obtain access to the API.</summary>

        /// <param name="userCredentials">  </param>


        /// <returns>  </returns>
        /// <response code="200">Returns if the operation was successful.</response>
        /// <response code="400">Returns if parameter is not valid.</response>            

        /// <remarks>
        /// 
        ///</remarks>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCredentialsResource userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var response = await _userService.CreateUserAsync(userCredentials, ERole.Common);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }


            return Ok(response);
        }
    }
}
