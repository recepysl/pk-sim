using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(TechaApiIdentityMvc.Areas.Identity.IdentityHostingStartup))]
namespace TechaApiIdentityMvc.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}