using System;

namespace TfsCmdlets.Util
{
    internal static class EnvironmentUtil
    {
#if NETCOREAPP3_1_OR_GREATER
        private const string ENVIRONMENT = "Core";
#elif NET47_OR_GREATER
        private const string ENVIRONMENT = "Desktop";
#else
#error Unsupported platform
#endif

        internal static string PSEdition { get; } = ENVIRONMENT;
    }
}
