using Microsoft.EntityFrameworkCore;

namespace SimApi.Data.Context;

public class SimEfDbContext : DbContext
{
    public SimEfDbContext(DbContextOptions<SimEfDbContext> options) : base(options)
    {

    }

    // dbset
    public DbSet<Product> Product { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<UserLog> UserLog { get; set; }
    public DbSet<Account> Account { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Transaction> Transaction { get; set; }
    public DbSet<TransactionView> TransactionView { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new UserLogConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionViewConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
