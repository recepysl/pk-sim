using Serilog;

namespace SimApi.Identity;

public class Program
{
    public static void Main(string[] args)
    {

        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
        Log.Information("Application is starting...");

        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .UseSerilog()
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });
}
