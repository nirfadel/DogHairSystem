using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogHairSystem.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
