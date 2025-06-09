using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Free.Models
{
	/// <summary>
	/// 题目
	/// </summary>
	[Table("questions")]
	public class Questions : ModelBase
	{
		/// <summary>
		/// 标题
		/// </summary>
		public string? question_title { get; set; }
		/// <summary>
		/// 解释
		/// </summary>
		public string? explanation_text { get; set; }
		/// <summary>
		/// 类型
		/// </summary>
		public string? question_type { get; set; }
	 
        /// <summary>
        /// 类别
        /// </summary>
        public string? category_name { get; set; }
	 
	
	}
}
