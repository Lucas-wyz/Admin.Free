using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Free.Models
{
    /// <summary>
    /// 权限
    /// </summary>
    [Table("permissions")]
    public class Permissions : ModelBase
    {
        public string? Name { get; set; }
        public string? UrlStr { get; set; }
        public string? Remark { get; set; }
      
    }
}
