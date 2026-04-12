using System.Management.Automation;
using DotNet.Globbing;
using Newtonsoft.Json.Linq;

namespace TfsCmdlets.Extensions
{
    public static class StringExtensions
    {
        private static readonly GlobOptions _defaultGlobOptions = new GlobOptions { Evaluation = { CaseInsensitive = true } };

        public static bool IsLike(this string input, string pattern)
        {
            if(string.IsNullOrEmpty(pattern)) return false;
            
            return WildcardPattern
                .Get(pattern, WildcardOptions.IgnoreCase | WildcardOptions.CultureInvariant)
                .IsMatch(input);
        }

        public static bool IsLikeGlob(this string input, string pattern)
        {
            if(string.IsNullOrEmpty(pattern)) return false;
            
            var g = Glob.Parse(pattern, _defaultGlobOptions);

            return g.IsMatch(input);
        }

        public static bool IsWildcard(this string input)
        {
            return WildcardPattern.ContainsWildcardCharacters(input);
        }

        public static bool IsGuid(this string input)
        {
            return Guid.TryParse(input, out _);
        }

        public static int FindIndex(this string input, Predicate<char> predicate, int startIndex = 0)
        {
            for (int i = startIndex; i < input.Length; i++)
            {
                if (predicate(input[i])) return i;
            }

            return -1;
        }
    }
}
