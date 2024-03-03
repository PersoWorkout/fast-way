using Application.Actions.Authorization;
using Domain.DTOs.Authorization.Requests;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;

namespace Presentation.Controllers.Authentication
{
    [ApiController]
    [Route("auth/register")]
    public class RegisterController : Controller
    {
        private readonly RegisterAction _action;

        public RegisterController(RegisterAction action)
        {
            _action = action;
        }

        [HttpPost]
        public async Task<IResult> Handle([FromBody] RegisterRequest request)
        {
            var result = await _action.Execute(request);

            return result.IsSucess ?
                Results.Ok(result.Data) :
                ResultsExtensions.FailureResult(result);
        }
    }
}
