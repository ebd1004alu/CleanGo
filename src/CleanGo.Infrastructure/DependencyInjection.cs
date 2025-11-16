using CleanGo.Application.Interfaces.Security;
using CleanGo.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace CleanGo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Register infrastructure services.
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            return services;
        }
    }
}
