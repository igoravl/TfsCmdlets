using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace TfsCmdlets.SourceGenerators.Generators.HttpClients
{
    public record MethodInfo
    {
        public MethodInfo(IMethodSymbol methodInfo)
        {
            Name = methodInfo.Name;
            ReturnType = methodInfo.ReturnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        }

        public string Name { get; private set; }
        public string ReturnType { get; private set; }
        //public List<ParameterInfo> Parameters { get; private set; } = new();
        //public bool IsAsync { get; private set; }
        //public bool IsGeneric { get; private set; }
        //public List<string> GenericParameters { get; private set; } = new();
        //public bool IsVoid => ReturnType == "void" || ReturnType == "System.Threading.Tasks.Task";
        //public string AsyncSuffix => IsAsync && !IsVoid ? "Async" : string.Empty;
        //public override string ToString()
        //{
        //    var sb = new StringBuilder();
        //    sb.Append("public ");
        //    if (IsAsync && !IsVoid)
        //    {
        //        sb.Append("async ");
        //    }
        //    sb.Append(ReturnType);
        //    sb.Append(" ");
        //    sb.Append(Name);
        //    if (IsGeneric && GenericParameters.Count > 0)
        //    {
        //        sb.Append("<");
        //        sb.Append(string.Join(", ", GenericParameters));
        //        sb.Append(">");
        //    }
        //    sb.Append("(");
        //    sb.Append(string.Join(", ", Parameters));
        //    sb.Append(")");
        //    return sb.ToString();
        //}
    }
}
