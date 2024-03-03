using Domain.Abstractions;
using Microsoft.OpenApi.Extensions;

namespace Presentation.Extensions
{
    public static class ResultsExtensions
    {
        public static IResult FailureResult<T>(Result<T> data) where T:class
        {

            return Results.Problem(
                    statusCode: (int)data.StatusCode,
                    title: data.StatusCode.GetDisplayName(),
                    extensions: new Dictionary<string, object?>()
                    {
                        {"errors", data.Errors }
                    });
        }
    }
}
