using Admin.Free.Models;

namespace Admin.Free.View
{
	/// <summary>
	/// 
	/// </summary>
	public class QuestionsView : Questions
	{
		public IEnumerable<QuestionOptions> options { get; set; }
	}
}
