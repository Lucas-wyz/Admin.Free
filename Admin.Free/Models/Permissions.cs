using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Free.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Table("permissions")]
    public class Permissions : ModelBase
    {
        public string? Name { get; set; }
      
    }
}
