// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using NSubstitute;
// using TfsCmdlets.Controllers.Admin;
// using TfsCmdlets.Models;
// using TfsCmdlets.Services;
// using Xunit;

// namespace TfsCmdlets.Tests.UnitTests.Commands.Admin
// {
//     public class GetVersion_Tests
//     {
//         [Fact]
//         public void Can_Get_Hosted_Version()
//         {
//             // Arrange

//             var fixture = new CommandsFixture();

//             var tfsVersionTable = Substitute.For<ITfsVersionTable>();
            
//             var restApi = Substitute.For<IRestApiService>();

//             var powerShell = Substitute.For<IPowerShellService>();

//             var data = Substitute.For<IDataManager>();
//             data.GetCollection(null).ReturnsForAnyArgs(fixture.GetAzdoCollection());

//             var logger = Substitute.For<ILogger>();

//             var parms = new ParameterDictionary();

//             var cmd = new GetVersion(tfsVersionTable, restApi, powerShell, data, logger);

//             // Act

//             var actual = cmd.Invoke(parms).First();

//             // Assert

//             Assert.True(actual.IsHosted);;
//         }
//     }
// }
