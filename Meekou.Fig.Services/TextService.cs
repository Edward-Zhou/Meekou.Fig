using Meekou.Fig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Meekou.Fig.Services
{
    public class TextService: ITextService
    {
        public async Task<List<RegexOutput>> Regex(RegexInput input)
        {
            // Create a Regex object with the provided pattern
            Regex regex = new Regex(input.Pattern);

            // Find all matches in the input string
            MatchCollection matches = regex.Matches(input.Content);

            // Create a list to store all the matched strings
            List<RegexOutput> results = new List<RegexOutput>();

            // Iterate through all the matches and add them to the list
            foreach (Match match in matches)
            {
                results.Add(new RegexOutput
                {
                    Match = match.Value,
                    Groups = match.Groups.Values.Where(g => g.Value != match.Value).Select(g => g.Value).ToList(),
                });
            }

            return results;
        }
    }
}
