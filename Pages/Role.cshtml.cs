using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Code1stUsersRoles.Models;
using Microsoft.EntityFrameworkCore;

namespace Code1stUsersRoles.Pages;

public class RoleModel : PageModel
{
    private readonly RoleManager<CustomRole> _roleManager;

    

    [BindProperty]
    public CustomRole? Role { get; set; }
    public IList<CustomRole>? Roles { get; set; }

    public RoleModel(RoleManager<CustomRole> roleManager)
    {
        _roleManager = roleManager;
    }
    

    public async Task OnGetAsync()
    {
        Roles = await _roleManager.Roles.ToListAsync();
    }

public async Task<IActionResult> OnPostAsync()
{
    if (!ModelState.IsValid)
    {
        return Page();
    }

    if (Role != null && !string.IsNullOrWhiteSpace(Role.Name))
    {
        // Check if the role already exists
        var roleExists = await _roleManager.RoleExistsAsync(Role.Name);
        if (!roleExists)
        {
            // Create the new role
            var result = await _roleManager.CreateAsync(new CustomRole(Role.Name));
            if (result.Succeeded)
            {
                // Redirect to the index page once the role is successfully created
                return RedirectToPage("./Index");
            }
            else
            {
                // If there are errors, add them to the ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Role already exists.");
        }
    }

    // If we get here, something went wrong; redisplay the form
    return Page();
}

public async Task<IActionResult> OnPostDeleteAsync(string id)
{
    if (string.IsNullOrWhiteSpace(id))
    {
        return Page();
    }

    var role = await _roleManager.FindByIdAsync(id);
    if (role != null)
    {
        var result = await _roleManager.DeleteAsync(role);
        if (result.Succeeded)
        {
            // 성공적으로 삭제된 후의 처리. 예를 들어, 목록 페이지로 리디렉션.
            return RedirectToPage();
        }
        else
        {
            // 삭제 중에 오류가 발생한 경우, 에러를 ModelState에 추가
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }

    // 역할을 찾을 수 없거나, 다른 이유로 페이지를 다시 표시
    return Page();
}


    // Add methods for deleting and updating roles

}
