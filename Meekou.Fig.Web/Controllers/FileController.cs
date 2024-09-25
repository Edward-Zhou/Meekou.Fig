using Meekou.Fig.Models;
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
        public FileController(IPuppeteerService puppeteerService)
        {
            _puppeteerService = puppeteerService;
        }
        [HttpPost]
        public async Task<IActionResult> HtmlToPdf(string htmlContent)
        {
            var fileBytes = await _puppeteerService.HtmlToPdf(htmlContent);
            return File(fileBytes, "application/pdf", $"{MeekouConsts.MeekouShare}.pdf");
        }
    }
}
