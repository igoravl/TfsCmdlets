namespace TfsCmdlets.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToShortString(this TimeSpan timeSpan, bool includeMilliseconds = false)
        {
            var format = new StringBuilder();

            if (timeSpan.Days > 0)
            {
                format.Append(@"d\d");
            }
            if (timeSpan.Hours > 0)
            {
                format.Append(@"h\h");
            }
            if (timeSpan.Minutes > 0)
            {
                format.Append(@"m\m");
            }
            if (timeSpan.Seconds > 0)
            {
                format.Append(@"s\s");
            }
            if (includeMilliseconds && timeSpan.Milliseconds > 0)
            {
                format.Append(@"f\m\s");
            }

            return timeSpan.ToString(format.ToString());
        }
    }
}