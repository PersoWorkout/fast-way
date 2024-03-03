using Application.Actions.Authorization;
using Domain.DTOs.Authorization.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Authentication
{
    [ApiController]
    [Route("/auth/login")]
    public class LoginController : Controller
    {
        private readonly LoginAction _action;

        public LoginController(LoginAction action)
        {
            _action = action;
        }

        [HttpPost]
        public async Task<IResult> Handle([FromBody] LoginRequest request)
        {
            var result = await _action.Execute(request);

            return result.IsSucess ?
                Results.Ok(result.Data) :
                Results.Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Bad Request",
                    extensions: new Dictionary<string, object?>()
                    {
                        {"errors", result.Errors }
                    });
        }
    }
}
