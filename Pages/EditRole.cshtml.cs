using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Code1stUsersRoles.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Code1stUsersRoles.Pages
{
    public class EditRoleModel : PageModel
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<CustomRole> _roleManager;

        public EditRoleModel(UserManager<CustomUser> userManager, RoleManager<CustomRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty(SupportsGet = true)]
        public string? UserId { get; set; }

        public new CustomUser? User { get; set; }

        [BindProperty]
        public IList<string>? UserRoles { get; set; }

        public IList<string>? AvailableRoles { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return NotFound();
            }

            User = await _userManager!.FindByIdAsync(UserId);
            if (User == null)
            {
                return NotFound();
            }

            // Load the current user's roles and all available roles
            UserRoles = await _userManager.GetRolesAsync(User);
            // AvailableRoles = _roleManager.Roles.Select(r => r.Name).ToList();
            AvailableRoles = [.. _roleManager.Roles.Select(r => r.Name)];

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return NotFound();
            }

            var user = await _userManager!.FindByIdAsync(UserId);
            if (user == null)
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!result.Succeeded)
            {
                // Error handling
                return Page();
            }

            result = await _userManager.AddToRolesAsync(user, UserRoles!);

            if (!result.Succeeded)
            {
                // Error handling
                return Page();
            }

            return RedirectToPage("./User");
        }
    }
}
