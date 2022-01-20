using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TfsCmdlets.SourceGenerators
{
    public interface IFilter: ISyntaxContextReceiver
    {
        IDictionary<string, (INamedTypeSymbol, ClassDeclarationSyntax)> TypesToProcess { get; }

        bool ShouldProcessType(INamedTypeSymbol type);
    }
}
