using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Code1stUsersRoles.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Code1stUsersRoles.Pages
{
    public class UserModel : PageModel
    {
        private readonly UserManager<CustomUser> _userManager;

        public IList<UserWithRoleViewModel> UsersWithRoles { get; set; } = new List<UserWithRoleViewModel>();

        public UserModel(UserManager<CustomUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                UsersWithRoles.Add(new UserWithRoleViewModel
                {
                    User = user,
                    Roles = roles
                });
            }
        }
    }
}
