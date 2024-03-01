using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IntegrationTests
{
    public abstract class BaseIntegrationTest(
        IntegrationWebApplicationFactory factory) : 
        IClassFixture<IntegrationWebApplicationFactory>
    {
        protected readonly IServiceScope _scope = 
            factory.Services.CreateScope();
    }
}
