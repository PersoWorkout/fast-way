using Application.Services.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.FunctionalTests
{
    public abstract class BaseFunctionalTest:
        IClassFixture<FunctionalWebApplicationFactory>
    {
        protected  readonly IServiceScope _scope;
        protected readonly HttpClient _client;
        protected readonly HashService _hashService;

        public BaseFunctionalTest(FunctionalWebApplicationFactory factory)
        {
            _scope = factory.Services.CreateScope();
            _client = factory.CreateClient();
            _hashService = _scope.ServiceProvider.GetRequiredService<HashService>();
        }
    }
}
