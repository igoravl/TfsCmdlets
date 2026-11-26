using System;
using System.IO;
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
            var tempRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
            var fileSystem = CreateFileSystem(tempRoot);
            _parms.HasParameter("Target").Returns(true);
            _parms.Get<ShellTarget>("Target", Arg.Any<ShellTarget>())
                .Returns(ShellTarget.All);

            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            var controller = new UninstallShellController(
                _powerShell, _data, _parms, _logger, fileSystem);

            // Act — run against a filesystem substitute that simulates installed shell files
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert — no exceptions, ran to completion
            Assert.Empty(results);
            AssertDeletedFilesAreWithin(fileSystem, tempRoot, 6);
        }

        [Fact]
        public void WhatIf_Does_Not_Remove_Shortcuts()
        {
            // Arrange
            var tempRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
            var fileSystem = CreateFileSystem(tempRoot);
            _parms.HasParameter("Target").Returns(true);
            _parms.Get<ShellTarget>("Target", Arg.Any<ShellTarget>())
                .Returns(ShellTarget.All);

            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(false);

            var controller = new UninstallShellController(
                _powerShell, _data, _parms, _logger, fileSystem);

            // Act
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert
            Assert.Empty(results);
            fileSystem.DidNotReceive().DeleteFile(Arg.Any<string>());
        }

        [Fact]
        public void Selective_Target_WindowsTerminal_Only()
        {
            // Arrange
            var tempRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
            var fileSystem = CreateFileSystem(tempRoot);
            _parms.HasParameter("Target").Returns(true);
            _parms.Get<ShellTarget>("Target", Arg.Any<ShellTarget>())
                .Returns(ShellTarget.WindowsTerminal);

            _powerShell.ShouldProcess(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            var controller = new UninstallShellController(
                _powerShell, _data, _parms, _logger, fileSystem);

            // Act
            var results = controller.InvokeCommand().Cast<object>().ToList();

            // Assert — only mocked Windows Terminal fragment paths are removed
            Assert.Empty(results);
            AssertDeletedFilesAreWithin(fileSystem, tempRoot, 2);
        }

        private static IUninstallShellFileSystem CreateFileSystem(string tempRoot)
        {
            var fileSystem = Substitute.For<IUninstallShellFileSystem>();
            fileSystem.IsWindows.Returns(true);
            fileSystem.GetFolderPath(Arg.Any<Environment.SpecialFolder>())
                .Returns(call => Path.Combine(tempRoot, call.Arg<Environment.SpecialFolder>().ToString()));
            fileSystem.FileExists(Arg.Any<string>()).Returns(true);
            fileSystem.DirectoryExists(Arg.Any<string>()).Returns(true);
            fileSystem.GetFileSystemEntries(Arg.Any<string>()).Returns(new[] { "existing-entry" });
            fileSystem.GetFiles(Arg.Any<string>(), Arg.Any<string>())
                .Returns(call => new[]
                {
                    Path.Combine(call.ArgAt<string>(0), "AzureDevOpsShell-test.json")
                });
            return fileSystem;
        }

        private static void AssertDeletedFilesAreWithin(
            IUninstallShellFileSystem fileSystem,
            string tempRoot,
            int expectedCount)
        {
            var deletedPaths = fileSystem.ReceivedCalls()
                .Where(call => call.GetMethodInfo().Name == nameof(IUninstallShellFileSystem.DeleteFile))
                .Select(call => (string)call.GetArguments()[0])
                .ToArray();

            Assert.Equal(expectedCount, deletedPaths.Length);
            Assert.All(deletedPaths, path =>
                Assert.StartsWith(tempRoot, path, StringComparison.OrdinalIgnoreCase));
            fileSystem.DidNotReceive().DeleteDirectory(Arg.Any<string>());
        }
    }
}
