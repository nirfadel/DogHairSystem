using DogHairSystem.Core.DB;
using DogHairSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Security.Claims;

namespace DogHairSystem.Core.Repository
{
    public class DogHairCutRepository : IDogHairCutRepository
    {
        private readonly DogHairDBContext _dogHairDBContext;
        private readonly IConfiguration _configuration;
        private string connectionString = string.Empty;
        public DogHairCutRepository(DogHairDBContext dogHairDBContext, IConfiguration configuration)
        {
            _dogHairDBContext = dogHairDBContext;
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
            
        }

        public void DeleteHairCut(int id)
        {
            using (_dogHairDBContext)
            {
                var dogHairCut = _dogHairDBContext.DogHairCuts.Where(d => d.Id == id).FirstOrDefault();
                if(dogHairCut != null)
                {
                    _dogHairDBContext.DogHairCuts.Remove(dogHairCut);
                    _dogHairDBContext.SaveChanges();
                }
            }
        }

        public DogHairCut InsertHairCut(DogHairCutModel dogHairCutModel)
        {
            using (_dogHairDBContext)
            {
                DogHairCut dogHairCut = new DogHairCut() {
                    DueDate = dogHairCutModel.DueDate,
                    UserId = dogHairCutModel.UserId,
                    InsertDate = DateTime.Now
                };
                _dogHairDBContext.Add(dogHairCut);
                _dogHairDBContext.SaveChanges();
                return dogHairCut;
            }
        }

        public DogHairCut UpdateHairCut(DogHairCutModel dogHairCutModel)
        {
            DogHairCut dogHairCut = null;
            using (_dogHairDBContext)
            {
                dogHairCut = _dogHairDBContext.DogHairCuts.Where(d => d.Id == dogHairCutModel.Id).FirstOrDefault();
                if(dogHairCut != null)
                {
                    dogHairCut.DueDate = dogHairCutModel.DueDate;
                    _dogHairDBContext.SaveChanges();
                    
                }
                return dogHairCut;
            }
        }

        public List<DogHairCutModel> GetDogHairCutList(int userId)
        {
            List<DogHairCutModel> hairCutList = null;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_HairCutList", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        var reader  = cmd.ExecuteReader();
                        hairCutList = new List<DogHairCutModel>();
                        while (reader.Read())
                        {
                            hairCutList.Add(new DogHairCutModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                DueDate = Convert.ToDateTime(reader["DueDate"]),
                                FirstName = Convert.ToString(reader["FirstName"]),
                                CanChange = Convert.ToInt32(reader["UserId"]) == userId ? true : false
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return hairCutList;
        }

        public DogHairCut GetDetails(int id)
        {
            DogHairCut dogHairCut = null;
            try
            {
                using (_dogHairDBContext)
                {
                    dogHairCut = _dogHairDBContext.DogHairCuts.Where(d => d.Id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return dogHairCut;
        }
    }
}
