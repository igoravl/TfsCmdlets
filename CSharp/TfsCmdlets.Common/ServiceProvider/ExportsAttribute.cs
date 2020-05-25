using System;

namespace TfsCmdlets.Services
{
    internal class ExportsAttribute: Attribute
    {
        public Type Exports { get; set; }

        public bool Singleton { get; set; } = false;

        public ExportsAttribute(Type exports)
        {
            Exports = exports;
        }
    }
}
