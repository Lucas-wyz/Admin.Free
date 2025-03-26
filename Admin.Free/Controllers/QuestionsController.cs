using Admin.Free.Extensions;
using Admin.Free.Infra;
using Admin.Free.Models;
using Admin.Free.View;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Admin.Free.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuestionsController(AppDbContext dbc, ILogger<QuestionsController> logger) : ControllerBase
	{
		 
		


	 
	 
		[HttpPost]
		public ResultObjet Post([FromBody] QuestionsView obj)
		{
			dbc.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

			obj.ID = Guid.NewGuid().ToString("n");
			dbc.Questions.Add(obj);

			List<QuestionOptions> oplist = obj.options.ToList();

			foreach (var item in oplist)
			{
				item.ID = obj.ID = Guid.NewGuid().ToString("n");
				item.QuestionID = obj.ID;
			}

			dbc.QuestionOptions.AddRange(oplist);

			dbc.SaveChanges();
			return this.OKResult();
		}

		[HttpPost("verify/{id}")]
		public ResultObjet Verify([FromRoute]string id,[FromBody] QuestionsView obj)
		{
			return this.OKResult(true);
		}
		 


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[HttpDelete("{id}")]
		public ResultObjet Del([FromRoute] string id)
		{
			dbc.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

			var QuestionList = dbc.Questions.Where(x => x.ID == id).ToList();

			foreach (var item in QuestionList)
			{
				item.IsDeleted = true;
			}

			var QuestionOptionsList = dbc.QuestionOptions.Where(x => x.QuestionID == id).ToList();

			foreach (var item in QuestionOptionsList)
			{
				item.IsDeleted = true;
			}

			dbc.SaveChanges();
			return this.OKResult();
		}





		[HttpGet]
		public ResultObjet<List<QuestionsView>> Get([FromQuery] QueryParameters queryParameters)
		{
			var configMap = new MapperConfiguration(cfg =>
			cfg.CreateMap<Questions, QuestionsView>().ForMember(x => x.options, o => o.MapFrom(s => dbc.QuestionOptions.Where(y => y.QuestionID == s.ID).ToList())));
			 
			var list = dbc.Questions.Page(queryParameters.Page, queryParameters.Size).ToList();

			var listView = configMap.CreateMapper().Map<List<Questions>, List<QuestionsView>>(list);
			 
			return this.OKResult(listView);

		}

	

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		[HttpPut("{id}")]
		public ActionResult Put([FromRoute] string id, [FromBody] QuestionsView obj)
		{

			var questions = dbc.Questions.Find(id);


			questions.question_title = obj.question_title;
			questions.explanation_text = obj.explanation_text;
			questions.question_type = obj.question_type;

			var QuestionOptionsList = dbc.QuestionOptions.Where(x => x.QuestionID == id).ToList();

			if (QuestionOptionsList.Count > 0)
			{
				foreach (var item in QuestionOptionsList)
				{
					item.IsDeleted = true;
				}

			}
			List<QuestionOptions> list = new List<QuestionOptions>();
			foreach (var item in obj.options)
			{

				item.ID = Guid.NewGuid().ToString("n");
				item.QuestionID = id;
				item.IsDeleted = false;

				list.Add(item);

			}

			dbc.QuestionOptions.AddRange(list);


			dbc.SaveChanges();

			return Ok();

		}



	}
}
