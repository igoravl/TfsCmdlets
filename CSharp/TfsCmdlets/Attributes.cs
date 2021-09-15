using System;
using TfsCmdlets.Services;

namespace TfsCmdlets
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportsAttribute : ExportsAttributeBase
    {
        public bool Singleton { get; set; } = false;

        public ExportsAttribute(Type exports)
        {
            Exports = exports;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ControllerAttribute : ExportsAttributeBase
    {
        public ControllerAttribute(Type exports)
        {
            Exports = exports;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CommandAttribute : ExportsAttributeBase
    {
        public CommandAttribute()
        {
            Exports = typeof(ICommand);
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class InjectAttribute : Attribute
    {
    }

    [AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class DesktopOnlyAttribute : Attribute
    {
    }

    [AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class RequiresVersionAttribute : Attribute
    {
        public int Version { get; set; }

        public decimal Update { get; set; }

        public RequiresVersionAttribute(int version) => Version = version;

        public RequiresVersionAttribute(int version, int update) : this(version) => Update = update;
    }

    public class ExportsAttributeBase : Attribute
    {
        public Type Exports { get; set; }
    }
}