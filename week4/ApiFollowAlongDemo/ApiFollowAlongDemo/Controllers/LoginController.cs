using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFollowAlongDemo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LoginController : ControllerBase
    {
		private BusinessLogicClass _businessLogicClass;
		private readonly ILogger<LoginController> _logger;
		public LoginController(BusinessLogicClass businessLogicClass, ILogger<LoginController> logger)
		{
			_businessLogicClass = businessLogicClass;
			_logger = logger;
		}

		// GET: LoginController
		//[ActionName("Login")]
		[HttpGet("[action]")]
		public ActionResult Login()
		{
			Player p = new Player()
			{
				Fname="First",
				Lname="Last",
			};
			p = null;
            if (p == null)
            {
				return NotFound(p);
            }
            else {
				return Ok(p);
            }
			//return Ok(p);
		}

		[ActionName("LoginPlayer")]
		public ActionResult Login(LoginPlayerViewModel loginPlayerViewModel)
		{
			// instead of doing logic here, call a method in the business logic 
			// layer to create teh player, persist to the Db, and return a player to display.
			// use DI (Dependency Injection) to get an instance to the business class and access to itds functionality.
			PlayerViewModel playerViewModel = _businessLogicClass.LoginPlayer(loginPlayerViewModel);

			//_logger.LogInformation($"The LogininPlayer() returned NUll");

			///do things to log in....
			return Ok(playerViewModel);
		}
	}
}
