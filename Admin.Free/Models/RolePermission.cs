using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Free.Models
{
    /// <summary>
    /// 角色-权限
    /// </summary>
    [Table("rolepermission")]
    public class RolePermission : ModelBase
    {

        public string? RoleID { get; set; }
        public string PermissionID { get; set; }

    }
}
