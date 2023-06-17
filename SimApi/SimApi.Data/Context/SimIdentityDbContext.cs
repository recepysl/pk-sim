using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SimApi.Data;

public class SimIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public SimIdentityDbContext(DbContextOptions<SimIdentityDbContext> options) : base(options)
    {

    }

}
