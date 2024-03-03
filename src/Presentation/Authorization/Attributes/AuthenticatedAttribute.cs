using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Authentication.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthenticatedAttribute : Attribute, IAsyncAuthorizationFilter
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

            context.HttpContext.Items["userId"] = session.UserId.ToString();
        }
    }
}
