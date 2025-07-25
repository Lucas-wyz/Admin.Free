﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Admin.Free;
using Admin.Free.Infra;
using Admin.Free.Extensions;
using Admin.Free.Infra;
using Admin.Free.Models;
using Admin.Free.View;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMapper.Execution;
using AutoMapper.Features;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Free.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public partial class UsersController(AppDbContext dbc, ILogger<UsersController> logger) : ControllerBase
	public partial class UsersController(AppDbContext dbc, ILogger<UsersController> logger, IMapper mapper, AutoMapper.IConfigurationProvider ConfigurationProvider) : ControllerBase
	{
		[HttpGet]
		public ResultObjet<List<UsersView>> Get([FromQuery] QueryParameters queryParameters, string? name)
		{

			var configMap = new MapperConfiguration(cfg =>
            {
			cfg.CreateMap<Users, UsersView>()
                .ForMember(x => x.RoleList, o => o.MapFrom(s => dbc.UserRole.Where(y => y.UserID == s.ID).Select(y => y.RoleID).ToList()));
            });

			var query = dbc.Users.AsQueryable();
			if (!string.IsNullOrWhiteSpace(name))
			{
				query = query.Where(x => x.Name.Contains(name));
			}

            var list = query.Page(queryParameters.Page, queryParameters.Size).ProjectTo<UsersView>(configMap).ToList();

			return this.OKResult<List<UsersView>>(list);
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
            var query = dbc.Users.Where(x => x.ID == ID).ToList();

            foreach (var item in query)
            {
                item.IsDeleted = true;

            }

            var queryRoles = dbc.UserRole.Where(x => x.UserID == ID).ToList();

            foreach (var item in queryRoles)
            {
                item.IsDeleted = true;

            }

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

		[HttpPost("EditRole/{id}")]
		public ResultObjet EditRole([FromRoute] string id, [FromBody] List<string> obj)
		{

			var query = dbc.UserRole.Where(x => x.UserID == id).ToList();

			foreach (var item in query)
			{
				item.IsDeleted = true;
			}
			var ff = obj.Select(x => new UserRole()
			{

				UserID = id,
				RoleID = x,
			}).ToList();
			dbc.UserRole.AddRange(ff);
			dbc.SaveChanges();

			return this.OKResult<bool>(true);
		}

	}


}
