using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.Admin
{
    /// <summary>
    ///   Gets the version information about Team Foundation / Azure DevOps servers and 
    ///   Azure DevOps Services organizations.
    /// </summary>
    /// <remarks>
    /// The Get-TfsVersion cmdlet retrieves version information from the supplied team project collection or Azure DevOps organization. 
    /// When available/applicable, detailed information about installed updates, deployed sprints and so on are also provided.
    /// </remarks>
    [Cmdlet(VerbsCommon.Get, "TfsVersion")]
    [OutputType(typeof(ServerVersion))]
    [TfsCmdlet(CmdletScope.Collection)]
    partial class GetVersion 
    {
    }
}
