using Application.Actions.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Authentication.Attributes;

namespace Presentation.Controllers.Authentication
{
    [ApiController]
    [Route("/users/logout")]
    public class LogoutController : Controller
    {
        private readonly LogoutAction _action;

        public LogoutController(LogoutAction action)
        {
            _action = action;
        }

        [Authenticated]
        [HttpDelete]
        public async Task<IResult> Handler()
        {
            var token = HttpContext.Request
                .Headers
                .Authorization
                .ToString();

            var result = await _action.Execute(token);

            return result.IsSucess ? 
                Results.NoContent() :
                Results.Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "BadRequest",
                    extensions: new Dictionary<string, object?>()
                    {
                        {"errors", result.Errors }
                    });
        }
    }
}
