using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using PollCreator.Controllers;
using PollCreator.Interfaces.Services;
using PollCreator.Models;
using PollCreator.ViewModels;

namespace PollCreator_Tests
{
	class PollRendererControllerTests
	{
		private PollRenderer _pollRendererController;
		private IPollRepository _pollRepository;
		private IPollOptionRepository _pollOptionRepository;
		private int primaryKey;

		[SetUp]
		public void Setup()
		{
			primaryKey = 123;
			_pollRepository = Substitute.For<IPollRepository>();
			_pollOptionRepository = Substitute.For<IPollOptionRepository>();
			_pollRendererController = new PollRenderer(_pollRepository,_pollOptionRepository);
			_pollRepository.Select(Arg.Any<string>()).Returns(new PollRenderViewModel());
			_pollRepository.SelectPollPk(Arg.Any<string>()).Returns(primaryKey);
			_pollOptionRepository.Select(Arg.Any<int>()).Returns(new List<PollOptionDTO>());
		}

		[Test]
		public async Task Vote_ReturnsCorrectView()
		{
			var viewResult = await _pollRendererController.Vote("pollId") as ViewResult;

			Assert.AreEqual("Vote", viewResult.ViewName);
		}

		[Test]
		public async Task Vote_PollRepositoryReceivedCalls()
		{
			string pollId = "someId";
			await _pollRendererController.Vote(pollId);

			await _pollRepository.Received().Select(Arg.Any<string>());
			await _pollRepository.Received().SelectPollPk(pollId);
			await _pollOptionRepository.Received().Select(primaryKey);
		}
	}
}
