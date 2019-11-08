using IreckonuFileHandler.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Core.Services.Communication
{
    public class CreateUserResponse
    {

        public User User { get;  set; }

        public bool Success { get; set; }
        public string Message { get; set; }



        public CreateUserResponse(bool success, string message, User user) 
        {

            User = user;
            Success = success;
            Message = message;

        }


    }
}
