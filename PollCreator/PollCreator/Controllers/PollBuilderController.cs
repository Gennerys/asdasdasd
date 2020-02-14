using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PollCreator.Interfaces.Services;
using PollCreator.Models;
using PollCreator.ViewModels;

namespace PollCreator.Controllers
{
	[Route("~/p/")]
	public class PollBuilderController : Controller
    {
	    private readonly ITokenService _tokenService;
	    private readonly IConfiguration _configuration;

	    public PollBuilderController(ITokenService tokenService, IConfiguration configuration)
	    {
		    _tokenService = tokenService;
		    _configuration = configuration;
	    }

	    [Route("create")]
		[HttpGet]
	    public RedirectToActionResult Create()
	    {
		    return RedirectToAction("Edit",new
		    {
			    pollId = _tokenService.GetRandomToken(6),
			    editorId = _tokenService.GetRandomToken(32)
		    });

	    }

		[Route("{pollId}/edit/{editorId}")]
		[HttpGet]
	    public IActionResult Edit(string pollId, string editorId)
	    {
		    PollBuilderViewModel pollBuilderViewModel = new PollBuilderViewModel()
		    {
			    PollId = pollId,
			    EditorToken = editorId
		    };

		    return View("Edit",pollBuilderViewModel);
	    }

	    [HttpPost]
		[Route("publish")]
		public IActionResult Publish([FromBody]PollPublishRequest pollPublishRequest)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"Insert Into Poll (Title, IsSingleOption, IsPublished, PollId, EditorToken) " +
				             $"Values ('{pollPublishRequest.Title}','{pollPublishRequest.IsSingleOption}','{pollPublishRequest.IsPublished}','{pollPublishRequest.PollId}','{pollPublishRequest.EditorToken}')";
				using (SqlCommand command = new SqlCommand(sql,connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					command.ExecuteNonQuery();
					connection.Close();
				}

				foreach (var option in pollPublishRequest.Options)
				{
					sql = $"Insert Into PollOption (SerialNumber, Value, Poll_PK) Values ('{option.SerialNumber}', '{option.Value}', '{pollPublishRequest.PollId}')";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						connection.Open();
						command.ExecuteNonQuery();
						connection.Close();
					}
				}
			}

			return Ok();
		}
    }
}