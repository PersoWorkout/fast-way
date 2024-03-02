using Application.Actions.Users;
using Domain.DTOs.Users.Request;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Users
{
    [ApiController]
    [Route("/users")]
    public class CreateUserController : Controller
    {
        private readonly CreateUserAction _action;

        public CreateUserController(CreateUserAction action)
        {
            _action = action;
        }

        [HttpPost]
        public async Task<IResult> Handle([FromBody] CreateUserRequest request)
        {
            var result = await _action.Execute(request);

            return result.IsSucess ?
                Results.Ok(result.Data) :
                Results.Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Invalid payload",
                    extensions: new Dictionary<string, object?>()
                    {
                        {"errors", result.Errors }
                    });
        }
    }
}
