﻿using System;
using System.Management.Automation;

namespace TfsCmdlets.Extensions
{
    internal static class StringExtensions
    {
        internal static bool IsLike(this string input, string pattern)
        {
            if(string.IsNullOrEmpty(pattern)) return false;
            
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

        internal static int FindIndex(this string input, Predicate<char> predicate, int startIndex = 0)
        {
            for (int i = startIndex; i < input.Length; i++)
            {
                if (predicate(input[i])) return i;
            }

            return -1;
        }
    }
}
