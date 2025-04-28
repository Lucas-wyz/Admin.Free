using Admin.Free.Extensions;
using Admin.Free.Infra;
using Admin.Free.Models;
using Admin.Free.View;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Admin.Free.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController(AppDbContext dbc, ILogger<LoginController> logger) : ControllerBase
	{


		[HttpPost("Jwt")]
		public ResultObjet<LoginRes> PostJwt([FromBody] LoginReq obj)
		{

			var user = dbc.Accounts.Where(x => x.account == obj.account && x.password == obj.password).FirstOrDefault();

			if (user != null)
			{
				var jwtToken = JwtConfig.CreateToken(new Claim[] { new Claim("uid", user.uid), });

				var token = new LoginRes()
				{
					Authentication = true,
					Token = jwtToken,
				};
				return this.OKResult<LoginRes>(token);

			}
			else
			{
				var token = new LoginRes()
				{
					Authentication = false,
					Token = "",
				};
				return this.OKResult<LoginRes>(token);
			}

		}


		[HttpPost("JwtTest")]
		public ResultObjet<LoginRes> PostJwtTest([FromBody] object obj)
		{

			var jwtToken = JwtConfig.CreateToken(new Claim[] { });

			var token = new LoginRes()
			{
				Authentication = true,
				Token = jwtToken,
			};
			return this.OKResult<LoginRes>(token);

		}


	}



}
