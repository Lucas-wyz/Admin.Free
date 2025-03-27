using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Free.Models
{
	/// <summary>
	/// 
	/// </summary>
	[Table("questionoptions")]
	public class QuestionOptions : ModelBase
	{
		public string? QuestionID { get; set; }
		public string? option_text { get; set; }
		public string? option_value { get; set; }
		public bool? correct { get; set; }
	 
	}
}
