using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets
{
    [AttributeUsage(System.AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class CmdletControllerAttribute: ExportAttribute
    {
        public Type DataType { get; }

        public string CustomCmdletName { get; set; }

        public string[] CustomVerbs { get; set; }

        public string[] CustomNouns { get; set; }

        public Type CustomBaseClass { get; set; }

        public CmdletControllerAttribute() : base(typeof(IController))
        {
        }

        public CmdletControllerAttribute(Type dataType) : base(typeof(IController))
        {
            DataType = dataType;
        }
    }

    [AttributeUsage(System.AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ModelAttribute: Attribute
    {
        public Type DataType { get; }

        public ModelAttribute(Type dataType)
        {
            DataType = dataType;
        }
    }

    [AttributeUsage(System.AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class WorkItemFieldAttribute: Attribute
    {
        public string Name { get; }
        public FieldType Type { get; }

        public WorkItemFieldAttribute(string name, FieldType type)
        {
            Name = name;
            Type = type;
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