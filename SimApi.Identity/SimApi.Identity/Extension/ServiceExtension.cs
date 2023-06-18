using SimApi.Operation;

namespace SimApi.Identity.Extension;

public static class ServiceExtension
{
    public static void AddServiceExtension(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
    }
}
