using System.Composition;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets.Commands.Shell
{
    [Command]
    internal class EnterShell : CommandBase
    {
        private IPowerShellService PowerShell { get; }

        public override object InvokeCommand(ParameterDictionary parameters)
        {
            PowerShell.WriteWarning("Entering interactive shell");

            return null;
        }

        [ImportingConstructor]
        public EnterShell(IPowerShellService powerShell, ILogger logger)
        : base(logger)
        {
            PowerShell = powerShell;
        }
    }
}