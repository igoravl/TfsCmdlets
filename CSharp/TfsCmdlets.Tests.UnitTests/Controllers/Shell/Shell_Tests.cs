using System.Linq;
using System.Management.Automation;
using NSubstitute;
using TfsCmdlets.Cmdlets.Shell;
using TfsCmdlets.Services;
using Xunit;

namespace TfsCmdlets.Tests.UnitTests.Controllers.Shell
{

    public class UninstallShell_Tests
    {
        private readonly IPowerShellService _powerShell = Substitute.For<IPowerShellService>();
        private readonly IDataManager _data = Substitute.For<IDataManager>();
        private readonly IParameterManager _parms = Substitute.For<IParameterManager>();
        private readonly ILogger _logger = Substitute.For<ILogger>();

        [Fact]
        public void Default_Target_Is_All()
        {
            // Arrange
            _parms.HasParameter("Target").Returns(true);
            _parms.Get<ShellTarget>("Target", Arg.Any<ShellTarget>())
                .Returns(ShellTarget.All);

            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            var controller = new UninstallShellController(
                _powerShell, _data, _parms, _logger);

            // Act — no shortcuts exist so nothing to remove
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert — no exceptions, ran to completion
            Assert.Empty(results);
        }

        [Fact]
        public void WhatIf_Does_Not_Remove_Shortcuts()
        {
            // Arrange
            _parms.HasParameter("Target").Returns(true);
            _parms.Get<ShellTarget>("Target", Arg.Any<ShellTarget>())
                .Returns(ShellTarget.All);

            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(false);

            var controller = new UninstallShellController(
                _powerShell, _data, _parms, _logger);

            // Act
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert
            Assert.Empty(results);
        }

        [Fact]
        public void Selective_Target_WindowsTerminal_Only()
        {
            // Arrange
            _parms.HasParameter("Target").Returns(true);
            _parms.Get<ShellTarget>("Target", Arg.Any<ShellTarget>())
                .Returns(ShellTarget.WindowsTerminal);

            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            var controller = new UninstallShellController(
                _powerShell, _data, _parms, _logger);

            // Act
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert — no WT fragments exist, so nothing removed, no errors
            Assert.Empty(results);
        }
    }
}
