using Meekou.Fig.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meekou.Fig.Core.Filters
{
    public class ResponseExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            // Log the exception here if necessary (logging skipped in this example)
            var exception = context.Exception;

            // Create a custom error response
            var errorResponse = new Response(new ErrorInfo
            {
                Code = context.HttpContext.Response.StatusCode,
                Message = exception.Message,
                Details = exception.InnerException.Message
            });

            // Return the custom error response with a 500 Internal Server Error status
            context.Result = new ObjectResult(errorResponse);

            // Mark exception as handled
            context.ExceptionHandled = true;
        }
    }
}
