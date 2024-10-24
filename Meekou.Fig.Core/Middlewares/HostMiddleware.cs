using Meekou.Fig.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Meekou.Fig.Core.Middlewares
{
    public class HostMiddleware
    {
        const string hostHeader = MeekouConsts.CustomHost;
        private readonly RequestDelegate _next;
        private readonly HttpClient _httpClient;

        public HostMiddleware(RequestDelegate next,
            HttpClient httpClient)
        {
            _next = next;
            _httpClient = httpClient;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the header is present; if not, you can either set a default or do nothing
            if (context.Request.Headers.TryGetValue(hostHeader, out var host)
                && !string.IsNullOrEmpty(host))
            {
                // Forward the request to the new host
                var targetUrl = host.ToString().TrimEnd('/'); // Ensure no trailing slash
                var requestMessage = new HttpRequestMessage
                {
                    Method = new HttpMethod(context.Request.Method),
                    RequestUri = new Uri(new Uri(targetUrl), context.Request.Path + context.Request.QueryString), // Combine base URL and path
                    Content = new StreamContent(context.Request.Body) // Forward the original request body
                };

                // Copy the headers from the original request
                foreach (var header in context.Request.Headers
                                              .Where(h => h.Key != "Host" && h.Key != hostHeader))
                {
                    requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                }

                // Send the request to the new host
                var responseMessage = await _httpClient.SendAsync(requestMessage);

                // Copy the response headers and status code back to the original response
                context.Response.StatusCode = (int)responseMessage.StatusCode;
                foreach (var header in responseMessage.Headers)
                {
                    context.Response.Headers[header.Key] = header.Value.ToArray();
                }
                foreach (var header in responseMessage.Content.Headers)
                {
                    context.Response.Headers[header.Key] = header.Value.ToArray();
                }

                // Write the response body to the original response
                await responseMessage.Content.CopyToAsync(context.Response.Body);
                return; // End the middleware pipeline
            }
            await _next(context);
        }
    }
}
