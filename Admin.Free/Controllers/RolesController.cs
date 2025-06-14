using Admin.Free.Extensions;
using Admin.Free.Infra;
using Admin.Free.Models;
using Admin.Free.View;
using AutoMapper;
using AutoMapper.QueryableExtensions;

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

		[HttpGet("list")]
		public ResultObjet<List<RolesView>> GetList([FromQuery] QueryParameters queryParameters)
		{
			var configMap = new MapperConfiguration(cfg => cfg.CreateMap<Roles, RolesView>());
			var list = dbc.Roles.ProjectTo<RolesView>(configMap).ToList();
			return this.OKResult(list);
		}

		[HttpPost]
		public ResultObjet Post([FromBody] Roles roles)
		{

			roles.ID = Guid.NewGuid().ToString("n");
			roles.IsDeleted = false;

			var query = dbc.Roles.Add(roles);
			dbc.SaveChanges();
			return this.OKResult();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="roles"></param>
		/// <returns></returns>
		[HttpPut("{id}")]
		public ResultObjet Put([FromRoute] string id, [FromBody] Roles roles)
		{
			var query = dbc.Roles.Find(id);

			query.Name = roles.Name;
			query.Permissions = roles.Permissions;
			query.Description = roles.Description;

			dbc.SaveChanges();
			return this.OKResult();
		}


		[HttpDelete("{ID}")]
		public ResultObjet Del([FromRoute] string ID)
		{
			var query = dbc.Roles.Where(x => x.ID == ID).ToList();

			foreach (var item in query)
			{
				item.IsDeleted = true;
			}

            var queryRoles = dbc.UserRole.Where(x => x.RoleID == ID).ToList();
            foreach (var item in queryRoles)
            {
                item.IsDeleted = true;
            }

			dbc.SaveChanges();
			return this.OKResult();
		}

	}
}
