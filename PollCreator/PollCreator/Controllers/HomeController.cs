﻿using Microsoft.AspNetCore.Mvc;

namespace PollCreator.Controllers
{
	[Route("~")]
	public class HomeController : Controller
	{

		[Route("")]
		[HttpGet]
		public IActionResult Index()
		{
			return View("Index");
		}

	}
}
