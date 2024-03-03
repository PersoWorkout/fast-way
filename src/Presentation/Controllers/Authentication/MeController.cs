using Application.Actions.Users;
using Microsoft.AspNetCore.Mvc;
using Presentation.Authentication.Attributes;

namespace Presentation.Controllers.Authentication
{
    [ApiController]
    [Route("auth/me")]
    public class MeController : Controller
    {
        private readonly GetUserByIdAction _action;
        
        public MeController(GetUserByIdAction action)
        {
            _action = action;
        }

        [Authenticated]
        [HttpGet]
        public async Task<IResult> Handle()
        {
            var userId = HttpContext
                .Items["userId"]!
                .ToString();

            var result = await _action.Execute(userId!);

            return result.IsSucess ? 
                Results.Ok(result.Data) : 
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
