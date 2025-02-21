using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Free.Models
{
	/// <summary>
	/// 
	/// </summary>
	[Table("roles")]
	public class Roles : ModelBase
	{
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Permissions { get; set; }

	}
}
