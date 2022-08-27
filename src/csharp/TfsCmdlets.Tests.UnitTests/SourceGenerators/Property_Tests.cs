//using System.Linq;
//using Microsoft.CodeAnalysis;
//using TfsCmdlets.SourceGenerators;
//using Xunit;

//namespace TfsCmdlets.Tests.UnitTests.SourceGenerators
//{
//    public class Property_Tests: IClassFixture<SourceGeneratorFixture>
//    {
//        public SourceGeneratorFixture Fixture { get; }

//        public Property_Tests(SourceGeneratorFixture fixture)
//        {
//            Fixture = fixture;
//        }

//        [Fact]
//        public void Can_Get_Property_Initializer()
//        {
//            const string code = @"
//public namespace SampleNamespace
//{
//  public class SampleClass
//  {
//    public string MyProp {get;set;} = ""*"";
//  }
//}
//";


//            var inputCompilation = Fixture.CreateCompilation(code);
//            var prop = inputCompilation.GetTypeByMetadataName("SampleNamespace.SampleClass").GetMembers().OfType<IPropertySymbol>().First();

//            var propInfo = new GeneratedProperty(prop, string.Empty);

//            Assert.Equal("\"*\"", propInfo.DefaultValue);

//        }
//    }
//}