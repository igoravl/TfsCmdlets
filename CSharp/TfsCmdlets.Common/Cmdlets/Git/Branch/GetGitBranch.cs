/*
.SYNOPSIS
    Gets information from one or more branches in a Git repository.

.PARAMETER Project
    Specifies either the name of the Team Project or a previously initialized Microsoft.TeamFoundation.WorkItemTracking.Client.Project object to connect to. If omitted, it defaults to the connection opened by Connect-TfsTeamProject (if any). 

For more details, see the Get-TfsTeamProject cmdlet.

.PARAMETER Collection
    Specifies either a URL/name of the Team Project Collection to connect to, or a previously initialized TfsTeamProjectCollection object. 

When using a URL, it must be fully qualified. The format of this string is as follows: 

http[s]://<ComputerName>:<Port>/[<TFS-vDir>/]<CollectionName> 

Valid values for the Transport segment of the URI are HTTP and HTTPS. If you specify a connection URI with a Transport segment, but do not specify a port, the session is created with standards ports: 80 for HTTP and 443 for HTTPS. 

To connect to a Team Project Collection by using its name, a TfsConfigurationServer object must be supplied either via -Server argument or via a previous call to the Connect-TfsConfigurationServer cmdlet. 

For more details, see the Get-TfsTeamProjectCollection cmdlet.

.INPUTS
    Microsoft.TeamFoundation.WorkItemTracking.Client.Project
    System.String
*/

using System.Management.Automation;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace TfsCmdlets.Cmdlets.Git.Branch
{
    [Cmdlet(VerbsCommon.Get, "GitBranch")]
    [OutputType(typeof(GitBranchStats))]
    public class GetGitBranch: BaseCmdlet
    {
/*
        [Parameter()]
        [Alias("RefName")]
        [SupportsWildcards()]
        public object Branch = "*";

        [Parameter(ValueFromPipeline=true)]
        [SupportsWildcards()]
        public object Repository,

        [Parameter()]
        public object Project { get; set; }

        [Parameter()]
        public object Collection { get; set; }

    protected override void BeginProcessing()
    {
        #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.Policy.WebApi"
    }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
    {
        if((! Repository) && Project)
        {
            tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)
            Repository = tp.Name
        }

        repos = Get-TfsGitRepository -Repository Repository -Project Project -Collection Collection

        tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})

        var client = tpc.GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();
        
        foreach(repo in repos)
        {
            if(repo.Size == 0)
            {
                Write-Verbose $"Repository {{repo}.Name} is empty. Skipping."
                continue
            }

            task = client.GetBranchesAsync(tp.Name,repo.Id); result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error retrieving branches from repository "{{repo}.Name}"" task.Exception.InnerExceptions })

            Write-Output result | Where-Object name -Like Branch | `
                Add-Member -Name  "Project" -MemberType NoteProperty -Value repo.ProjectReference.Name -PassThru | `
                Add-Member -Name  "Repository" -MemberType NoteProperty -Value repo.Name -PassThru | `
                Sort-Object Project, Repository
        }
    }
}
*/
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();
    }
}
