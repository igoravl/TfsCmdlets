// using System;
// using System.Collections.Generic;
// using System.Reflection;
// using System.Text;
// using Microsoft.CodeAnalysis;
// using Microsoft.CodeAnalysis.CSharp;

// namespace TfsCmdlets.Tests.UnitTests
// {
//     public class SourceGeneratorFixture: IDisposable
//     {
//         public Compilation CreateCompilation(string source)
//             => CSharpCompilation.Create("compilation",
//                 new[] { CSharpSyntaxTree.ParseText(source) },
//                 new[] { MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location) },
//                 new CSharpCompilationOptions(OutputKind.ConsoleApplication));

//         public void Dispose()
//         {
//         }
//     }
// }
