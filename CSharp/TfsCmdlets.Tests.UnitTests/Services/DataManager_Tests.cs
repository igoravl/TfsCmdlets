using System;
using NSubstitute;
using TfsCmdlets.Services;
using TfsCmdlets.Services.Impl;
using Xunit;

namespace TfsCmdlets.Tests.UnitTests.Services
{
    public class DataManager_Tests
    {
        [Fact]
        public void Server_Hostname_Without_Scheme_Throws_Instead_Of_Recursing()
        {
            var parameters = Substitute.For<IParameterManager>();
            parameters.PushContext(Arg.Any<object>(), Arg.Any<string>()).Returns(Substitute.For<IDisposable>());
            parameters.Get<object>("Server", Arg.Any<object>()).Returns("azuredevops.example.com");
            var data = new DataManagerImpl(Array.Empty<Lazy<IController>>(), parameters,
                Substitute.For<ILogger>(), Substitute.For<ICurrentConnections>());

            var error = Assert.Throws<ArgumentException>(() => data.GetServer());

            Assert.Equal("Server", error.ParamName);
            Assert.Contains("absolute URL", error.Message);
        }

        [Fact]
        public void Collection_Name_With_Invalid_Server_Throws_Instead_Of_Recursing()
        {
            var parameters = Substitute.For<IParameterManager>();
            parameters.PushContext(Arg.Any<object>(), Arg.Any<string>()).Returns(Substitute.For<IDisposable>());
            parameters.Get<object>("Collection", Arg.Any<object>()).Returns("OurCollection");
            parameters.Get<object>("Server", Arg.Any<object>()).Returns("azuredevops.example.com");
            var data = new DataManagerImpl(Array.Empty<Lazy<IController>>(), parameters,
                Substitute.For<ILogger>(), Substitute.For<ICurrentConnections>());

            var error = Assert.Throws<ArgumentException>(() => data.GetCollection());

            Assert.Equal("Server", error.ParamName);
        }
    }
}
