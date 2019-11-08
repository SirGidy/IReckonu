using IreckonuFileHandler.Core.Models;
using IreckonuFileHandler.Core.Security.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Core.Services.Communication
{
    public class TokenResponse
    {
        public AccessToken Token { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }


        public TokenResponse(bool success, string message, AccessToken token) 
        {
            Token = token;
       

           
            Success = success;
            Message = message;

        }
    }
}
