using Admin.Free.Models;

namespace Admin.Free.View
{
	/// <summary>
	/// 
	/// </summary>
	public class QuestionsView : Questions
	{
		public IEnumerable<QuestionOptions> options { get; set; }
        /// <summary>
        /// 正确答案
        /// </summary>

        /// <summary>
        /// 标签
        /// </summary>
        public new string[]? tags { get; set; }



        /// <summary>
        /// 是匿名用户
        /// </summary>
        public bool? AnonymousUsers { get; set; }

	}
}
