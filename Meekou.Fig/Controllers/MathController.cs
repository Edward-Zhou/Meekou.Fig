using Meekou.Fig.Models;
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
        public double Evaluate(string formula)
        {
            return _mathService.Evaluate(formula);
        }
        [HttpPost]
        public decimal Sum(SumInput input)
        {
            return _mathService.Sum(input);
        }
        [HttpPost]
        public decimal RoundUp(decimal input)
        {
            return _mathService.RoundUp(input);
        }
    }
}
