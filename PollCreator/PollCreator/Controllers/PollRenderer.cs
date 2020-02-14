using Microsoft.AspNetCore.Mvc;

namespace PollCreator.Controllers
{
	[Route("~/p/")]
	public class PollRenderer : Controller
	{
		[Route("{pollId}")]
		public IActionResult Vote([FromRoute]string pollId)
		{
			return View("Vote");
		}
	}
}
