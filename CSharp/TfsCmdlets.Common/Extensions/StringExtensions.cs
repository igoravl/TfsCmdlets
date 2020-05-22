using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

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
