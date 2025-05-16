using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Free.Models
{
	/// <summary>
	/// 
	/// </summary>
	[Table("userrole")]
	public class UserRole : ModelBase
	{
		public string UserID { get; set; }
		public string RoleID { get; set; }
	
	}

}
