using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SimApi.Base;
using SimApi.Data;
using SimApi.Rest;
using SimApi.Rest.Middleware;

namespace SimApi.Identity;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public static JwtConfig JwtConfig { get; private set; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        JwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
        services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

        services.AddControllersWithViews(options =>
        options.CacheProfiles.Add(ResponseCasheType.Minute45, new CacheProfile
        {
            Duration = 45 * 60,
            NoStore = false,
            Location = ResponseCacheLocation.Any
        }));

        services.AddResponseCompression();     
        services.AddCustomSwaggerExtension();
        services.AddJwtExtension(Configuration);


        var dbtype = Configuration.GetConnectionString("DbType");
        if (dbtype == "SQL")
        {
            var dbConfig = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SimIdentityDbContext>(options => options
               .UseSqlServer(dbConfig)
               );
        }
        else if (dbtype == "PostgreSQL")
        {
            var dbConfig = Configuration.GetConnectionString("PostgreSqlConnection");
            services.AddDbContext<SimIdentityDbContext>(options => options
               .UseNpgsql(dbConfig)
               );
        }

        // identity
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<SimIdentityDbContext>();


        // identity options
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredUniqueChars = 1;
        });


    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.DefaultModelsExpandDepth(-1);
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sim Company");
            c.DocumentTitle = "SimApi Company";
        });

        //DI
        app.AddExceptionHandler();
        app.AddDIExtension();

        app.UseMiddleware<HeartBeatMiddleware>();
        app.UseMiddleware<ErrorHandlerMiddleware>();
        Action<RequestProfilerModel> requestResponseHandler = requestProfilerModel =>
        {
            Log.Information("-------------Request-Begin------------");
            Log.Information(requestProfilerModel.Request);
            Log.Information(Environment.NewLine);
            Log.Information(requestProfilerModel.Response);
            Log.Information("-------------Request-End------------");
        };
        app.UseMiddleware<RequestLoggingMiddleware>(requestResponseHandler);

        app.UseHttpsRedirection();

        // add auth 
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
