using Application.Actions.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using Presentation.Authorization.Attributes;

namespace Presentation.Controllers.Users
{
    [ApiController]
    [Route("/users")]
    public class GetUserByIdController : Controller
    {
        private readonly GetUserByIdAction _action;

        public GetUserByIdController(GetUserByIdAction action)
        {
            _action = action;
        }

        [Admin]
        [HttpGet("{id}")]
        public async Task<IResult> Handle(string id)
        {
            var result = await _action.Execute(id);

            return result.IsSucess ?
                Results.Ok(result.Data) :
                Results.Problem(
                    statusCode: (int)result.StatusCode,
                    title: result.StatusCode.GetDisplayName(),
                    extensions: new Dictionary<string, object?>()
                    {
                        {"errors", result.Errors }
                    });
        }
    }
}
