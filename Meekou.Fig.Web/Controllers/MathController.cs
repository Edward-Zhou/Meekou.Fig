using Meekou.Fig.Models;
using Meekou.Fig.Models.Common;
using Meekou.Fig.Services.Math;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Meekou.Fig.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MathController : ControllerBase
    {    
        private readonly IMathService _mathService;
        public MathController(IMathService mathService)
        {
            _mathService = mathService;
        }
        [HttpPost]
        public Response Evaluate(string formula)
        {
            return new Response(_mathService.Evaluate(formula));
        }
        [HttpPost]
        public Response Sum(SumInput input)
        {
            return new Response(_mathService.Sum(input));
        }
        [HttpPost]
        public Response RoundUp(decimal input)
        {
            return new Response(_mathService.RoundUp(input));
        }
    }
}
