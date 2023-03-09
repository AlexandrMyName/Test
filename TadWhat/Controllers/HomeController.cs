using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TadWhat.Models;
using TadWhat.Repository;
using TadWhat.Domain;
using TadWhat.Encoding;
using TadWhat.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Dadata.Model;

namespace TadWhat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IConfigurationRoot api_dadata;
        ICrypto crypto;
        UserRepo userRepository;
        AddreseRussian addresseble;

        public HomeController(
            ILogger<HomeController> logger,
            UserRepo userRepository,
            ICrypto crypto,
            AddreseRussian addressSet,
            IHostEnvironment env
            )
        {
            api_dadata = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("dataToken.json").Build();
            addresseble = addressSet;
            this.crypto = crypto;
            crypto.Secret = api_dadata.GetConnectionString("secret");
            crypto.Token = api_dadata.GetConnectionString("token");
            _logger = logger;
            this.userRepository = userRepository;
        }
        
        public IActionResult Index() =>  View();
        public IActionResult Privacy() =>  View();
        [HttpGet]
        public IActionResult Success(JsonResult json) =>  View(json);
        public IActionResult GetInfoAboutAddress() =>  View();

        
         [HttpPost]
        public async Task<IActionResult> GetInfoAboutAddress(UserAddress userAddress)
        {

            if (string.IsNullOrEmpty(userAddress.City))
                ModelState.AddModelError(nameof(userAddress.City), "Введите город");
            
            if (string.IsNullOrEmpty(userAddress.Street))
                ModelState.AddModelError(nameof(userAddress.Street), "Введите улицу");
            
            if (string.IsNullOrEmpty(userAddress.House))
                ModelState.AddModelError(nameof(userAddress.House), "Введите дом");
            

            if (ModelState.IsValid){

                        await addresseble.Start(userAddress.City, userAddress.Street, userAddress.House, crypto.Token, crypto.Secret);
                Address adress = addresseble.Address;
                if (adress.postal_code == null){

                    ModelState.AddModelError(nameof(userAddress.House), "Похоже что то не так..");
                    return View(userAddress);
                }
                JsonResult result = Json(adress);
                return View("Success", result);
            }
            else return View(userAddress);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        
    }
}
