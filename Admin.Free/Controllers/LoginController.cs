using Admin.Free.Extensions;
using Admin.Free.Infra;
using Admin.Free.Models;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Free.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController(AppDbContext dbc, ILogger<LoginController> logger) : ControllerBase
	{

		[HttpPost]
		public ResultObjet<string> Post([FromBody] Users obj)
		{

			var user = dbc.Users.Where(x => x.Name == obj.Name && x.Phone == obj.Phone).First();

			return this.OKResult("token");
		}

	}



}
