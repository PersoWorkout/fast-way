using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace Presentation.FunctionalTests
{
    public class FunctionalWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer =
            new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .WithDatabase("fast-way-functional-test")
                .WithUsername("user-test")
                .WithPassword("integration")
                .Build();
        public Task InitializeAsync()
        {
            return _dbContainer.StartAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(x =>
                x.ServiceType == typeof(
                    DbContextOptions<ApplicationDbContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(_dbContainer.GetConnectionString()));

                var memoryDescriptor = services.SingleOrDefault(x =>
                    x.ServiceType == typeof(
                        DbContextOptions<AuthDbContext>));

                if (memoryDescriptor is not null)
                {
                    services.Remove(memoryDescriptor);
                }

                services.AddDbContext<AuthDbContext>(options =>
                    options.UseInMemoryDatabase("FunctionalAuthDbTest"));
            });
        }

        Task IAsyncLifetime.DisposeAsync()
        {
            return _dbContainer.StopAsync();
        }
    }
}
