using DogHairSystem.Core.Helper;
using DogHairSystem.Core.Jwt;
using DogHairSystem.Core.Repository;
using DogHairSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DogHairSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DogHairController : Controller
    {
        private readonly IDogHairCutRepository _dogHairCutRepository;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;
        public DogHairController(IDogHairCutRepository dogHairCutRepository,
            IHttpContextAccessor httpContextAccessor, ITokenService tokenService)
        {
            _dogHairCutRepository = dogHairCutRepository;
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("DeleteHairCut")]
        public IActionResult Delete(int id)
        {
           _dogHairCutRepository.DeleteHairCut(id);
            return Ok();
        }


        [HttpPut]
        public IActionResult Put(DogHairCutModel dogHairCutModel)
        {
            var result = _dogHairCutRepository.UpdateHairCut(dogHairCutModel);
            return Ok(result);
        }

        //[AuthorizeUserAttribute]
        [HttpPost]
        public IActionResult Post(DogHairCutModel dogHairCutModel)
        {
            var token = Request.Cookies["Token"];
            var res = _tokenService.GetClaimValue(token, "UserId");
            if (string.IsNullOrEmpty(res))
                return BadRequest();
            dogHairCutModel.UserId = Convert.ToInt32(res);
            var result = _dogHairCutRepository.InsertHairCut(dogHairCutModel);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetDogHairDetails")]
        public IActionResult GetDogHairDetails(int id)
        {
            var res = _dogHairCutRepository.GetDetails(id);
            return Ok(res);
        }
    }
}
