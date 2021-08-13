using System;

namespace TfsCmdlets.Util
{
    internal static class EnvironmentUtil
    {
        internal static string PSEdition { get; } = (AppDomain.CurrentDomain.FriendlyName.Equals("DefaultDomain") ? "Desktop" : "Core");
    }
}
