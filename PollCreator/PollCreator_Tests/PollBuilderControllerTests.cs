using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using PollCreator.Controllers;
using PollCreator.Interfaces.Services;

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
			Assert.AreEqual(tokenString, result.RouteValues["tokenId"]);
		}

	}
}
