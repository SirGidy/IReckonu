using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Core
{
    public class AppSettings
    {
        public string JsonSavePath { get; set; }
        public string FileUploadPath { get; set; }



        public Jwt Jwt { get; set; }
    }


    public class Jwt
    {

        public string Key { get; set; }
        public string Issuer { get; set; }


    }
}
