using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpsFollowAlong.Controllers
{
    public class LoginController : Controller
    {
        private readonly BusinessLogic _businessLogic;
        private ILogger<LoginController> _logger;
        public LoginController(BusinessLogic businessLogic, ILogger<LoginController> logger)
        {
            _businessLogic = businessLogic;
            _logger = logger;
        }

        // GET: LoginController
        //[ActionName("Login")]
        public ActionResult Login()
        {
            return View();
        }
        //POST
        [ActionName("LoginPlayer")]
        public ActionResult Login(LoginPlayerViewModel loginPlayerViewModel)
        {
            //instead of doing logic here, 
            //call a method in the business logic layer to create the player, 
            //persist to the db, and return a player to display
            //user DI(dependency injection) to get an instance of the business class and access its functionality
            PlayerViewModel playerViewModel = _businessLogic.LoginPlayer(loginPlayerViewModel);

            //_logger.LogInformation($"");

            return View("DisplayPlayerDetails", playerViewModel);
        }

        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
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
