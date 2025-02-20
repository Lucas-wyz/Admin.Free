using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Admin.Free;
using Admin.Free.Extensions;
using Admin.Free.Models;

namespace Admin.Free.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController(AppDbContext dbc, ILogger<UsersController> logger) : ControllerBase
	{
		[HttpGet]
		public ResultObjet<List<Users>> Get(string? name)
		{
			var query = dbc.Users.AsQueryable();
			if (!string.IsNullOrWhiteSpace(name))
			{
				query = query.Where(x => x.Name.Contains(name));
			}
			var list = query.ToList();
			return this.OKResult(list);
		}
	}
}
