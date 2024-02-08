using Code1stUsersRoles.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Code1stUsersRoles.Data;

public class ApplicationDbContext : IdentityDbContext<CustomUser, CustomRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder) {
    base.OnModelCreating(builder);

    // Use seed method here
    // builder.Seed();
    // 만약 Migrations 폴더를 삭제하고 다시 만들려면 builder.Seed()를 주석처리 해제하고 해야됨.
}

}

