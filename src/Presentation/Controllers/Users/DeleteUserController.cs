using Application.Actions.Users;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Users
{
    [ApiController]
    [Route("/users")]
    public class DeleteUserController : Controller
    {
        private readonly DeleteUserAction _action;

        public DeleteUserController(DeleteUserAction action)
        {
            _action = action;
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Handle(string id)
        {
            var result = await _action.Execute(id);

            return result.IsSucess ?
                Results.NoContent() :
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
