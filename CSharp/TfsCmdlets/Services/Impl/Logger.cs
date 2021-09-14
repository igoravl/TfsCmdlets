using System;

namespace TfsCmdlets.Services.Impl
{
    [Exports(typeof(ILogger))]
    internal class Logger : ILogger
    {
        private IPowerShellService PowerShell { get; set; }

        public Logger(IPowerShellService powerShell)
        {
            PowerShell = powerShell;
        }

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
    }
}
