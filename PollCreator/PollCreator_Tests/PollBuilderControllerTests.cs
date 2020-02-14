using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using PollCreator.Controllers;
using PollCreator.Interfaces.Services;
using PollCreator.Models;
using PollCreator.ViewModels;

namespace PollCreator_Tests
{
	class PollBuilderControllerTests
	{
		private PollBuilderController _pollBuilderController;
		private ITokenService _tokenService;

		[SetUp]
		public void Setup()
		{
			_tokenService = Substitute.For<ITokenService>();
			_pollBuilderController = new PollBuilderController(_tokenService);
		}

		[Test]
		public void Create_CorrectlyRedirectsToEdit()
		{
			string tokenString = "tokenString";
			_tokenService.GetRandomToken(Arg.Any<int>()).Returns(tokenString);
			RedirectToActionResult result = _pollBuilderController.Create();

			Assert.AreEqual(2,result.RouteValues.Count,"Check, that we are passing two values: pollId and tokenId");
			Assert.AreEqual("Edit", result.ActionName,"Check, that redirect action is Edit");
			Assert.AreEqual(tokenString,result.RouteValues["pollId"]);
			Assert.AreEqual(tokenString, result.RouteValues["editorId"]);
		}

		[Test]
		public void Edit_ReturnsViewWithModel()
		{
			string tokenString = "tokenString";
			_tokenService.GetRandomToken(Arg.Any<int>()).Returns(tokenString);
			var result = _pollBuilderController.Edit(tokenString, tokenString) as ViewResult;
			Assert.AreEqual("Edit",result.ViewName,"Check, that action is Edit");
			Assert.IsInstanceOf<PollBuilderViewModel>(result.ViewData.Model,"Check,that model is type of PollRequest");
		}

		[Test]
		public void Publish_ReturnsBadRequestResultWhenModelStateIsInvalid()
		{
			_pollBuilderController.ModelState.AddModelError("error","Json Parsing failed and we received null");
			var result = _pollBuilderController.Publish(null);
			Assert.IsInstanceOf<BadRequestResult>(result);
		}

		[Test]
		public void Publish_ReturnsOkResultWhenModelStateIsValid()
		{
			var result = _pollBuilderController.Publish(new PollPublishRequest());
			Assert.IsInstanceOf<OkResult>(result);
		}


	}
}
