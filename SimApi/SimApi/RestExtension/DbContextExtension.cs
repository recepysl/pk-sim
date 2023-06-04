using Microsoft.EntityFrameworkCore;
using SimApi.Data.Context;

namespace SimApi.Service.RestExtension;

public static class DbContextExtension
{
    public static void AddDbContextExtension(this IServiceCollection services, IConfiguration Configuration)
    {
        var dbType = Configuration.GetConnectionString("DbType");
        if (dbType == "SQL")
        {
            var dbConfig = Configuration.GetConnectionString("MsSqlConnection");
            services.AddDbContext<SimEfDbContext>(opts =>
            opts.UseSqlServer(dbConfig));
        }
        else if (dbType== "PostgreSql")
        {
            var dbConfig = Configuration.GetConnectionString("PostgreSqlConnection");
            services.AddDbContext<SimEfDbContext>(opts =>
              opts.UseNpgsql(dbConfig));
        }

        services.AddScoped<SimDapperDbContext>();
    }
}
