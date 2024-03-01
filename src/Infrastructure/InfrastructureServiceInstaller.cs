using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceInstaller
    {
        public static IServiceCollection InstallInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddDbContext<ApplicationDbContext>(option =>
                option.UseNpgsql(configuration
                    .GetConnectionString("DatabaseConnection")));

            services.AddDbContext<AuthDbContext>(option =>
                option.UseInMemoryDatabase("AuthDatabase"));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();

            return services;
        }
    }
}
