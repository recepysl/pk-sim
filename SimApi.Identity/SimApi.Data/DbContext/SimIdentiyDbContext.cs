using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimApi.Data.Domain;

namespace SimApi.Data.DbContext;

public class SimIdentiyDbContext : IdentityDbContext<ApplicationUser>
{
    public SimIdentiyDbContext(DbContextOptions<SimIdentiyDbContext> options) : base(options)
    {

    }

    public DbSet<Account> Account { get; set; }
    public DbSet<Transaction> Transaction { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());

        base.OnModelCreating(modelBuilder);
    }


}
