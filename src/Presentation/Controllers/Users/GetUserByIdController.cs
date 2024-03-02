using Application.Actions.Users;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Users
{
    [ApiController]
    [Route("/users")]
    public class GetUserByIdController : Controller
    {
        private readonly GetUserByIdAction _action;

        public GetUserByIdController(GetUserByIdAction action)
        {
            _action = action;
        }

        [HttpGet("{id}")]
        public async Task<IResult> Handle(string id)
        {
            var result = await _action.Execute(id);

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
