using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PollCreator.Implementation.Services;
using PollCreator.Interfaces.Services;
using PollCreator.Models;
using PollCreator.ViewModels;

namespace PollCreator.Controllers
{
	[Route("~/p/")]
	public class PollBuilderController : Controller
	{
		private readonly ITokenService _tokenService;
		private readonly IPollRepository _pollRepository;
		private readonly IPollOptionRepository _pollOptionRepository;

		public PollBuilderController(ITokenService tokenService, IPollRepository pollRepository, IPollOptionRepository pollOptionRepository)
		{
			_tokenService = tokenService;
			_pollRepository = pollRepository;
			_pollOptionRepository = pollOptionRepository;
		}

		[Route("create")]
		[HttpGet]
		public async Task<RedirectToActionResult> Create()
		{
			string pollToken = _tokenService.GetRandomToken(6);
			string editorToken = _tokenService.GetRandomToken(32);

			await _pollRepository.Insert(new PollPublishRequest
			{
				Title = null,
				IsSingleOption = null,
				PollId = pollToken,
				EditorToken = editorToken
			});
			
			return RedirectToAction("Edit",new
			{
				pollId = pollToken,
				editorId = editorToken
			});

		}

		[Route("{pollId}/edit/{editorId}")]
		[HttpGet]
		public async Task<IActionResult> Edit(string pollId, string editorId)
		{
			var pollRenderViewModel = await _pollRepository.Select(pollId);
			PollBuilderViewModel pollBuilderViewModel;

			if (pollRenderViewModel == null)
			{
				pollBuilderViewModel = new PollBuilderViewModel()
				{
					PollId = pollId,
					EditorToken = editorId
				};

			}
			else
			{
				int primaryKey = await _pollRepository.SelectPollPk(pollId);
				pollBuilderViewModel = new PollBuilderViewModel()
				{
					Title = pollRenderViewModel.Title,
					IsSingleOption = pollRenderViewModel.IsSingleOption,
					IsPublished = pollRenderViewModel.IsPublished,
					PollId = pollId,
					EditorToken = editorId,
					Options = await _pollOptionRepository.Select(primaryKey)

				};
			}
			return View("Edit", pollBuilderViewModel);
		}

		[HttpPost]
		[Route("publish")]
		public async Task<IActionResult> Publish([FromBody]PollPublishRequest pollPublishRequest)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			//var pollInDb = await _pollRepository.Select(pollPublishRequest.PollId);

			//if (!pollInDb.IsPublished)
			//{
				await _pollRepository.Update(pollPublishRequest);
				int pollPk = await _pollRepository.SelectPollPk(pollPublishRequest.PollId);
				await _pollOptionRepository.Insert(pollPublishRequest.Options, pollPk);
			//}
			//else
			//{
				//return BadRequest();
			//}
		//

			return Ok();
		}
	}
}