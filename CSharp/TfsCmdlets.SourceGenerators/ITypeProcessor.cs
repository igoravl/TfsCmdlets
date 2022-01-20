using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TfsCmdlets.SourceGenerators
{
    public interface ITypeProcessor
    {
        void Initialize(INamedTypeSymbol type, ClassDeclarationSyntax cds, GeneratorExecutionContext context);
        INamedTypeSymbol Type { get; }
        ClassDeclarationSyntax ClassDeclaration { get; }
        GeneratorExecutionContext Context { get; }
        string Name { get; }
        string FullName { get; }
        string Namespace { get; }
        bool Ignore { get; }
        string GenerateCode();
    }
}
