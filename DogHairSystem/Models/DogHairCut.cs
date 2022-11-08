using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogHairSystem.Models
{
    public class DogHairCut
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
