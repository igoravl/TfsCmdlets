using System;
using System.Collections.Generic;
using System.Text;
using TfsCmdlets.Controllers.WorkItem.AreasIterations;
using Xunit;

namespace TfsCmdlets.Tests.UnitTests.Controllers
{
    public class NewIteration_Tests
    {
        public ControllerFixtures ControllerFixtures { get; }

        [Fact]
        public void Can_Specify_Duration_With_Weekends()
        {
            //var controller = new NewClassificationNodeController();
        }

        public NewIteration_Tests(ControllerFixtures controllerFixtures, object nodeUtilFixture)
        {
            ControllerFixtures = controllerFixtures;
        }
    }
}
