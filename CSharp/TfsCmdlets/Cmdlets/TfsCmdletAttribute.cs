using System;

namespace TfsCmdlets.Cmdlets
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class TfsCmdletAttribute : Attribute
    {
        public CmdletScope Scope { get; }

        public bool DesktopOnly { get; set; }

        public bool HostedOnly { get; set; }

        public int RequiresVersion { get; set; }

        public bool SkipAutoProperties { get; set; }

        public bool NoAutoPipeline { get; set; }

        public Type DataType { get; set; }

        public Type OutputType { get; set; }

        public bool SupportsShouldProcess { get; set; }

        public string DefaultParameterSetName { get; set; }

        public TfsCmdletAttribute(CmdletScope scope = CmdletScope.None)
        {
            Scope = scope;
        }
    }

    internal enum CmdletScope
    {
        None = 0,
        Server = 1,
        Collection = 2,
        Project = 3,
        Team = 4
    }
}
