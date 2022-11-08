using DogHairSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogHairSystem.Core.Repository
{
    public interface IUserRepository
    {
        User GetUser(UserModel userMode);
    }
}
