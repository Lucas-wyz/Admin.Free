using Microsoft.AspNetCore.Mvc;
using Admin.Free.Infra;
using Admin.Free.Extensions;
using Admin.Free.Models;

namespace Admin.Free.Controllers
{
	/// <summary>
	/// 答题历史记录
	/// </summary>
	/// <param name="dbc"></param>
	/// <param name="logger"></param>
	[Route("api/[controller]")]
	[ApiController]
	public class QuestionHistoryController(AppDbContext dbc, ILogger<QuestionHistoryController> logger) : ControllerBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="queryParameters"></param>
		/// <returns></returns>
		[HttpGet]
		public ResultObjet<List<QuestionHistory>> Get([FromQuery] QueryParameters queryParameters)
		{
			var query = dbc.QuestionHistory.AsQueryable();

			var list = query.Page(queryParameters.Page, queryParameters.Size).ToList();
			return this.OKResult(list);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		[HttpDelete("{ID}")]
		public ResultObjet Del([FromRoute] string ID)
		{
			var query = dbc.QuestionHistory.Find(ID);
			dbc.QuestionHistory.Remove(query);
			dbc.SaveChanges();
			return this.OKResult();
		}


	}




}
