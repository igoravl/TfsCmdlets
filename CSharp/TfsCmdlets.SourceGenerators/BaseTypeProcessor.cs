using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TfsCmdlets.SourceGenerators
{
    public abstract class BaseTypeProcessor : ITypeProcessor
    {
        public INamedTypeSymbol Type { get; set; }

        public ClassDeclarationSyntax ClassDeclaration { get; set; }

        public GeneratorExecutionContext Context { get; set; }

        public string Name => Type.Name;

        public string FullName => Type.FullName();

        public string Namespace => Type.FullNamespace();

        public virtual bool Ignore { get; set; } = false;

        public abstract string GenerateCode();

        protected abstract void OnInitialize();

        protected Logger Logger { get; private set; }

        public void Initialize(Logger logger, INamedTypeSymbol type, ClassDeclarationSyntax cds, GeneratorExecutionContext context)
        {
            Type = type;
            ClassDeclaration = cds;
            Context = context;
            Logger = logger;

            OnInitialize();
        }
    }
}
