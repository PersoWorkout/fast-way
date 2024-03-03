using Application.Actions.Users;
using Microsoft.AspNetCore.Mvc;
using Presentation.Authorization.Attributes;
using Presentation.Extensions;

namespace Presentation.Controllers.Users
{
    [ApiController]
    [Route("/users")]
    public class DeleteUserController : Controller
    {
        private readonly DeleteUserAction _action;

        public DeleteUserController(DeleteUserAction action)
        {
            _action = action;
        }

        [Admin]
        [HttpDelete("{id}")]
        public async Task<IResult> Handle(string id)
        {
            var result = await _action.Execute(id);

            return result.IsSucess ?
                Results.NoContent() :
                ResultsExtensions.FailureResult(result);
        }
    }
}
