using Microsoft.AspNetCore.Mvc;
using PollCreator.Interfaces.Services;

namespace PollCreator.Controllers
{
	[Route("~/p/")]
	public class PollBuilderController : Controller
    {
	    private readonly ITokenService _tokenService;

	    public PollBuilderController(ITokenService tokenService)
	    {
		    _tokenService = tokenService;
	    }

	    [Route("create")]
		[HttpGet]
	    public RedirectToActionResult Create()
	    {
		    return RedirectToAction("Edit",new
		    {
			    pollId = _tokenService.GetRandomToken(6),
			    tokenId = _tokenService.GetRandomToken(32)
		    });

	    }

		[Route("{pollId}/edit/{tokenId}")]
		[HttpGet]
	    public IActionResult Edit(string pollId, string tokenId)
	    {
		    return View();
	    }
    }
}