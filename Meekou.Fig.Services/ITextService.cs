using Meekou.Fig.Models;

namespace Meekou.Fig.Services
{
    public interface ITextService
    {
        Task<List<string>> Regex(RegexInput input);
    }
}