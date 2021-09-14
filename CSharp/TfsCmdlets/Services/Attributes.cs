using System;
using System.Collections.Generic;

namespace TfsCmdlets.Services
{
    public class BaseExportsAttribute : Attribute
    {
        public Type Exports { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportsAttribute : BaseExportsAttribute
    {
        public bool Singleton { get; set; } = false;

        public ExportsAttribute(Type exports)
        {
            Exports = exports;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ControllerAttribute : BaseExportsAttribute
    {
        public ControllerAttribute(Type exports)
        {
            Exports = exports;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class InjectAttribute : Attribute
    {
        public InjectAttribute()
        {
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public class InjectConnectionAttribute : InjectAttribute
    {
        public ClientScope Scope { get; private set; }

        public bool Optional { get; set; }

        public InjectConnectionAttribute(ClientScope scope)
        {
            Scope = scope;
        }
    }

    [AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class DesktopOnlyAttribute : Attribute
    {
    }
}