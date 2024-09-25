using Meekou.Fig.Models;
using Meekou.Fig.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Meekou.Fig.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {      
        private readonly ITextService _textService;
        public TextController(ITextService textService)
        {
            _textService = textService;
        }
        [HttpPost]
        public Task<List<string>> Post(RegexInput input)
        {
            var result = _textService.Regex(input);
            return result;
        }
    }
}
