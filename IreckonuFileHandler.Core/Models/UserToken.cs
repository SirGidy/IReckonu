using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Core.Models
{
    public class UserToken
    {

        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        public string Email { get; set; }

        //[Required]
        public string AccessToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }


        [Required]

        public long Expiration { get; set; }
    }
}
