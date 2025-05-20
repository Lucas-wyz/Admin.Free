using Admin.Free.Models;

namespace Admin.Free.View
{
    public class UsersView : Users
    {
        public IEnumerable<string> RoleList { get; set; }
    }
}
