using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp;

public class ApplicationDbContecxt : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContecxt(DbContextOptions<ApplicationDbContecxt> options) : base(options)
    {

    }
}
