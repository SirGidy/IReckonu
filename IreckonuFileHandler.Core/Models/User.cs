using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Core.Models
{
    public class User
    {
        public long Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new Collection<UserRole>();
    }
}
