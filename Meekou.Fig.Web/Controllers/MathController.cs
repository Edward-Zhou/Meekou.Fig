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
        /// <summary>
        /// evaluate formula text
        /// </summary>
        /// <param name="formula">formula text</param>
        /// <returns>formula calculate result</returns>
        /// <remarks>formula calculate result</remarks>
        [HttpPost]
        public Response Evaluate(string formula)
        {
            return new Response(_mathService.Evaluate(formula));
        }
        /// <summary>
        /// sum value for json by path
        /// </summary>
        /// <param name="input">iput for sum</param>
        /// <returns>sum value result</returns>
        /// <remarks>sum value result</remarks>
        [HttpPost]
        public Response Sum(SumInput input)
        {
            return new Response(_mathService.Sum(input));
        }
        /// <summary>
        /// round up number value
        /// </summary>
        /// <param name="input">number value</param>
        /// <returns>round up result for input</returns>
        /// <remarks>round up result for input</remarks>
        [HttpPost]
        public Response RoundUp(decimal input)
        {
            return new Response(_mathService.RoundUp(input));
        }
    }
}
