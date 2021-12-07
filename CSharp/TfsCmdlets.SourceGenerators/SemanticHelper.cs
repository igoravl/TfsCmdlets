using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators
{
    internal static class SemanticHelper
    {
        internal static string FullName(this INamedTypeSymbol symbol)
        {
            if (symbol == null)
                return null;

            var prefix = FullNamespace(symbol);
            var suffix = "";
            if (symbol.Arity > 0)
            {
                suffix = "<" + string.Join(", ", symbol.TypeArguments.Select(targ => FullName((INamedTypeSymbol)targ))) + ">";
            }

            if (prefix != "")
                return prefix + "." + symbol.Name + suffix;
            else
                return symbol.Name + suffix;
        }

        internal static string FullNamespace(this ISymbol symbol)
        {
            var parts = new Stack<string>();
            INamespaceSymbol iterator = (symbol as INamespaceSymbol) ?? symbol.ContainingNamespace;
            while (iterator != null)
            {
                if (!string.IsNullOrEmpty(iterator.Name))
                    parts.Push(iterator.Name);
                iterator = iterator.ContainingNamespace;
            }
            return string.Join(".", parts);
        }


        internal static bool HasDefaultConstructor(this INamedTypeSymbol symbol)
        {
            return symbol.Constructors.Any(c => c.Parameters.Count() == 0);
        }

        internal static IEnumerable<IPropertySymbol> ReadWriteScalarProperties(this INamedTypeSymbol symbol)
        {
            return symbol.GetMembers().OfType<IPropertySymbol>().Where(p => p.CanRead() && p.CanWrite() && !p.HasParameters());
        }

        internal static IEnumerable<IPropertySymbol> ReadableScalarProperties(this INamedTypeSymbol symbol)
        {
            return symbol.GetMembers().OfType<IPropertySymbol>().Where(p => p.CanRead() && !p.HasParameters());
        }

        internal static IEnumerable<IPropertySymbol> WritableScalarProperties(this INamedTypeSymbol symbol)
        {
            return symbol.GetMembers().OfType<IPropertySymbol>().Where(p => p.CanWrite() && !p.HasParameters());
        }

        internal static bool CanRead(this IPropertySymbol symbol) => symbol.GetMethod != null;
        internal static bool CanWrite(this IPropertySymbol symbol) => symbol.SetMethod != null;
        internal static bool HasParameters(this IPropertySymbol symbol) => symbol.Parameters.Any();
    }
}
