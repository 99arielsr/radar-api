using Microsoft.AspNetCore.Mvc;
using radar_api.ModelViews;

namespace radar_api.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
	[HttpGet]
	public ActionResult Index()
	{
		return StatusCode(200, new Home
		{
			Message = "Bem vindo a minha API"
		});
	}
}
