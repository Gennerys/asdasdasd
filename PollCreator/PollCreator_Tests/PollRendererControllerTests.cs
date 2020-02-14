using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PollCreator.Controllers;

namespace PollCreator_Tests
{
	class PollRendererControllerTests
	{
		private PollRenderer _pollRendererController;

		[SetUp]
		public void Setup()
		{
			_pollRendererController = new PollRenderer();
		}
		[Test]
		public void Vote_ReturnsCorrectView()
		{
			ViewResult viewResult = _pollRendererController.Vote("randomId") as ViewResult;
			Assert.AreEqual("Vote", viewResult.ViewName);
		}
	}
}
