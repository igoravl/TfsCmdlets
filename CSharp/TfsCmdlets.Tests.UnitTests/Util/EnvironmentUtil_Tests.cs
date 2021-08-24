using Xunit;
using TfsCmdlets.Util;
using System;

namespace TfsCmdlets.Tests.UnitTests.Util
{
    public class EnvironmentUtil_Tests
    {
        [Fact]
        [Trait("Platform", "Core")]
        public void PSEdition_Returns_Core_When_PSCore()
        {
            Assert.Equal("Core", EnvironmentUtil.PSEdition);
        }

        [Fact]
        [Trait("Platform", "Desktop")]
        public void PSEdition_Returns_Desktop_When_PSWindows()
        {
            Assert.Equal("Desktop", EnvironmentUtil.PSEdition);
        }

    }
}