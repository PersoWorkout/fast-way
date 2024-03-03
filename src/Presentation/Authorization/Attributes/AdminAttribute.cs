using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Authorization.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var token = context.HttpContext
                .Request
                .Headers
                .Authorization
                .ToString();

            var session = await context.HttpContext
                .RequestServices
                .GetRequiredService<IAuthorizationRepository>()
                .GetByToken(token);

            if (session is null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var user = await context.HttpContext
                .RequestServices
                .GetRequiredService<IUserRepository>()
                .GetById(session.UserId);

            if (user is null || user.Role != UserRoles.Administrateur)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            context.HttpContext.Items["userId"] = session.UserId.ToString();
        }
    }
}
