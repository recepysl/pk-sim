using Microsoft.Extensions.DependencyInjection;
using SimApi.Data.Repository;

namespace SimApi.Rest;

public static class RepositoryExtension
{
    public static void AddRepositoryExtension(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository,CategoryRepository>();
        services.AddScoped<IProductRepository,ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
