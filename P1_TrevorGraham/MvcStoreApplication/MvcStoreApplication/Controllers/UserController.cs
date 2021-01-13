using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcStoreApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BusinessLogic _businessLogic;
        //private readonly SessionManager _session;

        public UserController(ILogger<HomeController> logger, BusinessLogic businessLogic)
        {
            _logger = logger;
            _businessLogic = businessLogic;
            //_session = new SessionManager(HttpContext, _businessLogic);
        }
        // GET: UserController
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignIn()
        {
            if (_businessLogic.UserIsSignedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            //check if already signed in
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(string username, string password)
        {
            try
            {
                if (_businessLogic.AttemptSignIn(username, password))
                {
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SignOutUser()
        {
            _businessLogic.SignOut();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult ListAllCustomers()
        {
            //var curCust = _businessLogic.GetCurrentCustomer();
            //var curUser = _session.GetCurrentUser(HttpContext);
            if (_businessLogic.CurrentUserIsAdministrator())
            {

                return View(_businessLogic.GetAllCustomerViewModels());

        }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: UserController/Details/5
        public ActionResult Details(Guid id)
        {
            //if (_businessLogic.UserIsSignedIn())
            //{
                if (_businessLogic.CurrentUserIsCustomer())
                {
                    return View("CustomerDetails", _businessLogic.GetCustomerViewModel(id));
                }
                else if (_businessLogic.CurrentUserIsAdministrator())
                {
                    return View("CustomerDetails", _businessLogic.GetAdministratorViewModel(id));
                }
            //}
            //not signed in or something
            return RedirectToAction("Index","Home") ;
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            //return View("CreateAdmin");
            ViewBag.Locations = _businessLogic.GetAllLocationViewModels();
            return View("CreateCustomer");
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomer(CustomerViewModel customer)
        {
            try
            {
                _businessLogic.CreateNewCustomer(customer);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdmin(AdministratorViewModel admin)
        {
            //comment this out to allow creation of new admin
            if (!_businessLogic.CurrentUserIsAdministrator())
            {
                return RedirectToAction("Home", "Index");
            }
            try
            {
                _businessLogic.CreateNewAdministrator(admin);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: UserController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: UserController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: UserController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
