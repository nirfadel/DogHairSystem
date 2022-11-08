using DogHairSystem.Core.DB;
using DogHairSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DogHairSystem.Core.Jwt.TokenService;

namespace DogHairSystem.Core.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DogHairDBContext _dogHairDBContext;
        public UserRepository(DogHairDBContext dogHairDBContext)
        {
            _dogHairDBContext = dogHairDBContext;
        }


        public User GetUser(UserModel userModel)
        {
            User user = null;
            try
            {
                using (_dogHairDBContext)
                {
                    string CryptedPassword = Helper.Helper.GetMD5(userModel.Password);
                    user = _dogHairDBContext.Users.Where(u => u.UserName.ToLower() == userModel.UserName.ToLower() &&
                    u.Password == CryptedPassword).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return user;
        }

        public bool SaveUser(UserModel userModel)
        {
            try
            {
                using (_dogHairDBContext)
                {
                    string CryptedPassword = Helper.Helper.GetMD5(userModel.Password);
                User user = _dogHairDBContext.Users.Where(u => u.UserName.ToLower() == userModel.UserName.ToLower()).FirstOrDefault();
                if (user == null)
                {
                   
                        string cryptedPassword = Helper.Helper.GetMD5(userModel.Password);
                        User newUser = new User() {
                            FirstName = userModel.FirstName,
                            UserName = userModel.UserName,
                            Password = cryptedPassword,
                            Role = DHRoles.StandartUser.ToString()
                        };
                        _dogHairDBContext.Users.Add(newUser);
                        _dogHairDBContext.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return false;
        }
    }
}
