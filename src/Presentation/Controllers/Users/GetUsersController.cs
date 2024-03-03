using Application.Actions.Users;
using Microsoft.AspNetCore.Mvc;
using Presentation.Authentication.Attributes;
using Presentation.Authorization.Attributes;

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

        [Admin]
        [HttpGet]
        public async Task<IResult> Handle()
        {
            var result = await _action.Execute();

            return Results.Ok(result.Data);
        }
    }
}
