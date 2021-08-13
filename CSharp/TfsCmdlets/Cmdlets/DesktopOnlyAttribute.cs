using System;

namespace TfsCmdlets.Cmdlets
{
    [AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class DesktopOnlyAttribute: Attribute
    {
    }
}