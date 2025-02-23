

using System.ComponentModel.DataAnnotations;

namespace Admin.Free.Models
{
	/// <summary>
	/// 实体类基础
	/// </summary>
	public class ModelBase
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Key]

		public string ID { get; set; } = "";

		/// <summary>
		/// 租户Id
		/// </summary>
		public required string TenantId { get; set; } = "";


	}
}
