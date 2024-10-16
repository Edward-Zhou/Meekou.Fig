using Meekou.Fig.Models;
using Meekou.Fig.Models.Common;
using Meekou.Fig.Services;
using Meekou.Fig.Services.Puppeteer;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Meekou.Fig.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IPuppeteerService _puppeteerService;
        private readonly IConvertService _convertService;
        public FileController(IPuppeteerService puppeteerService
            , IConvertService convertService)
        {
            _puppeteerService = puppeteerService;
            _convertService = convertService;
        }
        /// <summary>
        /// convert html string to pdf file
        /// </summary>
        /// <param name="htmlContent">html string</param>
        /// <returns>pdf file</returns>
        /// <remarks>convert html string to pdf file</remarks>
        [HttpPost]
        public async Task<Response> HtmlToPdf(string htmlContent)
        {
            var fileBytes = await _puppeteerService.HtmlToPdf(htmlContent);
            return new Response(File(fileBytes, "application/pdf", $"{MeekouConsts.MeekouShare}.pdf"));
        }
        /// <summary>
        /// convert swagger v3 to v2
        /// </summary>
        /// <param name="swaggerUrl">publich url for swagger content</param>
        /// <returns>converted v2 swagger file</returns>
        /// <remarks>convert swagger v3 to v2</remarks>
        [HttpPost]        
        public async Task<IActionResult> SwaggerThreeToTwo(string swaggerUrl)
        {
            var content = await _convertService.Swagger(swaggerUrl, Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0);
            return File(content, "application/json", $"{MeekouConsts.MeekouShare}.json");
        }
    }
}
