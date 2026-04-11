using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;
using Microsoft.VisualStudio.Services.TokenAdmin.Client;
using Microsoft.VisualStudio.Services.WebApi.Contracts.DelegatedAuthorization;
using NSubstitute;
using TfsCmdlets.Cmdlets.Identity.PersonalAccessToken;
using TfsCmdlets.HttpClients;
using TfsCmdlets.Services;
using Xunit;

namespace TfsCmdlets.Tests.UnitTests.Controllers.Identity
{
    public class GetPersonalAccessToken_Tests
    {
        private readonly IPowerShellService _powerShell = Substitute.For<IPowerShellService>();
        private readonly IDataManager _data = Substitute.For<IDataManager>();
        private readonly IParameterManager _parms = Substitute.For<IParameterManager>();
        private readonly ILogger _logger = Substitute.For<ILogger>();
        private readonly ITokensHttpClient _client = Substitute.For<ITokensHttpClient>();
        private readonly ITokenAdminHttpClient _adminClient = Substitute.For<ITokenAdminHttpClient>();

        [Fact]
        public void Get_By_Id_Returns_Token()
        {
            // Arrange
            var tokenId = Guid.NewGuid();
            var expectedToken = new PatToken { AuthorizationId = tokenId, DisplayName = "TestToken" };

            _parms.HasParameter("ParameterSetName").Returns(true);
            _parms.Get<string>("ParameterSetName", Arg.Any<string>()).Returns("Get by id");
            _parms.HasParameter("AuthorizationId").Returns(true);
            _parms.Get<Guid>("AuthorizationId", Arg.Any<Guid>()).Returns(tokenId);
            _parms.HasParameter("PersonalAccessToken").Returns(false);

            _client.GetPatAsync(tokenId)
                .Returns(Task.FromResult(new PatTokenResult { PatToken = expectedToken, PatTokenError = SessionTokenError.None }));

            var controller = new GetPersonalAccessTokenController(
                _adminClient, _client, _powerShell, _data, _parms, _logger);

            // Act
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert
            Assert.Single(results);
            Assert.Equal(expectedToken, results[0]);
        }

        [Fact]
        public void Get_By_Id_Logs_Error_When_Token_Not_Found()
        {
            // Arrange
            var tokenId = Guid.NewGuid();

            _parms.HasParameter("ParameterSetName").Returns(true);
            _parms.Get<string>("ParameterSetName", Arg.Any<string>()).Returns("Get by id");
            _parms.HasParameter("AuthorizationId").Returns(true);
            _parms.Get<Guid>("AuthorizationId", Arg.Any<Guid>()).Returns(tokenId);
            _parms.HasParameter("PersonalAccessToken").Returns(false);

            _client.GetPatAsync(tokenId)
                .Returns(Task.FromResult(new PatTokenResult { PatTokenError = SessionTokenError.InvalidAuthorizationId }));

            var controller = new GetPersonalAccessTokenController(
                _adminClient, _client, _powerShell, _data, _parms, _logger);

            // Act
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert
            Assert.Empty(results);
            _logger.Received().LogError(Arg.Any<string>());
        }

        [Fact]
        public void Get_By_Name_Lists_And_Filters_Tokens()
        {
            // Arrange
            var token1 = new PatToken { DisplayName = "Build-Token", AuthorizationId = Guid.NewGuid() };
            var token2 = new PatToken { DisplayName = "Deploy-Token", AuthorizationId = Guid.NewGuid() };
            var token3 = new PatToken { DisplayName = "Build-Other", AuthorizationId = Guid.NewGuid() };

            _parms.HasParameter("ParameterSetName").Returns(true);
            _parms.Get<string>("ParameterSetName", Arg.Any<string>()).Returns("Get by name");
            _parms.HasParameter("PersonalAccessToken").Returns(true);
            _parms.Get<object>("PersonalAccessToken", Arg.Any<object>()).Returns("Build-*");
            _parms.HasParameter("DisplayFilter").Returns(false);
            _parms.Get<DisplayFilterOptions>("DisplayFilter", Arg.Any<DisplayFilterOptions>()).Returns(DisplayFilterOptions.Active);
            _parms.HasParameter("SortBy").Returns(false);
            _parms.Get<SortByOptions>("SortBy", Arg.Any<SortByOptions>()).Returns(SortByOptions.DisplayName);
            _parms.HasParameter("IsSortAscending").Returns(false);
            _parms.Get<bool>("IsSortAscending", Arg.Any<bool>()).Returns(false);
            _parms.HasParameter("User").Returns(false);

            _client.ListPatsAsync(
                    Arg.Any<DisplayFilterOptions?>(),
                    Arg.Any<SortByOptions?>(),
                    Arg.Any<bool?>(),
                    Arg.Any<string>())
                .Returns(Task.FromResult(new PagedPatTokens
                {
                    PatTokens = new List<PatToken> { token1, token2, token3 },
                    ContinuationToken = null
                }));

            var controller = new GetPersonalAccessTokenController(
                _adminClient, _client, _powerShell, _data, _parms, _logger);

            // Act
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert
            Assert.Equal(2, results.Count);
            Assert.Contains(token1, results);
            Assert.Contains(token3, results);
            Assert.DoesNotContain(token2, results);
        }

        [Fact]
        public void Get_For_User_Projects_SessionTokens_To_PatTokens()
        {
            // Arrange
            var authId1 = Guid.NewGuid();
            var authId2 = Guid.NewGuid();
            var validFrom = DateTime.UtcNow.AddDays(-10);
            var validTo = DateTime.UtcNow.AddDays(20);

            var sessionTokens = new List<Microsoft.VisualStudio.Services.DelegatedAuthorization.SessionToken>
            {
                new Microsoft.VisualStudio.Services.DelegatedAuthorization.SessionToken
                {
                    AuthorizationId = authId1,
                    DisplayName = "Admin-Token",
                    Scope = "vso.work",
                    ValidFrom = validFrom,
                    ValidTo = validTo
                },
                new Microsoft.VisualStudio.Services.DelegatedAuthorization.SessionToken
                {
                    AuthorizationId = authId2,
                    DisplayName = "Other-Token",
                    Scope = "vso.code"
                }
            };

            _parms.HasParameter("ParameterSetName").Returns(true);
            _parms.Get<string>("ParameterSetName", Arg.Any<string>()).Returns("Get for user");
            _parms.HasParameter("PersonalAccessToken").Returns(true);
            _parms.Get<object>("PersonalAccessToken", Arg.Any<object>()).Returns("Admin-*");
            _parms.HasParameter("User").Returns(true);
            _parms.Get<object>("User", Arg.Any<object>()).Returns("aad.descriptor");

            _adminClient.ListPersonalAccessTokensAsync(
                    Arg.Any<Microsoft.VisualStudio.Services.Common.SubjectDescriptor>(),
                    Arg.Any<int?>(),
                    Arg.Any<string>())
                .Returns(Task.FromResult(new TokenAdminPagedSessionTokens
                {
                    SessionTokens = sessionTokens,
                    ContinuationToken = null
                }));

            var controller = new GetPersonalAccessTokenController(
                _adminClient, _client, _powerShell, _data, _parms, _logger);

            // Act
            var results = controller.InvokeCommand().Cast<PatToken>().ToList();

            // Assert — wildcard "Admin-*" should match only the first token, projected to PatToken
            Assert.Single(results);
            var result = results[0];
            Assert.IsType<PatToken>(result);
            Assert.Equal(authId1, result.AuthorizationId);
            Assert.Equal("Admin-Token", result.DisplayName);
            Assert.Equal("vso.work", result.Scope);
            Assert.Equal(validFrom, result.ValidFrom);
            Assert.Equal(validTo, result.ValidTo);
        }
    }

    public class NewPersonalAccessToken_Tests
    {
        private readonly IPowerShellService _powerShell = Substitute.For<IPowerShellService>();
        private readonly IDataManager _data = Substitute.For<IDataManager>();
        private readonly IParameterManager _parms = Substitute.For<IParameterManager>();
        private readonly ILogger _logger = Substitute.For<ILogger>();
        private readonly ITokensHttpClient _client = Substitute.For<ITokensHttpClient>();

        [Fact]
        public void Create_Token_Returns_New_Token()
        {
            // Arrange
            var expectedToken = new PatToken
            {
                DisplayName = "MyToken",
                AuthorizationId = Guid.NewGuid(),
                Scope = "vso.work_full",
                ValidTo = DateTime.UtcNow.AddDays(30)
            };

            _parms.HasParameter("DisplayName").Returns(true);
            _parms.Get<string>("DisplayName", Arg.Any<string>()).Returns("MyToken");
            _parms.HasParameter("Scope").Returns(true);
            _parms.Get<string>("Scope", Arg.Any<string>()).Returns("vso.work_full");
            _parms.HasParameter("ValidTo").Returns(true);
            _parms.Get<DateTime>("ValidTo", Arg.Any<DateTime>()).Returns(expectedToken.ValidTo);
            _parms.HasParameter("AllOrganizations").Returns(false);
            _parms.Get<bool>("AllOrganizations", Arg.Any<bool>()).Returns(false);

            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(true);
            _data.GetCollection().Returns(new Models.Connection(
                new Microsoft.VisualStudio.Services.WebApi.VssConnection(
                    new Uri("https://dev.azure.com/testorg"),
                    new Microsoft.VisualStudio.Services.Common.VssCredentials())));

            _client.CreatePatAsync(Arg.Any<PatTokenCreateRequest>())
                .Returns(Task.FromResult(new PatTokenResult { PatToken = expectedToken, PatTokenError = SessionTokenError.None }));

            var controller = new NewPersonalAccessTokenController(
                _client, _powerShell, _data, _parms, _logger);

            // Act
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert
            Assert.Single(results);
            Assert.Equal(expectedToken, results[0]);
            _logger.Received().LogWarn(Arg.Is<string>(s => s.Contains("Save the token")));
        }

        [Fact]
        public void Create_Token_WhatIf_Returns_Nothing()
        {
            // Arrange
            _parms.HasParameter("DisplayName").Returns(true);
            _parms.Get<string>("DisplayName", Arg.Any<string>()).Returns("MyToken");
            _parms.HasParameter("Scope").Returns(true);
            _parms.Get<string>("Scope", Arg.Any<string>()).Returns("vso.work_full");
            _parms.HasParameter("ValidTo").Returns(true);
            _parms.Get<DateTime>("ValidTo", Arg.Any<DateTime>()).Returns(DateTime.UtcNow.AddDays(30));
            _parms.HasParameter("AllOrganizations").Returns(false);
            _parms.Get<bool>("AllOrganizations", Arg.Any<bool>()).Returns(false);

            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(false);
            _data.GetCollection().Returns(new Models.Connection(
                new Microsoft.VisualStudio.Services.WebApi.VssConnection(
                    new Uri("https://dev.azure.com/testorg"),
                    new Microsoft.VisualStudio.Services.Common.VssCredentials())));

            var controller = new NewPersonalAccessTokenController(
                _client, _powerShell, _data, _parms, _logger);

            // Act
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert
            Assert.Empty(results);
            _client.DidNotReceive().CreatePatAsync(Arg.Any<PatTokenCreateRequest>());
        }

        [Fact]
        public void Create_Token_Throws_On_Error()
        {
            // Arrange
            _parms.HasParameter("DisplayName").Returns(true);
            _parms.Get<string>("DisplayName", Arg.Any<string>()).Returns("MyToken");
            _parms.HasParameter("Scope").Returns(true);
            _parms.Get<string>("Scope", Arg.Any<string>()).Returns("vso.work_full");
            _parms.HasParameter("ValidTo").Returns(true);
            _parms.Get<DateTime>("ValidTo", Arg.Any<DateTime>()).Returns(DateTime.UtcNow.AddDays(30));
            _parms.HasParameter("AllOrganizations").Returns(false);
            _parms.Get<bool>("AllOrganizations", Arg.Any<bool>()).Returns(false);

            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(true);
            _data.GetCollection().Returns(new Models.Connection(
                new Microsoft.VisualStudio.Services.WebApi.VssConnection(
                    new Uri("https://dev.azure.com/testorg"),
                    new Microsoft.VisualStudio.Services.Common.VssCredentials())));

            _client.CreatePatAsync(Arg.Any<PatTokenCreateRequest>())
                .Returns(Task.FromResult(new PatTokenResult { PatTokenError = SessionTokenError.InvalidScope }));

            var controller = new NewPersonalAccessTokenController(
                _client, _powerShell, _data, _parms, _logger);

            // Act & Assert
            Assert.Throws<Exception>(() => controller.InvokeCommand().Cast<object>().ToList());
        }
    }

    public class SetPersonalAccessToken_Tests
    {
        private readonly IPowerShellService _powerShell = Substitute.For<IPowerShellService>();
        private readonly IDataManager _data = Substitute.For<IDataManager>();
        private readonly IParameterManager _parms = Substitute.For<IParameterManager>();
        private readonly ILogger _logger = Substitute.For<ILogger>();
        private readonly ITokensHttpClient _client = Substitute.For<ITokensHttpClient>();

        [Fact]
        public void Set_Token_Updates_DisplayName()
        {
            // Arrange
            var tokenId = Guid.NewGuid();
            var currentToken = new PatToken
            {
                AuthorizationId = tokenId,
                DisplayName = "OldName",
                Scope = "vso.work"
            };
            var updatedToken = new PatToken
            {
                AuthorizationId = tokenId,
                DisplayName = "NewName",
                Scope = "vso.work"
            };

            _parms.HasParameter("PersonalAccessToken").Returns(true);
            _parms.Get<object>("PersonalAccessToken", Arg.Any<object>()).Returns(tokenId);
            _parms.HasParameter("DisplayName").Returns(true);
            _parms.Get<string>("DisplayName", Arg.Any<string>()).Returns("NewName");
            _parms.HasParameter("Scope").Returns(false);
            _parms.HasParameter("ValidTo").Returns(false);
            _parms.HasParameter("AllOrganizations").Returns(false);

            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            _client.GetPatAsync(tokenId)
                .Returns(Task.FromResult(new PatTokenResult { PatToken = currentToken, PatTokenError = SessionTokenError.None }));

            _client.UpdatePatAsync(Arg.Any<PatTokenUpdateRequest>())
                .Returns(Task.FromResult(new PatTokenResult { PatToken = updatedToken, PatTokenError = SessionTokenError.None }));

            var controller = new SetPersonalAccessTokenController(
                _client, _powerShell, _data, _parms, _logger);

            // Act
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert
            Assert.Single(results);
            Assert.Equal(updatedToken, results[0]);
            _client.Received().UpdatePatAsync(Arg.Is<PatTokenUpdateRequest>(r =>
                r.DisplayName == "NewName" && r.AuthorizationId == tokenId));
        }

        [Fact]
        public void Set_Token_WhatIf_Does_Not_Update()
        {
            // Arrange
            var tokenId = Guid.NewGuid();
            var currentToken = new PatToken { AuthorizationId = tokenId, DisplayName = "ExistingToken" };

            _parms.HasParameter("PersonalAccessToken").Returns(true);
            _parms.Get<object>("PersonalAccessToken", Arg.Any<object>()).Returns(tokenId);
            _parms.HasParameter("DisplayName").Returns(true);
            _parms.Get<string>("DisplayName", Arg.Any<string>()).Returns("NewName");
            _parms.HasParameter("Scope").Returns(false);
            _parms.HasParameter("ValidTo").Returns(false);
            _parms.HasParameter("AllOrganizations").Returns(false);

            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(false);

            _client.GetPatAsync(tokenId)
                .Returns(Task.FromResult(new PatTokenResult { PatToken = currentToken, PatTokenError = SessionTokenError.None }));

            var controller = new SetPersonalAccessTokenController(
                _client, _powerShell, _data, _parms, _logger);

            // Act
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert
            Assert.Empty(results);
            _client.DidNotReceive().UpdatePatAsync(Arg.Any<PatTokenUpdateRequest>());
        }
    }

    public class UpdatePersonalAccessToken_Tests
    {
        private readonly IPowerShellService _powerShell = Substitute.For<IPowerShellService>();
        private readonly IDataManager _data = Substitute.For<IDataManager>();
        private readonly IParameterManager _parms = Substitute.For<IParameterManager>();
        private readonly ILogger _logger = Substitute.For<ILogger>();
        private readonly ITokensHttpClient _client = Substitute.For<ITokensHttpClient>();

        [Fact]
        public void Regenerate_Token_Revokes_Old_And_Creates_New()
        {
            // Arrange
            var tokenId = Guid.NewGuid();
            var currentToken = new PatToken
            {
                AuthorizationId = tokenId,
                DisplayName = "MyToken",
                Scope = "vso.work",
                ValidTo = DateTime.UtcNow.AddDays(30)
            };
            var newToken = new PatToken
            {
                AuthorizationId = Guid.NewGuid(),
                DisplayName = "MyToken",
                Scope = "vso.work"
            };

            _parms.HasParameter("PersonalAccessToken").Returns(true);
            _parms.Get<object>("PersonalAccessToken", Arg.Any<object>()).Returns(tokenId);
            _parms.HasParameter("Scope").Returns(false);
            _parms.HasParameter("ValidTo").Returns(false);
            _parms.HasParameter("AllOrganizations").Returns(false);

            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            _client.GetPatAsync(tokenId)
                .Returns(Task.FromResult(new PatTokenResult { PatToken = currentToken, PatTokenError = SessionTokenError.None }));

            _client.RevokeAsync(tokenId).Returns(Task.CompletedTask);

            _client.CreatePatAsync(Arg.Any<PatTokenCreateRequest>())
                .Returns(Task.FromResult(new PatTokenResult { PatToken = newToken, PatTokenError = SessionTokenError.None }));

            var controller = new UpdatePersonalAccessTokenController(
                _client, _powerShell, _data, _parms, _logger);

            // Act
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert
            Assert.Single(results);
            Assert.Equal(newToken, results[0]);
            _client.Received().RevokeAsync(tokenId);
            _client.Received().CreatePatAsync(Arg.Is<PatTokenCreateRequest>(r => r.DisplayName == "MyToken"));
            _logger.Received().LogWarn(Arg.Is<string>(s => s.Contains("Save the new token")));
        }

        [Fact]
        public void Regenerate_Token_Overrides_Scope_When_Specified()
        {
            // Arrange
            var tokenId = Guid.NewGuid();
            var currentToken = new PatToken
            {
                AuthorizationId = tokenId,
                DisplayName = "MyToken",
                Scope = "vso.work",
                ValidTo = DateTime.UtcNow.AddDays(30)
            };
            var newToken = new PatToken { AuthorizationId = Guid.NewGuid(), DisplayName = "MyToken", Scope = "vso.code_write" };

            _parms.HasParameter("PersonalAccessToken").Returns(true);
            _parms.Get<object>("PersonalAccessToken", Arg.Any<object>()).Returns(tokenId);
            _parms.HasParameter("Scope").Returns(true);
            _parms.Get<string>("Scope", Arg.Any<string>()).Returns("vso.code_write");
            _parms.HasParameter("ValidTo").Returns(false);
            _parms.HasParameter("AllOrganizations").Returns(false);

            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            _client.GetPatAsync(tokenId)
                .Returns(Task.FromResult(new PatTokenResult { PatToken = currentToken, PatTokenError = SessionTokenError.None }));
            _client.RevokeAsync(tokenId).Returns(Task.CompletedTask);
            _client.CreatePatAsync(Arg.Any<PatTokenCreateRequest>())
                .Returns(Task.FromResult(new PatTokenResult { PatToken = newToken, PatTokenError = SessionTokenError.None }));

            var controller = new UpdatePersonalAccessTokenController(
                _client, _powerShell, _data, _parms, _logger);

            // Act
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert
            Assert.Single(results);
            _client.Received().CreatePatAsync(Arg.Is<PatTokenCreateRequest>(r => r.Scope == "vso.code_write"));
        }
    }

    public class RemovePersonalAccessToken_Tests
    {
        private readonly IPowerShellService _powerShell = Substitute.For<IPowerShellService>();
        private readonly IDataManager _data = Substitute.For<IDataManager>();
        private readonly IParameterManager _parms = Substitute.For<IParameterManager>();
        private readonly ILogger _logger = Substitute.For<ILogger>();
        private readonly ITokensHttpClient _client = Substitute.For<ITokensHttpClient>();
        private readonly ITokenAdminHttpClient _adminClient = Substitute.For<ITokenAdminHttpClient>();

        [Fact]
        public void Remove_Own_Token_With_Force()
        {
            // Arrange
            var tokenId = Guid.NewGuid();

            _parms.HasParameter("PersonalAccessToken").Returns(true);
            _parms.Get<object>("PersonalAccessToken", Arg.Any<object>()).Returns(tokenId);
            _parms.HasParameter("User").Returns(false);
            _parms.HasParameter("Force").Returns(true);
            _parms.Get<bool>("Force", Arg.Any<bool>()).Returns(true);

            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            _client.RevokeAsync(tokenId).Returns(Task.CompletedTask);

            var controller = new RemovePersonalAccessTokenController(
                _adminClient, _client, _powerShell, _data, _parms, _logger);

            // Act
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert
            Assert.Empty(results); // Remove doesn't return output
            _client.Received().RevokeAsync(tokenId);
        }

        [Fact]
        public void Remove_Own_Token_Without_Force_Requires_Confirmation()
        {
            // Arrange
            var tokenId = Guid.NewGuid();

            _parms.HasParameter("PersonalAccessToken").Returns(true);
            _parms.Get<object>("PersonalAccessToken", Arg.Any<object>()).Returns(tokenId);
            _parms.HasParameter("User").Returns(false);
            _parms.HasParameter("Force").Returns(false);
            _parms.Get<bool>("Force", Arg.Any<bool>()).Returns(false);

            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(true);
            _powerShell.ShouldContinue(Arg.Any<string>()).Returns(false); // User says no

            var controller = new RemovePersonalAccessTokenController(
                _adminClient, _client, _powerShell, _data, _parms, _logger);

            // Act
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert
            Assert.Empty(results);
            _client.DidNotReceive().RevokeAsync(Arg.Any<Guid>());
        }

        [Fact]
        public void Remove_WhatIf_Does_Not_Revoke()
        {
            // Arrange
            var tokenId = Guid.NewGuid();

            _parms.HasParameter("PersonalAccessToken").Returns(true);
            _parms.Get<object>("PersonalAccessToken", Arg.Any<object>()).Returns(tokenId);
            _parms.HasParameter("User").Returns(false);
            _parms.HasParameter("Force").Returns(true);
            _parms.Get<bool>("Force", Arg.Any<bool>()).Returns(true);

            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(false);

            var controller = new RemovePersonalAccessTokenController(
                _adminClient, _client, _powerShell, _data, _parms, _logger);

            // Act
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert
            Assert.Empty(results);
            _client.DidNotReceive().RevokeAsync(Arg.Any<Guid>());
        }

        [Fact]
        public void Remove_Admin_Token_With_Force()
        {
            // Arrange
            var tokenId = Guid.NewGuid();
            var user = "aad.user-descriptor";

            _parms.HasParameter("PersonalAccessToken").Returns(true);
            _parms.Get<object>("PersonalAccessToken", Arg.Any<object>()).Returns(tokenId);
            _parms.HasParameter("User").Returns(true);
            _parms.Get<string>("User", Arg.Any<string>()).Returns(user);
            _parms.HasParameter("Force").Returns(true);
            _parms.Get<bool>("Force", Arg.Any<bool>()).Returns(true);

            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            _adminClient.RevokeAuthorizationsAsync(
                Arg.Any<IEnumerable<TokenAdminRevocation>>())
                .Returns(Task.CompletedTask);

            var controller = new RemovePersonalAccessTokenController(
                _adminClient, _client, _powerShell, _data, _parms, _logger);

            // Act
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert
            Assert.Empty(results);
            _adminClient.Received().RevokeAuthorizationsAsync(
                Arg.Any<IEnumerable<TokenAdminRevocation>>());
        }
    }
}
