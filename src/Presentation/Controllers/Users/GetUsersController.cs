using Application.Actions.Users;
using Microsoft.AspNetCore.Mvc;
using Presentation.Authorization.Attributes;
using Presentation.Presenters.Users;

namespace Presentation.Controllers.Users
{
    [ApiController]
    [Route("/users")]
    public class GetUsersController : Controller
    {
        private readonly GetUsersAction _action;
        private readonly GetUsersPresenter _presenter;

        public GetUsersController(GetUsersAction action, GetUsersPresenter presenter)
        {
            _action = action;
            _presenter = presenter;
        }

        [Admin]
        [HttpGet]
        public async Task<IResult> Handle()
        {
            var result = await _action.Execute();

            return Results.Ok(_presenter.Json(result.Data!));
        }
    }
}
