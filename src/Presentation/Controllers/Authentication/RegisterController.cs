using Application.Actions.Authorization;
using Domain.DTOs.Authorization.Requests;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;
using Presentation.Presenters.Authorization;

namespace Presentation.Controllers.Authentication
{
    [ApiController]
    [Route("auth/register")]
    public class RegisterController : Controller
    {
        private readonly RegisterAction _action;
        private readonly RegisterPresenter _presenter;

        public RegisterController(RegisterAction action, RegisterPresenter presenter)
        {
            _action = action;
            _presenter = presenter;
        }

        [HttpPost]
        public async Task<IResult> Handle([FromBody] RegisterRequest request)
        {
            var result = await _action.Execute(request);

            return result.IsSucess ?
                Results.Ok(_presenter.Json(result.Data!)) :
                ResultsExtensions.FailureResult(result);
        }
    }
}
