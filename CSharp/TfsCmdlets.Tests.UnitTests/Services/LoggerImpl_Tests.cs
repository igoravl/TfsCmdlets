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

        private static (LoggerImpl Logger, List<string> Output) CreateLogger()
        {
            var output = new List<string>();
            var ps = Substitute.For<IPowerShellService>();
            ps.IsVerbose.Returns(true);
            ps.When(p => p.WriteVerbose(Arg.Any<string>())).Do(c => output.Add(c.Arg<string>()));
            return (new LoggerImpl(ps), output);
        }

        [Theory]
        [InlineData("Header: Authorization: Bearer {0}", "Authorization: Bearer *****")]
        [InlineData("Authorization: Basic {0}", "Authorization: Basic *****")]
        [InlineData("Proxy-Authorization={0}", "Proxy-Authorization=*****")]
        [InlineData("token is Bearer {0}", "Bearer *****")]
        [InlineData("GET https://dev.azure.com/org/_apis/x?api-version=7.1&access_token={0}&sig={0}", "?api-version=7.1&access_token=*****&sig=*****")]
        [InlineData("GET https://someuser:{0}@dev.azure.com/org", "https://*****@dev.azure.com/org")]
        public void Log_Scrubs_Secrets_From_Messages(string template, string expected)
        {
            var (logger, output) = CreateLogger();

            // Short secrets must be redacted too - no minimum length cutoff
            foreach (var secret in new[] { Secret, "abc" })
            {
                output.Clear();
                logger.Log(string.Format(template, secret));

                Assert.DoesNotContain(secret, output[0].Substring(output[0].IndexOf("] ", output[0].IndexOf("] ") + 2) + 2));
                Assert.Contains(expected, output[0]);
            }
        }

        [Fact]
        public void Log_Keeps_NonSensitive_Text()
        {
            var (logger, output) = CreateLogger();

            logger.Log("Using the Basic process template; GET https://dev.azure.com/org/_apis/projects?api-version=7.1&$top=10");

            Assert.Contains("Basic process template", output[0]);
            Assert.Contains("?api-version=7.1&$top=10", output[0]);
        }

        [Fact]
        public void LogError_Does_Not_Serialize_Exception_Data()
        {
            var ps = Substitute.For<IPowerShellService>();
            ps.IsVerbose.Returns(true);
            var output = new List<string>();
            ps.When(p => p.WriteVerbose(Arg.Any<string>())).Do(c => output.Add(c.Arg<string>()));

            var inner = new InvalidOperationException("inner failure");
            inner.Data["RequestHeaders"] = $"X-Custom: {Secret}";
            var ex = new Exception("outer failure", inner);
            ex.Data["Body"] = Secret;

            new LoggerImpl(ps).LogError(ex);

            var log = string.Join("\n", output);
            Assert.DoesNotContain(Secret, log);
            Assert.Contains("System.InvalidOperationException", log);
            Assert.Contains("outer failure", log);
            Assert.Contains("inner failure", log);
            ps.Received(1).WriteError(Arg.Is<ErrorRecord>(r => r.Exception == ex));
        }
    }
}
