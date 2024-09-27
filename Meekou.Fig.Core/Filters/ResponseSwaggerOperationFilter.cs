using Meekou.Fig.Models.Common;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meekou.Fig.Core.Filters
{
    public class ResponseSwaggerOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Update each response to be wrapped in ApiResponse<T>
            foreach (var response in operation.Responses)
            {
                if (response.Value.Content.Count == 0)
                {
                    continue;
                }
                var originalSchema = response.Value.Content.First().Value.Schema;

                // Define the wrapped schema (ApiResponse<T>)
                var wrappedSchema = new OpenApiSchema
                {
                    Type = "object",
                    Properties =
                    {
                        ["Success"] = new OpenApiSchema { Type = "boolean" },
                        ["Error"] = new OpenApiSchema { Type = typeof(ErrorInfo).Name },
                        ["Result"] = originalSchema // Original response schema goes into "data"
                    }
                };

                // Update the content schema
                foreach (var contentType in response.Value.Content.Keys.ToList())
                {
                    response.Value.Content[contentType].Schema = wrappedSchema;
                }
            }
        }
    }
}
