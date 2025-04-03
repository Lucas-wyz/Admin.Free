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
	public partial class QuestionsController(AppDbContext dbc, ILogger<QuestionsController> logger) : ControllerBase
	{

		/// <summary>
		/// 添加
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
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


		/// <summary>
		/// 删除
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




		/// <summary>
		/// 获取列表
		/// </summary>
		/// <param name="queryParameters"></param>
		/// <returns></returns>
		[HttpGet]
		public ResultObjet<List<QuestionsView>> Get([FromQuery] QueryParameters queryParameters)
		{
			var configMap = new MapperConfiguration(cfg =>
			cfg.CreateMap<Questions, QuestionsView>()
			.ForMember(x => x.options, o => o.MapFrom(s => dbc.QuestionOptions.Where(y => y.QuestionID == s.ID).ToList()))
			.ForMember(x => x.correct_answer, o => o.MapFrom(s => dbc.QuestionOptions.Where(y => y.QuestionID == s.ID).Where(y => y.correct == true).Select(x => x.option_text).ToList()))
			);

			var list = dbc.Questions.Page(queryParameters.Page, queryParameters.Size).ToList();

			var listView = configMap.CreateMapper().Map<List<Questions>, List<QuestionsView>>(list);

			return this.OKResult(listView);

		}

		/// <summary>
		/// 获取单个
		/// </summary>
		/// <param name="queryParameters"></param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public ResultObjet<QuestionsView> Get([FromRoute] string id, [FromQuery] QueryParameters queryParameters)
		{
			var configMap = new MapperConfiguration(cfg =>
			cfg.CreateMap<Questions, QuestionsView>()
			.ForMember(x => x.options, o => o.MapFrom(s => dbc.QuestionOptions.Where(y => y.QuestionID == s.ID).ToList()))
			.ForMember(x => x.correct_answer, o => o.MapFrom(s => dbc.QuestionOptions.Where(y => y.QuestionID == s.ID).Where(y => y.correct == true).Select(x => x.option_text).ToList()))
			);

			var query = dbc.Questions.Where(x => x.ID == id).First();

			var listView = configMap.CreateMapper().Map<Questions, QuestionsView>(query);

			return this.OKResult(listView);

		}




		/// <summary>
		/// 编辑
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




	public partial class QuestionsController : ControllerBase

	{


		/// <summary>
		/// 随机获取题目
		/// </summary>
		/// <param name="queryParameters"></param>
		/// <returns></returns>
		[HttpGet("GetRandom")]
		public ResultObjet<QuestionsView> GetRandom([FromQuery] QueryParameters queryParameters)
		{
			var configMap = new MapperConfiguration(cfg =>
			cfg.CreateMap<Questions, QuestionsView>().ForMember(x => x.options, o => o.MapFrom(s => dbc.QuestionOptions.Where(y => y.QuestionID == s.ID).ToList())));
			var random = Random.Shared.Next(0, dbc.Questions.Count());
			var list = dbc.Questions.Skip(random).First();

			var listView = configMap.CreateMapper().Map<Questions, QuestionsView>(list);

			return this.OKResult(listView);

		}

		/// <summary>
		/// 验证
		/// </summary>
		/// <param name="id"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		[HttpPost("verify/{id}")]
		public ResultObjet Verify([FromRoute] string id, [FromBody] QuestionsView obj)
		{

			var configMap = new MapperConfiguration(cfg =>
			cfg.CreateMap<Questions, QuestionsView>()
			.ForMember(x => x.options, o => o.MapFrom(s => dbc.QuestionOptions.Where(y => y.QuestionID == s.ID).ToList()))
			.ForMember(x => x.correct_answer, o => o.MapFrom(s => dbc.QuestionOptions.Where(y => y.QuestionID == s.ID).Where(y => y.correct == true).Select(x => x.option_text).ToList()))
			);

			var query = dbc.Questions.Where(x => x.ID == id).First();

			var listView = configMap.CreateMapper().Map<Questions, QuestionsView>(query);
			 

			var _answer = listView.options.Where(x => x.correct == true).ToList();
			var _input = obj.options.Where(x => x.correct == true).ToList();

			var _tag = false;
			if (_answer.Count == _input.Count)
			{
				var _result = _input.Where(x => _answer.Where(y => y.ID == x.ID).Any()).ToList();
				if (_input.Count == _result.Count)
				{
					_tag = true;
				}
			}

			///记录结果
			dbc.QuestionHistory.Add(new QuestionHistory()
			{
				IsDeleted = false,
				QuestionID = obj.ID,
				is_correct = _tag,
				submit_time = new DateTime(),
				UserID = "",
				UserName = "",
			});
			dbc.SaveChanges();

			return this.OKResult(_tag);
		}

	}
}
