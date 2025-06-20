using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Free.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Table("examsquertion")]
    public class ExamsQuertion : ModelBase
    {

        public string ExamsID { get; set; }
        public string QuertionID { get; set; }

    }
}
