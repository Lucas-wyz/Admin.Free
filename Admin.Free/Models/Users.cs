using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Free.Models
{
	/// <summary>
	/// 
	/// </summary>
	[Table("users")]
	public class Users : ModelBase
	{
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public string? Address { get; set; }
	}
}
