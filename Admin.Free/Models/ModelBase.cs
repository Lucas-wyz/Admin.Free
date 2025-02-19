using Admin.Free.Infra;
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
		/// 租户
		/// </summary>
		public required string Tenant { get; set; } = "";


	}
}
