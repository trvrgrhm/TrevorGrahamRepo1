using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.ViewModels;
using MvcStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MvcStoreApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        private readonly BusinessLogic _businessLogic;

        public HomeController(ILogger<HomeController> logger, BusinessLogic businessLogic)
        {
            _logger = logger;
            _businessLogic = businessLogic;
        }

        public IActionResult Index()
        {
            if (_businessLogic.CurrentUserIsAdministrator())
            {
                return RedirectToAction("Administration");
            }
            var locationId = _businessLogic.GetCurrentLocation();

            LocationWithInventoriesViewModel viewModel = _businessLogic.GetInventoryDetails(locationId);

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Administration()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
