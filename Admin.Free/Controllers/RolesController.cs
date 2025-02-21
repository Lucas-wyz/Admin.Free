using Microsoft.AspNetCore.Mvc;
using Admin.Free.Extensions;
using Admin.Free.Models;

namespace Admin.Free.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="dbc"></param>
	/// <param name="logger"></param>
	[Route("api/[controller]")]
	[ApiController]
	public class RolesController(AppDbContext dbc, ILogger<UsersController> logger) : ControllerBase
	{
		[HttpGet]
		public ResultObjet<List<Roles>> Get()
		{
			var list = dbc.Roles.Take(10).ToList();
			return this.OKResult(list);
		}
	}
}
