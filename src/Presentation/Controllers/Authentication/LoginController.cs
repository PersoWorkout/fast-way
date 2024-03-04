using Application.Actions.Authorization;
using Domain.DTOs.Authorization.Requests;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;
using Presentation.Presenters.Authorization;

namespace Presentation.Controllers.Authentication
{
    [ApiController]
    [Route("auth/login")]
    public class LoginController(
        LoginAction action, 
        LoginPresenter presenter) : Controller
    {
        private readonly LoginAction _action = action;
        private readonly LoginPresenter _presenter = presenter;

        [HttpPost]
        public async Task<IResult> Handle([FromBody] LoginRequest request)
        {
            var result = await _action.Execute(request);

            return result.IsSucess ?
                Results.Ok(_presenter.Json(result.Data!)) :
                ResultsExtensions.FailureResult(result);
        }
    }
}
