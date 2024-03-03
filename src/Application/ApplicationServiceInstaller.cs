using Application.Actions.Authorization;
using Application.Actions.Users;
using Application.Services.Authorization;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ApplicationServiceInstaller
    {
        public static IServiceCollection InstallApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(
                    Assembly.GetExecutingAssembly()));

            services.AddScoped<HashService>();

            services.InstallUserActions();

            services.InstallAuthActions();

            return services;
        }

        private static IServiceCollection InstallUserActions(this IServiceCollection services)
        {
            services.AddScoped<CreateUserAction>();
            services.AddScoped<DeleteUserAction>();
            services.AddScoped<GetUserByIdAction>();
            services.AddScoped<GetUsersAction>();
            services.AddScoped<UpdateUserAction>();

            return services;
        }

        private static IServiceCollection InstallAuthActions(this IServiceCollection services)
        {
            services.AddScoped<LoginAction>();
            services.AddScoped<RegisterAction>();
            services.AddScoped<LogoutAction>();

            return services;
        }
    }
}
