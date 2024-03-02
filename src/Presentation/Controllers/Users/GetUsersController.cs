using Application.Actions.Users;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Users
{
    [ApiController]
    [Route("/users")]
    public class GetUsersController : Controller
    {
        public readonly GetUsersAction _action;

        public GetUsersController(GetUsersAction action)
        {
            _action = action;
        }

        [HttpGet]
        public async Task<IResult> Handle()
        {
            var result = await _action.Execute();

            return Results.Ok(result.Data);
        }
    }
}
