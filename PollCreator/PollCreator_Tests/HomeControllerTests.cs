using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PollCreator.Controllers;

namespace PollCreator_Tests
{
	class HomeControllerTests
	{
		private HomeController _homeController;

		[SetUp]
		public void Setup()
		{
			_homeController = new HomeController();
		}

		[Test]
		public void Index_ReturnsCorrectView()
		{
			ViewResult viewResult = _homeController.Index() as ViewResult;
			Assert.AreEqual("Index", viewResult.ViewName);
		}
	}
}
