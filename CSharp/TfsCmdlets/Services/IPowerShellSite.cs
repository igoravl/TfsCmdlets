using System.Collections.Generic;
using System.Management.Automation;
using TfsCmdlets.Cmdlets;
using TfsCmdlets.Extensions;
using TfsCmdlets.Services;

namespace TfsCmdlets.Services
{
    public interface IPowerShellSite
    {
        void SetCmdlet(PSCmdlet cmdlet);
    }
}