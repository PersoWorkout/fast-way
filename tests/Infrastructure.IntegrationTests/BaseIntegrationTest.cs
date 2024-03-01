using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IntegrationTests
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationWebApplicationFactory>
    {
        protected readonly IServiceScope _scope;
        protected readonly ApplicationDbContext _dbContext;
        public BaseIntegrationTest(IntegrationWebApplicationFactory factory)
        {
            _scope = factory.Services.CreateScope();
            _dbContext = _scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
        }
    }
}
