using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogHairSystem.Core.DB
{
    public class DogHairDBContext : DbContext
    {
        public DogHairDBContext()
        {
        }
        public DogHairDBContext(DbContextOptions<DogHairDBContext> options) : base(options)
        {

        }
    }
}
