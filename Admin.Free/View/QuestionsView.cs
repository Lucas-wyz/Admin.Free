using Admin.Free.Models;

namespace Admin.Free.View
{
	/// <summary>
	/// 
	/// </summary>
	public class QuestionsView : Questions
	{
		public IEnumerable<QuestionOptions> options { get; set; }

		public IEnumerable<string> correct_answer { get; set; }
        public new string[]? tags { get; set; }
	}
}
