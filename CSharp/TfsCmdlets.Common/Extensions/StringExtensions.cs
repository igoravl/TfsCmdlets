using System;
using System.Management.Automation;

namespace TfsCmdlets.Extensions
{
    internal static class StringExtensions
    {
        internal static bool IsLike(this string input, string pattern)
        {
            return WildcardPattern
                .Get(pattern, WildcardOptions.IgnoreCase | WildcardOptions.CultureInvariant)
                .IsMatch(input);
        }

        internal static bool IsWildcard(this string input)
        {
            return WildcardPattern.ContainsWildcardCharacters(input);
        }

        internal static bool IsGuid(this string input)
        {
            return Guid.TryParse(input, out _);
        }
    }
}
