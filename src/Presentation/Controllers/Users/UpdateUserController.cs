using Application.Actions.Users;
using Domain.DTOs.Users.Request;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Users
{
    [ApiController]
    [Route("/users")]
    public class UpdateUserController : Controller
    {
        private readonly UpdateUserAction _action;

        public UpdateUserController(UpdateUserAction action)
        {
            _action = action;
        }

        [HttpPut("{id}")]
        public async Task<IResult> Handle(string id, [FromBody] UpdateUserRequest request)
        {
            var result = await _action.Execute(id, request);

            return result.IsSucess ?
                Results.Ok(result.Data) :
                Results.Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad Request",
                    extensions: new Dictionary<string, object?>()
                    {
                        {"errors", result.Errors }
                    });
        }
    }
}
