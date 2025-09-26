using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets
{
    [AttributeUsage(System.AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class WorkItemFieldAttribute : Attribute
    {
        public string Name { get; }
        public FieldType Type { get; }

        public WorkItemFieldAttribute(string name, FieldType type)
        {
            Name = name;
            Type = type;
        }
    }
}