using MongoDB.Driver.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Free.Models
{
	/// <summary>
	/// 答题历史记录
	/// </summary>
	[Table("questionhistory")]
	public class QuestionHistory : ModelBase
	{

		/// <summary>
		/// 题目
		/// </summary>
		public int QuestionID { get; set; }
		/// <summary>
		/// 用户
		/// </summary>
		public string UserID { get; set; }
		/// <summary>
		/// 用户
		/// </summary>
		public string UserName { get; set; }
		/// <summary>
		/// 提交时间
		/// </summary>
		public DateTime? submit_time { get; set; }
		/// <summary>
		/// 答题结果
		/// </summary>
		public bool? is_correct { get; set; }


	}
}
