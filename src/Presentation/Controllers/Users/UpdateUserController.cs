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
    public class UpdateUserController : Controller
    {
        private readonly UpdateUserAction _action;
        private readonly UpdateUserPresenter _presenter;

        public UpdateUserController(UpdateUserAction action, UpdateUserPresenter presenter)
        {
            _action = action;
            _presenter = presenter;
        }

        [Admin]
        [HttpPut("{id}")]
        public async Task<IResult> Handle(string id, [FromBody] UpdateUserRequest request)
        {
            var result = await _action.Execute(id, request);

            return result.IsSucess ?
                Results.Ok(_presenter.Json(result.Data!)) :
                ResultsExtensions.FailureResult(result);
        }
    }
}
