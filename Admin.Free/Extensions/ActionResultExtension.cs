using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace Admin.Free.Extensions
{
	/// <summary>
	/// 返回状态扩展
	/// </summary>
	public static class ActionResultExtension
	{
		public static ResultObjet OKResult(this ControllerBase controller, object? obj=null)
		{
			return new ResultObjet()
			{
				Code = 200,
				Data = obj,
				Message = "OK",
			};
		}
		public static ResultObjet<T> OKResult<T>(this ControllerBase controller, T obj) where T : class
		{
			return new ResultObjet<T>()
			{
				Code = 200,
				Data = obj,
				Message = "OK",
			};
		}
		public static object NotFoundResult(this ControllerBase controller, object obj)
		{
			return new ResultObjet()
			{
				Code = 404,
				Data = obj,
				Message = "NotFound",
			};
		}
		public static object BadRequestResult(this ControllerBase controller, object obj)
		{
			return new ResultObjet()
			{
				Code = 400,
				Data = obj,
				Message = "BadRequest",
			};
		}
		public static object ProblemResult(this ControllerBase controller, object obj)
		{
			return new ResultObjet()
			{
				Code = 500,
				Data = obj,
				Message = "Problem",
			};
		}
	}
}
