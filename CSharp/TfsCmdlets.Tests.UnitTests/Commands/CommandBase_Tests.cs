using System.Collections.Generic;
using TfsCmdlets.Commands;
using TfsCmdlets.Models;
using Xunit;

namespace TfsCmdlets.Tests.UnitTests.Commands
{
    public class CommandBase_Tests
    {
        [Fact]
        public void Can_Get_Verb_From_Type_Name()
        {
            var cmd = new GetTestCommand(null);

            //Assert.Equal("Get", cmd.Verb);
        }

        [Fact]
        public void Can_Get_Noun_From_Type_Name()
        {
            var cmd = new GetTestCommand(null);

            //Assert.Equal("TestCommand", cmd.Noun);
        }

        private class GetTestCommand : CommandBase<object>
        {
            public GetTestCommand(TpcConnection collection) : base(collection)
            {
            }

            public override IEnumerable<object> Invoke(ParameterDictionary parameters)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}