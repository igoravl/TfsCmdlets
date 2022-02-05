using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TfsCmdlets.SourceGenerators
{
    internal static class Extensions
    {
        public static T GetAttributeConstructorValue<T>(this INamedTypeSymbol symbol, string attributeName, int argumentPosition = 0)
        {
            var attr = symbol
                .GetAttributes()
                .FirstOrDefault(a => a.AttributeClass.Name.Equals(attributeName));

            if (attr == null || attr.ConstructorArguments == null || attr.ConstructorArguments.Length <= argumentPosition) return default;

            var arg = attr.ConstructorArguments[argumentPosition];

            return (T)arg.Value;
        }

        public static T GetAttributeNamedValue<T>(this INamedTypeSymbol symbol, string attributeName, string argumentName)
        {
            var attr = symbol
                .GetAttributes()
                .FirstOrDefault(a => a.AttributeClass.Name.Equals(attributeName));

            if (attr == null) return default;

            var arg = attr.NamedArguments.FirstOrDefault(a => a.Key.Equals(argumentName));

            return (T)(arg.Value.Value ?? default(T));
        }

        public static bool GetAttributeNamedValue(INamedTypeSymbol symbol, string attributeName, string argumentName, bool defaultValue = false)
        {
            var attr = symbol
                .GetAttributes()
                .FirstOrDefault(a => a.AttributeClass.Name.Equals(attributeName));

            if (attr == null) return defaultValue;

            var arg = attr.NamedArguments.FirstOrDefault(a => a.Key.Equals(argumentName));

            return (arg.Value.Value?.ToString() ?? string.Empty).Equals("True", StringComparison.OrdinalIgnoreCase);
        }

        public static bool HasAttribute(this ISymbol symbol, string attributeName)
            => symbol.GetAttributes().Any(a => a.AttributeClass.Name.Equals(attributeName));

        public static int FindIndex(this string input, Predicate<char> predicate, int startIndex = 0)
        {
            for (var i = startIndex; i < input.Length; i++)
            {
                if (predicate(input[i])) return i;
            }

            return -1;
        }

        public static string GetImportingConstructorArguments(this INamedTypeSymbol type, INamedTypeSymbol baseClass)
            => string.Join(", ", type.GetPropertiesWithAttribute("ImportAttribute")
                .Select(p => $"{p.Type.Name} {p.Name[0].ToString().ToLower()}{p.Name.Substring(1)}")
                .Concat(baseClass
                    .Constructors[0]
                    .Parameters
                    .Select(p => $"{p.Type.Name} {p.Name}")
                )
            );

        public static string GetConstructorArguments(this INamedTypeSymbol type)
        {
            return string.Join(", ", type
                .Constructors[0]
                .Parameters
                .Select(parm => parm.Name));
        }

        public static IEnumerable<IPropertySymbol> GetPropertiesWithAttribute<T>(this INamedTypeSymbol type)
            where T : Attribute
            => GetPropertiesWithAttribute(type, typeof(T).Name);

        public static IEnumerable<IPropertySymbol> GetPropertiesWithAttribute(this INamedTypeSymbol type, string attributeName)
            => type
                .GetMembers()
                .OfType<IPropertySymbol>()
                .Where(p => p.GetAttributes().Any(
                    a => a.AttributeClass.Name.Equals(attributeName)));

        public static string FullName(this INamedTypeSymbol symbol)
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

        public static string FullNamespace(this ISymbol symbol)
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


        public static bool HasDefaultConstructor(this INamedTypeSymbol symbol)
        {
            return symbol.Constructors.Any(c => c.Parameters.Count() == 0);
        }

        public static IEnumerable<IPropertySymbol> ReadWriteScalarProperties(this INamedTypeSymbol symbol)
        {
            return symbol.GetMembers().OfType<IPropertySymbol>().Where(p => p.CanRead() && p.CanWrite() && !p.HasParameters());
        }

        public static IEnumerable<IPropertySymbol> ReadableScalarProperties(this INamedTypeSymbol symbol)
        {
            return symbol.GetMembers().OfType<IPropertySymbol>().Where(p => p.CanRead() && !p.HasParameters());
        }

        public static IEnumerable<IPropertySymbol> WritableScalarProperties(this INamedTypeSymbol symbol)
        {
            return symbol.GetMembers().OfType<IPropertySymbol>().Where(p => p.CanWrite() && !p.HasParameters());
        }

        public static bool CanRead(this IPropertySymbol symbol) => symbol.GetMethod != null;

        public static bool CanWrite(this IPropertySymbol symbol) => symbol.SetMethod != null;

        public static bool HasParameters(this IPropertySymbol symbol) => symbol.Parameters.Any();

        public static bool IsPartial(this ClassDeclarationSyntax cds) 
            => cds.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword));

    }
}
