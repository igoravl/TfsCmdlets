using System;
using System.Composition;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(ILogger)), Shared]
    internal class LoggerImpl : ILogger
    {
        private IPowerShellService PowerShell { get; set; }


        public void Log(string message)
        {
            if(!PowerShell.IsVerbose) return;
            
            PowerShell.WriteVerbose(message);
        }

        public void LogError(string message)
        {
            throw new NotImplementedException();
        }

        public void LogError(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void LogWarn(string message)
        {
            PowerShell.WriteWarning(message);
        }

        [ImportingConstructor]
        public LoggerImpl(IPowerShellService powerShell)
        {
            PowerShell = powerShell;
        }
     }
}
