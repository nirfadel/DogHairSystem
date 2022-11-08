using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DogHairSystem.Models
{
    public class DogHairCutModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public DateTime DueDate { get; set; }
        public bool CanChange { get; set; }

    }
}
