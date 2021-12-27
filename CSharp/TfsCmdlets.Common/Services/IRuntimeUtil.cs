using System.Reflection;
using System.Runtime.Versioning;

namespace TfsCmdlets.Util
{
    public interface IRuntimeUtil
    {
        string TargetFramework { get; }

        string Platform { get; }

        string OperatingSystem { get; }
    }
}
