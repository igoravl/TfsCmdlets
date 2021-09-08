using System.Management.Automation;
using System;
using System.Management.Automation.Runspaces;

namespace TfsCmdlets.Tests.UnitTests
{
    public class PowerShellFixture : IDisposable
    {
        private bool _IsDisposed;

        public PowerShell PowerShell {get; private set;}

        public PowerShellFixture()
        {
            PowerShell = PowerShell.Create();
            Runspace.DefaultRunspace = PowerShell.Runspace;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_IsDisposed)
            {
                if (disposing)
                {
                    PowerShell.Dispose();
                }

                Runspace.DefaultRunspace = null;
                PowerShell = null;
                _IsDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}