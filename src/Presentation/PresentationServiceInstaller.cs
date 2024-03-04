using Presentation.Presenters.Authorization;

namespace Presentation
{
    public static class PresentationServiceInstaller
    {
        public static IServiceCollection InstallPresentationServices(this IServiceCollection services)
        {
            services.AddScoped<LoginPresenter>();

            return services;
        }
    }
}
