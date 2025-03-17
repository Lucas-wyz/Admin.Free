using Admin.Free.Extensions;
using Admin.Free.Infra;
using Admin.Free.Models;
using Microsoft.AspNetCore.Mvc;

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
		public ResultObjet<List<Roles>> Get([FromQuery] QueryParameters queryParameters)
		{
			var list = dbc.Roles.Page(queryParameters.Page, queryParameters.Size).ToList();
			return this.OKResult(list);
		}


		[HttpDelete("{ID}")]
		public ResultObjet Del([FromRoute] string ID)
		{
			var query = dbc.Roles.Find(ID);
			dbc.Roles.Remove(query);
			dbc.SaveChanges();
			return this.OKResult();
		}

	}
}
