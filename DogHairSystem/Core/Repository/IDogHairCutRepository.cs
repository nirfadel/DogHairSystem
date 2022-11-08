using DogHairSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogHairSystem.Core.Repository
{
    public interface IDogHairCutRepository
    {
        List<DogHairCutModel> GetDogHairCutList(int userId);
        void DeleteHairCut(int id);
        DogHairCut UpdateHairCut(DogHairCutModel dogHairCutModel);
        DogHairCut InsertHairCut(DogHairCutModel dogHairCutModel);
        DogHairCut GetDetails(int id);

    }
}
