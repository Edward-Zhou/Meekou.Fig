using Meekou.Fig.Models;
using Meekou.Fig.Models.Common;
using Meekou.Fig.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Meekou.Fig.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TextController : ControllerBase
    {      
        private readonly ITextService _textService;
        public TextController(ITextService textService)
        {
            _textService = textService;
        }
        [HttpPost]
        public Response Regex(RegexInput input)
        {
            var result = _textService.Regex(input);
            return new Response(result);
        }
    }
}
