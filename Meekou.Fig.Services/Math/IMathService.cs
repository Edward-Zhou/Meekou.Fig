using Meekou.Fig.Models;

namespace Meekou.Fig.Services.Math
{
    public interface IMathService
    {
        double Evaluate(string formula);
        decimal RoundUp(decimal input);
        decimal Sum(SumInput input);
    }
}