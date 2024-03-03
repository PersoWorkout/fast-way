using Microsoft.Extensions.DependencyInjection;

namespace Presentation.FunctionalTests
{
    public abstract class BaseFunctionalTest(
        FunctionalWebApplicationFactory factory) :
        IClassFixture<FunctionalWebApplicationFactory>
    {
        protected  readonly IServiceScope _scope =
            factory.Services.CreateScope();

        protected readonly HttpClient _client =
            factory.CreateClient();
    }
}
