using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Admin.Free;
using Admin.Free.Infra;
using Admin.Free.Extensions;
using Admin.Free.Models;

namespace Admin.Free.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public partial class UsersController(AppDbContext dbc, ILogger<UsersController> logger) : ControllerBase
	{
		[HttpGet]
		public ResultObjet<List<Users>> Get([FromQuery] QueryParameters queryParameters, string? name)
		{
			var query = dbc.Users.AsQueryable();
			if (!string.IsNullOrWhiteSpace(name))
			{
				query = query.Where(x => x.Name.Contains(name));
			}
			var list = query.Page(queryParameters.Page, queryParameters.Size).ToList();
			return this.OKResult(list);
		}

		[HttpPost]
		public ResultObjet<Users> Post(Users users)
		{
			var query = dbc.Users.Add(users);
			dbc.SaveChanges();
			return this.OKResult(users);
		}

		[HttpPut("{ID}")]
		public ResultObjet<Users> Put([FromRoute] string ID, Users users)
		{
			var query = dbc.Users.Find(ID);
			query.Name = users.Name;
			query.Email = users.Email;
			query.Phone = users.Phone;
			query.Address = users.Address;
			dbc.SaveChanges();
			return this.OKResult(users);
		}
		[HttpDelete("{ID}")]
		public ResultObjet Del([FromRoute] string ID)
		{
			var query = dbc.Users.Find(ID);
			dbc.Users.Remove(query);
			dbc.SaveChanges();
			return this.OKResult();
		}
	}

	public partial class UsersController : ControllerBase
	{
		[HttpGet("EditPassword/{id}")]
		public ResultObjet<Accounts> EditPassword([FromRoute] string id)
		{
			var query = dbc.Accounts.Where(x => x.uid == id).FirstOrDefault();
			 
			return this.OKResult(query);
		}
		
		[HttpPost("EditPassword/{id}")]
		public ResultObjet EditPassword([FromBody] Accounts obj, [FromRoute] string id)
		{

			var query = dbc.Accounts.Where(x => x.uid == id).ToList();

			foreach (var item in query)
			{
				item.IsDeleted = true;
			}

			dbc.Accounts.Add(new Accounts { uid = id, account = obj.account, password = obj.password });
			dbc.SaveChanges();

			return this.OKResult<bool>(true);
		}

	}


}
