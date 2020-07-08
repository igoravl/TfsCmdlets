using System;

namespace TfsCmdlets.Cmdlets
{
    [AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class RequiresVersionAttribute: Attribute
    {
        public int Version {get;set;}

        public decimal Update {get;set;}

        public RequiresVersionAttribute(int version) => Version = version;
        
        public RequiresVersionAttribute(int version, int update): this(version) => Update = update;
    }
}