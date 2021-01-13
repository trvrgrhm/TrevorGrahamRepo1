using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcStoreApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BusinessLogic _businessLogic;

        public OrderController(ILogger<HomeController> logger, BusinessLogic businessLogic)
        {
            _logger = logger;
            _businessLogic = businessLogic;
        }

        public ActionResult AddToCart(InventoryViewModel viewModel)
        {
            //_businessLogic.AddToCart(viewModel)
            //_businessLogic.

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmAddToCart(OrderLineViewModel viewModel)
        {
            if (_businessLogic.CurrentUserIsCustomer())
            {
                viewModel.OrderLineId =Guid.NewGuid();
                viewModel.TotalPrice = viewModel.Quantity * viewModel.Price;
                _businessLogic.AddToCart(viewModel);
                //add to cart
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ViewCart()
        {
            if (!_businessLogic.CurrentUserIsCustomer())
            {
                return RedirectToAction("Index", "Home");
            }
            var cart = _businessLogic.GetCart();
            //ViewBag.orderLines = cart.orderLines;
            return View(cart);
        }
        public ActionResult Checkout(Guid orderId)
        {
            if (!_businessLogic.CurrentUserIsCustomer())
            {
                return RedirectToAction("Index", "Home");
            }
            _businessLogic.Checkout();
            return RedirectToAction("OrderDetails", new { id = orderId });
        }
        public ActionResult OrderDetails(Guid id)
        {
            if (!_businessLogic.UserIsSignedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            var viewModel = _businessLogic.GetOrderViewModel(id);
            return View(viewModel);
        }

        public ActionResult ListOrders()
        {
            if (!_businessLogic.CurrentUserIsCustomer())
            {
                return RedirectToAction("Index", "Home");
            }
            var orders = _businessLogic.GetAllOrderViewModels();
            return View(orders);

        }
    }
}
