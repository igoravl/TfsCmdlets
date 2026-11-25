using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Security;
using NSubstitute;
using TfsCmdlets.Services;
using TfsCmdlets.Services.Impl;
using Xunit;

namespace TfsCmdlets.Tests.UnitTests.Services
{
    public class LoggerImpl_Tests
    {
        private const string Secret = "SYNTHETIC-s3cr3t-VALUE-0123456789";

        private static string LogParameters(Dictionary<string, object> values)
        {
            var output = new List<string>();
            var ps = Substitute.For<IPowerShellService>();
            ps.IsVerbose.Returns(true);
            ps.CurrentCommand.Returns("Test-Command");
            ps.When(p => p.WriteVerbose(Arg.Any<string>())).Do(c => output.Add(c.Arg<string>()));

            var parameters = Substitute.For<IParameterManager>();
            parameters.Keys.Returns(values.Keys.ToList());
            parameters[Arg.Any<string>()].Returns(c => values.TryGetValue(c.Arg<string>(), out var v) ? v : null);

            new LoggerImpl(ps).LogParameters("Test-Command", parameters);

            return string.Join("\n", output);
        }

        private static SecureString ToSecureString(string s)
        {
            var ss = new SecureString();
            foreach (var c in s) ss.AppendChar(c);
            return ss;
        }

        [Theory]
        [InlineData("Password")]
        [InlineData("PersonalAccessToken")]
        [InlineData("personalaccesstoken")]
        [InlineData("AccessToken")]
        [InlineData("CLIENTSECRET")]
        [InlineData("ApiKey")]
        [InlineData("Body")]
        [InlineData("body")]
        public void LogParameters_Redacts_Sensitive_Parameter_Names(string name)
        {
            var output = LogParameters(new Dictionary<string, object> { [name] = Secret });

            Assert.Contains(name, output);
            Assert.DoesNotContain(Secret, output);
            Assert.Contains("*****", output);
        }

        [Fact]
        public void LogParameters_Redacts_Sensitive_Header_Values_But_Keeps_Keys()
        {
            var headers = new Hashtable
            {
                ["authorization"] = $"Basic {Secret}",
                ["X-TFS-FedAuthToken"] = Secret,
                ["Cookie"] = Secret,
                ["Nested"] = new Hashtable { ["Proxy-Authorization"] = Secret, ["Plain"] = "nested-visible" },
                ["Accept"] = "application/json"
            };

            var output = LogParameters(new Dictionary<string, object> { ["AdditionalHeaders"] = headers });

            Assert.DoesNotContain(Secret, output);
            Assert.Contains("authorization", output);
            Assert.Contains("X-TFS-FedAuthToken", output);
            Assert.Contains("Proxy-Authorization", output);
            Assert.Contains("application/json", output);
            Assert.Contains("nested-visible", output);
        }

        [Fact]
        public void LogParameters_Redacts_Credential_Objects_Regardless_Of_Parameter_Name()
        {
            var output = LogParameters(new Dictionary<string, object>
            {
                ["Credential"] = new PSCredential("someuser", ToSecureString(Secret)),
                ["Foo"] = new PSCredential("someuser", ToSecureString(Secret)),
                ["Bar"] = new NetworkCredential("someuser", Secret),
                ["Baz"] = new PSObject(new NetworkCredential("someuser", Secret)),
                ["Qux"] = new { Name = "visible", Password = Secret, Inner = new NetworkCredential("u", Secret) }
            });

            Assert.DoesNotContain(Secret, output);
            Assert.Contains("visible", output);
        }

        [Fact]
        public void LogParameters_Keeps_NonSensitive_Values()
        {
            var output = LogParameters(new Dictionary<string, object>
            {
                ["Resource"] = "_apis/projects",
                ["Method"] = "GET",
                ["Passthru"] = new SwitchParameter(true),
                ["AuthorizationId"] = Guid.Empty
            });

            Assert.Contains("_apis/projects", output);
            Assert.Contains("GET", output);
            Assert.Contains("\"Passthru\" = true", output);
            Assert.Contains(Guid.Empty.ToString(), output);
        }

        [Fact]
        public void Log_Scrubs_Authorization_Tokens_From_Messages()
        {
            var output = new List<string>();
            var ps = Substitute.For<IPowerShellService>();
            ps.IsVerbose.Returns(true);
            ps.When(p => p.WriteVerbose(Arg.Any<string>())).Do(c => output.Add(c.Arg<string>()));

            var logger = new LoggerImpl(ps);
            logger.Log($"Header: Authorization: Bearer {Secret}");
            logger.Log("Using the Basic process template");

            Assert.DoesNotContain(Secret, output[0]);
            Assert.Contains("Bearer *****", output[0]);
            Assert.Contains("Basic process template", output[1]);
        }
    }
}
