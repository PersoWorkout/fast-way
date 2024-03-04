using Presentation.Presenters.Authorization;
using Presentation.Presenters.Users;

namespace Presentation
{
    public static class PresentationServiceInstaller
    {
        public static IServiceCollection InstallPresentationServices(this IServiceCollection services)
        {
            return services
                .InstallAuthPresenters()
                .InstallUserPresenters();
        }

        private static IServiceCollection InstallAuthPresenters(this IServiceCollection services)
        {
            services.AddScoped<LoginPresenter>();
            services.AddScoped<RegisterPresenter>();
            services.AddScoped<MePresenter>();

            return services;
        }

        private static IServiceCollection InstallUserPresenters(this IServiceCollection services)
        {
            services.AddScoped<GetUserByIdPresenter>();
            services.AddScoped<GetUsersPresenter>();
            services.AddScoped<CreateUserPresenter>();

            return services;
        }
    }
}
