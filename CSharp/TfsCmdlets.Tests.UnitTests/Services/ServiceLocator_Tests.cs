using System.Composition;
using System.Composition.Hosting;
using TfsCmdlets.Services;
using Xunit;

namespace TfsCmdlets.Tests.UnitTests.Services
{
    public class ServiceLocator_Tests
    {
        [Fact]
        public void Can_Register_Part()
        {
            var container = new ContainerConfiguration()
                .WithPart<LoggerSample>()
                .CreateContainer();

            Assert.True(container.TryGetExport(typeof(ILogger), out var export));
            Assert.IsAssignableFrom<LoggerSample>(export);
        }

        [Fact]
        public void Can_Register_Assembly()
        {
            var container = new ContainerConfiguration()
                .WithAssembly(GetType().Assembly)
                .CreateContainer(); 

            Assert.True(container.TryGetExport(typeof(ILogger), out var export));
            Assert.IsAssignableFrom<LoggerSample>(export);
        }

        [Fact]
        public void Can_Import()
        {
            var container = new ContainerConfiguration()
                .WithAssembly(GetType().Assembly)
                .CreateContainer();

            var cmdlet = new CmdletSample1();

            container.SatisfyImports(cmdlet);

            Assert.IsAssignableFrom<LoggerSample>(cmdlet.GetLogger());
        }

        [Fact]
        public void Can_Import_Many()
        {
            var container = new ContainerConfiguration()
                .WithAssembly(GetType().Assembly)
                .CreateContainer();

            var cmdlet = new CmdletSample2();

            container.SatisfyImports(cmdlet);

            Assert.IsAssignableFrom<LoggerSample>(cmdlet.Logger);
            Assert.Collection(cmdlet.Commands,
                i => Assert.IsAssignableFrom<CommandSample1>(i),
                i => Assert.IsAssignableFrom<CommandSample2>(i),
                i => Assert.IsAssignableFrom<CommandSample3>(i));
        }


        [Fact]
        public void Can_Import_Nested()
        {
            var container = new ContainerConfiguration()
                .WithAssembly(GetType().Assembly)
                .CreateContainer();

            var cmdlet = new CmdletSample3();

            container.SatisfyImports(cmdlet); 

            Assert.IsAssignableFrom<PowerShellSample>(cmdlet.PowerShellService);
            Assert.IsAssignableFrom<LoggerSample>(((PowerShellSample) cmdlet.PowerShellService).Logger);
        }
    }
}