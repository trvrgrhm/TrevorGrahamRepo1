using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpsFollowAlong.Controllers
{
    public class PlayerController : Controller
    {
        private readonly BusinessLogic _businessLogic;
        private ILogger<PlayerController> _logger;
        public PlayerController(BusinessLogic businessLogic, ILogger<PlayerController> logger)
        {
            _businessLogic = businessLogic;
            _logger = logger;
        }


        // GET: PlayerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PlayerController/Details/5
        public ActionResult Details(Guid playerId)
        {

            return View("DisplayPlayerDetails", _businessLogic.GetPlayerViewById(playerId));
        }

        // GET: PlayerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlayerController/Create
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

        // GET: PlayerController/Edit/5
        //[Route("{playerGuid}")]
        public ActionResult Edit(Guid playerId)
        {
            PlayerViewModel playerViewModel = _businessLogic.GetPlayerViewById(playerId);
            //call a method in BusinessLogic that takes playerId and returns a playerViewModel
            return View(playerViewModel);
        }
        // GET: PlayerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("EditedPlayer")]
        public ActionResult EditPlayer(PlayerViewModel playerViewModel)
        {
            PlayerViewModel playerViewModel1 = _businessLogic.EditedPlayer(playerViewModel);
            //call a method in BusinessLogic that takes playerId and returns a playerViewModel
            return View("DisplayPlayerDetails",playerViewModel1);
        }

        //// POST: PlayerController/Edit/5
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

        // GET: PlayerController/Delete/5
        public ActionResult Delete(Guid playerId)
        {
            return View(_businessLogic.GetPlayerViewById(playerId));
        }

        // POST: PlayerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid playerId, IFormCollection collection)
        {
            try
            {
                _businessLogic.DeletePlayer(playerId);
                return RedirectToAction("ListPlayers");
            }
            catch
            {
                return View();
            }
        }

        public IActionResult ListPlayers()
        {
            var players = _businessLogic.GetAllPlayerViewModels();
            //view generated using a list of playerViewModels
            return View(players);
        }
    }
}
