using Meekou.Fig.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Meekou.Fig.Core.Filters
{
    public class ResponseWrapperFilter : IAsyncResultFilter
    {
        public ResponseWrapperFilter()
        {
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult objectResult)
            {
                // Get the actual result object from the action
                var resultValue = objectResult.Value;

                // Wrap the result in ApiResponse
                var apiResponse = new Response(resultValue);

                // Set the new result
                context.Result = new ObjectResult(apiResponse)
                {
                    StatusCode = objectResult.StatusCode
                };
            }
            await next();
        }
    }
}
