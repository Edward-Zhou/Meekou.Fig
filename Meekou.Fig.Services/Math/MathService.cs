using Meekou.Fig.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meekou.Fig.Services.Math
{
    public class MathService: IMathService
    {
        public double Evaluate(string formula)
        {
            DataTable table = new DataTable();
            var result = table.Compute(formula, null);
            return Convert.ToDouble(result);
        }
        public decimal Sum(SumInput input)
        {
            var dataValue = JToken.Parse(input.Data);
            var datas = dataValue.SelectTokens($"$.{input.Path}").ToList();
            var result = datas.Sum(d => (decimal)d);
            return result;
        }
        public decimal RoundUp(decimal input)
        {
            var result = System.Math.Ceiling(input);
            return result;
        }
    }
}
