using DogHairSystem.Core.Jwt;
using DogHairSystem.Core.Repository;
using DogHairSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DogHairSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly IDogHairCutRepository _dogHairCutRepository;
        private readonly ITokenService _tokenService;
        private string generatedToken = null;
        public HomeController(ILogger<HomeController> logger, 
                              IConfiguration config, 
                              ITokenService tokenService,
                              IUserRepository userRepository,
                              IDogHairCutRepository dogHairCutRepository)
        {
            _logger = logger;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _dogHairCutRepository = dogHairCutRepository;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login(UserModel userModel)
        {
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
            {
                return (RedirectToAction("Error"));
            }
            IActionResult response = Unauthorized();
            var validUser = GetUser(userModel);
            if (validUser != null)
            {
                generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), validUser);
                if (generatedToken != null)
                {
                    HttpContext.Response.Cookies.Append("Token", generatedToken);
                    var result = _dogHairCutRepository.GetDogHairCutList(validUser.Id);
                    return View("~/Views/Home/DogHair.cshtml", result);
                }
                else
                {
                    return (RedirectToAction("Error"));
                }
            }
            else
            {
                return (RedirectToAction("Error"));
            }
        }

        private User GetUser(UserModel userModel)
        {
            return _userRepository.GetUser(userModel);
        }

        //[Authorize]
        [Route("mainwindow")]
        [HttpGet]
        public IActionResult MainWindow()
        {
            string token = HttpContext.Request.Cookies["Token"];
            if (token == null)
            {
                return (RedirectToAction("Index"));
            }
            if (!_tokenService.IsTokenValid(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), token))
            {
                return (RedirectToAction("Index"));
            }
            //ViewBag.Message = BuildMessage(token, 50);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.Message = "An error occured...";
            return View();
        }

        private string BuildMessage(string stringToSplit, int chunkSize)
        {
            var data = Enumerable.Range(0, stringToSplit.Length / chunkSize).Select(i => stringToSplit.Substring(i * chunkSize, chunkSize));
            string result = "The Token is:";
            foreach (string str in data)
            {
                result += Environment.NewLine + str;
            }
            return result;
        }

        public ActionResult SignUp()
        {
            return View();
        }

        //POST: SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserModel _user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.SaveUser(_user);
                return RedirectToAction("Index");
            }
            return View();


        }

    }
}

