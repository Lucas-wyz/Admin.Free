using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Free.Models
{
    /// <summary>
    /// 试卷
    /// </summary>
    [Table("exams")]
    public class Exams : ModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Type { get; set; }

    }

}
