using Microsoft.AspNetCore.Mvc;
using Admin.Free.Infra;
using Admin.Free.Extensions;
using Admin.Free.Models;
using Admin.Free;
using Admin.Free.View;
using AutoMapper;

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
        public ResultObjet<List<QuestionHistoryView>> Get([FromQuery] QueryParameters queryParameters)
		{
            var configMap = new MapperConfiguration(cfg =>
            cfg.CreateMap<QuestionHistory, QuestionHistoryView>()
            .ForMember(x => x.question_title, o => o.MapFrom(s => dbc.Questions.Where(y => y.ID == s.QuestionID).Select(y => y.question_title).FirstOrDefault()))
            );
			var query = dbc.QuestionHistory.AsQueryable();

            var list = query.OrderByDescending(x => x.CreatDate).Page(queryParameters.Page, queryParameters.Size).ToList().ProjectTo<QuestionHistoryView>(configMap).ToList();

            foreach (var item in list)
            {
                item.question_title = dbc.Questions.Where(x => x.ID == item.QuestionID).Select(x => x.question_title).FirstOrDefault();
            }

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
