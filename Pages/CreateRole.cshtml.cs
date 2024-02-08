using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Code1stUsersRoles.Models;

namespace Code1stUsersRoles.Pages;

public class CreateRoleModel : PageModel
{
    private readonly RoleManager<CustomRole> _roleManager;

    public CreateRoleModel(RoleManager<CustomRole> roleManager)
    {
        _roleManager = roleManager;
    }

    [BindProperty]
    public CustomRole? Role { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await _roleManager.CreateAsync(new CustomRole { Name = Role!.Name, Description = Role.Description, CreatedDate = Role.CreatedDate });

        if (result.Succeeded)
        {
            return RedirectToPage("./Role"); // 성공 시 역할 목록 페이지로 리디렉션
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return Page(); // 실패 시 현재 페이지에 머무름
    }
}
