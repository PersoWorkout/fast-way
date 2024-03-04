using Application.Actions.Users;
using Domain.DTOs.Users.Request;
using Microsoft.AspNetCore.Mvc;
using Presentation.Authorization.Attributes;
using Presentation.Extensions;
using Presentation.Presenters.Users;

namespace Presentation.Controllers.Users
{
    [ApiController]
    [Route("/users")]
    public class CreateUserController : Controller
    {
        private readonly CreateUserAction _action;
        private readonly CreateUserPresenter _presenter;

        public CreateUserController(CreateUserAction action, CreateUserPresenter presenter)
        {
            _action = action;
            _presenter = presenter;
        }

        [Admin]
        [HttpPost]
        public async Task<IResult> Handle([FromBody] CreateUserRequest request)
        {
            var result = await _action.Execute(request);

            return result.IsSucess ?
                Results.Ok(_presenter.Json(result.Data!)) :
                ResultsExtensions.FailureResult(result);
        }
    }
}
