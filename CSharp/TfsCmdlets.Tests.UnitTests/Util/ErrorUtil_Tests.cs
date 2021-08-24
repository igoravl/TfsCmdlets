using Xunit;
using TfsCmdlets.Util;
using System;

namespace TfsCmdlets.Tests.UnitTests.Util
{
    public class ErrorUtil_Tests
    {
        [Fact]
        public void ThrowDesktopOnlyCmdlet_Throws_NotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => 
                ErrorUtil.ThrowDesktopOnlyCmdlet()
            );
        }

        [Fact]
        public void ThrowIfNotFound_Throws_ArgumentException()
        {
            Assert.Throws<ArgumentException>("test", () => 
                ErrorUtil.ThrowIfNotFound(null, "test", "searchCriteria")
            );
        }

    }
}