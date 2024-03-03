using Application.Actions.Users;
using Domain.DTOs.Users.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using Presentation.Authorization.Attributes;
using Presentation.Extensions;

namespace Presentation.Controllers.Users
{
    [ApiController]
    [Route("/users")]
    public class UpdateUserController : Controller
    {
        private readonly UpdateUserAction _action;

        public UpdateUserController(UpdateUserAction action)
        {
            _action = action;
        }

        [Admin]
        [HttpPut("{id}")]
        public async Task<IResult> Handle(string id, [FromBody] UpdateUserRequest request)
        {
            var result = await _action.Execute(id, request);

            return result.IsSucess ?
                Results.Ok(result.Data) :
                ResultsExtensions.FailureResult(result);
        }
    }
}
