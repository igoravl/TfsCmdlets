using System.Management.Automation;

namespace TfsCmdlets.Extensions
{
    public static class StringExtensions
    {
        public static bool IsLike(this string input, string pattern)
        {
            if(string.IsNullOrEmpty(pattern)) return false;
            
            return WildcardPattern
                .Get(pattern, WildcardOptions.IgnoreCase | WildcardOptions.CultureInvariant)
                .IsMatch(input);
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

        public static T ToJsonObject<T>(this string self)
        {
            return (T) Newtonsoft.Json.JsonConvert.DeserializeObject(self);
        }
    }
}
