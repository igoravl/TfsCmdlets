using System;
using System.Composition;
using TfsCmdlets.Services;

namespace TfsCmdlets
{
    [AttributeUsage(System.AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class CmdletControllerAttribute: ExportAttribute
    {
        public Type DataType { get; }

        public CmdletControllerAttribute() : base(typeof(IController))
        {
        }

        public CmdletControllerAttribute(Type dataType) : base(typeof(IController))
        {
            DataType = dataType;
        }
    }

    // [AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    // public sealed class DesktopOnlyAttribute : Attribute
    // {
    // }

    // [AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    // public sealed class RequiresVersionAttribute : Attribute
    // {
    //     public int Version { get; set; }

    //     public decimal Update { get; set; }

    //     public RequiresVersionAttribute(int version) => Version = version;

    //     public RequiresVersionAttribute(int version, int update) : this(version) => Update = update;
    // }
}