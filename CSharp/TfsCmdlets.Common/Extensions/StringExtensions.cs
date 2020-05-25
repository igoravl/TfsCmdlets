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
    }
}
