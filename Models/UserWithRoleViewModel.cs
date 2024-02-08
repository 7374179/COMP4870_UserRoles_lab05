using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Code1stUsersRoles.Models
{
    public class UserWithRoleViewModel
    {
        public CustomUser? User { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();

    }
}