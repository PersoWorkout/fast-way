using Application.Actions.Users;
using Microsoft.AspNetCore.Mvc;
using Presentation.Authorization.Attributes;
using Presentation.Extensions;
using Presentation.Presenters.Users;

namespace Presentation.Controllers.Users
{
    [ApiController]
    [Route("/users")]
    public class GetUserByIdController : Controller
    {
        private readonly GetUserByIdAction _action;
        private readonly GetUserByIdPresenter _presenter;

        public GetUserByIdController(GetUserByIdAction action, GetUserByIdPresenter presenter)
        {
            _action = action;
            _presenter = presenter;
        }

        [Admin]
        [HttpGet("{id}")]
        public async Task<IResult> Handle(string id)
        {
            var result = await _action.Execute(id);

            return result.IsSucess ?
                Results.Ok(_presenter.Json(result.Data!)) :
                ResultsExtensions.FailureResult(result);
        }
    }
}
