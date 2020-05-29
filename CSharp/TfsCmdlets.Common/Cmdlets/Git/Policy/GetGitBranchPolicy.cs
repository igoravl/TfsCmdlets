/*
.SYNOPSIS
    Gets information from one or more Git repositories in a team project.

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
using Microsoft.TeamFoundation.Policy.WebApi;

namespace TfsCmdlets.Cmdlets.Git.Policy
{
    [Cmdlet(VerbsCommon.Get, "GitBranchPolicy")]
    [OutputType(typeof(PolicyConfiguration))]
    public class GetGitBranchPolicy: BaseCmdlet
    {
/*
        [Parameter(Position=0, ValueFromPipeline=true)]
        [SupportsWildcards()]
        public object Repository = "*";

        [Parameter()]
        [Alias("RefName")]
        [AllowNull()]
        public object Branch = "master",

        [Parameter()]
        public object PolicyType,

        [Parameter()]
        public object Project { get; set; }

        [Parameter()]
        public object Collection { get; set; }

    protected override void BeginProcessing()
    {
        #_ImportRequiredAssembly -AssemblyName "Microsoft.TeamFoundation.Policy.WebApi"
    }

    protected override void ProcessRecord()
    {
        if(Repository.ProjectReference.Name) {Project = Repository.ProjectReference.Name}; tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        var client = tpc.GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();
        
        if(Branch && (Branch -notlike "refs/*"))
        {
            Branch = $"refs/heads/{Branch}"
        }

        policyTypeId = null

        if(PolicyType)
        {
            policy = Get-TfsPolicyType -Type PolicyType -Project tp -Collection tpc

            if(! policy)
            {
                throw new Exception($"Invalid or non-existent policy type "{PolicyType}"")
            }
            
            policyTypeId = PolicyType.Id
        }

        repos = Get-TfsGitRepository -Repository Repository -Project tp -Collection tpc

        foreach(repo in repos)
        {
            task = client.GetPolicyConfigurationsAsync(tp.Name, repo.Id, Branch, policyTypeId); result = task.Result; if(task.IsFaulted) { _throw new Exception($"Error retrieving branch policy configurations for repository "{{repo}.Name}"" task.Exception.InnerExceptions })
        }
        
        WriteObject(result.PolicyConfigurations); return;
    }
}
*/
    protected override void EndProcessing() => throw new System.NotImplementedException();
    }
}
