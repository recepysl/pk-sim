using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimApi.Data.DbContext;
using SimApi.Data.Domain;
using SimApi.Data.Uow;

namespace SimApi.Identity.Extension;

public static class DbContextExtension
{
    public static void AddDbContextExtension(this IServiceCollection services, IConfiguration Configuration)
    {
        var dbType = Configuration.GetConnectionString("DbType");
        if (dbType == "SQL")
        {
            var dbConfig = Configuration.GetConnectionString("MsSqlConnection");
            services.AddDbContext<SimIdentiyDbContext>(opts =>
            opts.UseSqlServer(dbConfig));
        }
        else if (dbType== "PostgreSql")
        {
            var dbConfig = Configuration.GetConnectionString("PostgreSqlConnection");
            services.AddDbContext<SimIdentiyDbContext>(opts =>
              opts.UseNpgsql(dbConfig));
        }

        services.AddIdentity<ApplicationUser,IdentityRole>()
            .AddEntityFrameworkStores<SimIdentiyDbContext>();



        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredUniqueChars = 1;
            options.User.RequireUniqueEmail = true;
        });
    }
}
