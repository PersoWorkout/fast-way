using Application.Actions.Users;
using Microsoft.AspNetCore.Mvc;
using Presentation.Authentication.Attributes;
using Presentation.Extensions;
using Presentation.Presenters.Authorization;

namespace Presentation.Controllers.Authentication
{
    [ApiController]
    [Route("auth/me")]
    public class MeController : Controller
    {
        private readonly GetUserByIdAction _action;
        private readonly MePresenter _presenter;

        public MeController(GetUserByIdAction action, MePresenter presenter)
        {
            _action = action;
            _presenter = presenter;
        }

        [Authenticated]
        [HttpGet]
        public async Task<IResult> Handle()
        {
            var userId = HttpContext
                .Items["userId"]!
                .ToString();

            var result = await _action.Execute(userId!);

            return result.IsSucess ?
                Results.Ok(_presenter.Json(result.Data!)) :
                ResultsExtensions.FailureResult(result);
            }
    }
}
