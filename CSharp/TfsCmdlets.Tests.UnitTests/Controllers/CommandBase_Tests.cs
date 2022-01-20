// using System.Collections.Generic;
// using TfsCmdlets.Controllers;
// using TfsCmdlets.Models;
// using TfsCmdlets.Services;
// using Xunit;

// namespace TfsCmdlets.Tests.UnitTests.Commands
// {
//     public class CommandBase_Tests
//     {
//         [Fact]
//         public void Can_Get_Verb_From_Type_Name()
//         {
//             var cmd = new GetTestController();

//             //Assert.Equal("Get", cmd.Verb);
//         }

//         [Fact]
//         public void Can_Get_Noun_From_Type_Name()
//         {
//             var cmd = new GetTestController();

//             //Assert.Equal("TestCommand", cmd.Noun);
//         }

//         private class GetTestController : ControllerBase<object>
//         {
//             public GetTestController()
//             : base(null, null, null)
//             {
//             }

//             protected override IEnumerable Run()
//             {
//                 throw new System.NotImplementedException();
//             }
//         }
//     }
// }