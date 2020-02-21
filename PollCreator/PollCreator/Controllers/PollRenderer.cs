using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PollCreator.Interfaces.Services;
using PollCreator.ViewModels;

namespace PollCreator.Controllers
{
	[Route("~/p/")]
	public class PollRenderer : Controller
	{
		private readonly IPollRepository _pollRepository;
		private readonly IPollOptionRepository _pollOptionRepository;
		public PollRenderer(IPollRepository pollRepository, IPollOptionRepository pollOptionRepository)
		{
			_pollRepository = pollRepository;
			_pollOptionRepository = pollOptionRepository;
		}

		[Route("{pollId}")]
		public async Task<IActionResult> Vote([FromRoute]string pollId)
		{
			PollRenderViewModel pollRenderViewModel = await _pollRepository.Select(pollId);
			int primaryKey = 0;
			primaryKey = await _pollRepository.SelectPollPk(pollId); 
			pollRenderViewModel.Options = await _pollOptionRepository.Select(primaryKey);
			
			return View("Vote", pollRenderViewModel);
		}
	}
}
