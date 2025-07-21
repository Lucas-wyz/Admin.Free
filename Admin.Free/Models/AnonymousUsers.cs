using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Free.Models
{

    /// <summary>
    /// 匿名用户
    /// </summary>
    [Table("anonymoususers")]
    public class AnonymousUsers : ModelBase
    {
        public string? Name { get; set; }

    }

}
