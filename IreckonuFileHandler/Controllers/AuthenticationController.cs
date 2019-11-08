using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IreckonuFileHandler.Core.Resource;
using IreckonuFileHandler.Core.Services;
using IreckonuFileHandler.Core.Services.Communication;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IreckonuFileHandler.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {


        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

        }




        /// <summary>
        /// This method is used to authenticate users of the API.</summary>

        /// <param name="userCredentials">  </param>


        /// <returns>  </returns>
        /// <response code="200">Returns if the operation was successful.</response>
        /// <response code="400">Returns if parameter is not valid.</response>            

        /// <remarks>
        /// Access token and Refresh tokens are returned on successful authentication. Kindly add the Access Token to the  Authorization header to access protected API method
        ///</remarks>



        [ProducesResponseType(200, Type = typeof(TokenResponse))]
        [ProducesResponseType(400)]

        [Route("/api/login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserCredentialsResource userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authenticationService.CreateAccessTokenAsync(userCredentials.Email, userCredentials.Password);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }


            return Ok(response);
        }


        /// <summary>
        /// This method is used to extend the validity of  access token issued to users of the API.</summary>

        /// <param name="refreshTokenResource">  </param>


        /// <returns>  </returns>
        /// <response code="200">Returns if the operation was successful.</response>
        /// <response code="400">Returns if parameter is not valid.</response>            

        /// <remarks>
        /// 
        ///</remarks>
        [ProducesResponseType(200, Type = typeof(TokenResponse))]
        [ProducesResponseType(400)]
        [Route("/api/token/refresh")]
        [HttpPost]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenResource refreshTokenResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authenticationService.RefreshTokenAsync(refreshTokenResource.Token, refreshTokenResource.UserEmail);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }


            return Ok(response);
        }


        /// <summary>
        /// This method is used to revoke the access token issued to users of the API.</summary>

        /// <param name="revokeTokenResource">  </param>


        /// <returns>  </returns>
        /// <response code="204">Returns if the operation was successful.</response>
        /// <response code="400">Returns if parameter is not valid.</response>            

        /// <remarks>
        /// 
        ///</remarks>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Route("/api/token/revoke")]
        [HttpPost]
        public IActionResult RevokeToken([FromBody] RevokeTokenResource revokeTokenResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _authenticationService.RevokeRefreshToken(revokeTokenResource.Token);
            return NoContent();
        }
    }
}
