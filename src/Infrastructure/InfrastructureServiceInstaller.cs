using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceInstaller
    {
        public static IServiceCollection InstallInfrastructureServices(
            this ServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
                option.UseNpgsql(configuration
                    .GetConnectionString("DatabaseConnection")));

            services.AddDbContext<AuthDbContext>(option =>
                option.UseInMemoryDatabase("AuthDatabase"));

            return services;
        }
    }
}
