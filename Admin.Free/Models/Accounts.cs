using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Free.Models
{
	/// <summary>
	/// 
	/// </summary>
	[Table("Accounts")]
	public class Accounts : ModelBase
	{
		public string uid { get; set; }
		public string? Name { get; set; }

		public string? password { get; set; }

	}
}
