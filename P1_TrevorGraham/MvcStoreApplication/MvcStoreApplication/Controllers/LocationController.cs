using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcStoreApplication.Controllers
{
    public class LocationController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly BusinessLogic _businessLogic;

        public LocationController(ILogger<HomeController> logger, BusinessLogic businessLogic)
        {
            _logger = logger;
            _businessLogic = businessLogic;
        }

        // GET: LocationController
        public ActionResult Index()
        {
            return View(_businessLogic.GetAllLocationViewModels());
        }

        // GET: LocationController/Details/5
        public ActionResult Details(Guid id)
        {
            return View(_businessLogic.GetLocationViewModel(id));
        }

        // GET: LocationController/InventoryDetails/5
        public ActionResult InventoryDetails(Guid id)
        {
            //--------------------------------------------------------------Remember to delete this method please-----------------------------------------------------------
            //_businessLogic.TempMethodDeleteMePlease();
            return View(_businessLogic.GetInventoryDetails(id));
        }


        // GET: LocationController/AllLocations
        public ActionResult AllLocations()
        {
            return View(_businessLogic.GetAllLocationViewModels());
        }


        // GET: LocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: LocationController/CreateInventory

        public ActionResult CreateInventory(Guid locationId)
        {
            var loc = _businessLogic.GetLocationViewModel(locationId);
            InventoryViewModel viewModel = new InventoryViewModel()
            {
                LocationId = loc.LocationId,
                LocationName = loc.Name
            };
            ViewBag.Products = _businessLogic.GetAllProductViewModels();
            return View(viewModel);
        }
        //Post: LocationController/FinishCreateInventory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinishCreateInventory(InventoryViewModel viewModel)
        {
            //TODO: implement create inventory in business logic
            //_businessLogic.
            if (_businessLogic.AddProductToInventory(viewModel))
            return RedirectToAction(nameof(Index));
            //if unsuccessful
            return View("CreateInventory", viewModel.LocationId);
        }

        // POST: LocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(nameof(LocationViewModel.Name))] LocationViewModel newLocation)
        {
            try
            {
                _businessLogic.CreateNewLocation(newLocation);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LocationController/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View(_businessLogic.GetLocationViewModel(id));
        }

        // POST: LocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(nameof(LocationViewModel.LocationId),nameof(LocationViewModel.Name))] LocationViewModel newLocation)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
