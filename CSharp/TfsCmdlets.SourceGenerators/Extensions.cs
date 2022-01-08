using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace TfsCmdlets.SourceGenerators
{
    internal static class Extensions
    {
        public static T GetAttributeConstructorValue<T>(this INamedTypeSymbol symbol, string attributeName, int argumentPosition = 0)
        {
            var attr = symbol
                .GetAttributes()
                .FirstOrDefault(a => a.AttributeClass.Name.Equals(attributeName));

            if (attr == null || attr.ConstructorArguments == null || attr.ConstructorArguments.Length <= argumentPosition) return default(T);

            var arg = attr.ConstructorArguments[argumentPosition];

            return (T)arg.Value;
        }

        public static T GetAttributeNamedValue<T>(this INamedTypeSymbol symbol, string attributeName, string argumentName)
        {
            var attr = symbol
                .GetAttributes()
                .FirstOrDefault(a => a.AttributeClass.Name.Equals(attributeName));

            if (attr == null) return default(T);

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
    }
}
