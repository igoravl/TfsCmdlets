using System.Reflection;
using System.Runtime.Versioning;

namespace TfsCmdlets.Util
{
    [Export(typeof(IRuntimeUtil)), Shared]
    internal class RuntimeUtil: IRuntimeUtil
    {
        private static readonly string _frameworkName = Assembly.GetExecutingAssembly()?
            .GetCustomAttribute<TargetFrameworkAttribute>()?
            .FrameworkName;

        public string TargetFramework { get; } = _frameworkName;

        public string Platform { get; } = _frameworkName.StartsWith(".NETFramework") ? "Desktop" : "Core";

        public string OperatingSystem { get; } = Environment.OSVersion.Platform == PlatformID.Win32NT ? "Windows" : Environment.OSVersion.Platform.ToString();
    }
}
