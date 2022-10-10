namespace TfsCmdlets.Cmdlets.Organization
{
    /// <summary>
    /// Gets one of more team project collections (organizations in Azure DevOps).
    /// </summary>
    [TfsCmdlet(CmdletScope.Server, DefaultParameterSetName = "Get by organization", CustomControllerName = "GetTeamProjectCollection", 
#if NETCOREAPP3_1_OR_GREATER    
        OutputType = typeof(Microsoft.VisualStudio.Services.WebApi.VssConnection))]
#else
        OutputType = typeof(Microsoft.TeamFoundation.Client.TfsTeamProjectCollection))]
#endif
    partial class GetOrganization
    {
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Get by organization")]
        [Parameter(Position = 0, ParameterSetName = "Cached credentials")]
        [Parameter(Position = 0, ParameterSetName = "User name and password")]
        [Parameter(Position = 0, ParameterSetName = "Credential object")]
        [Parameter(Position = 0, ParameterSetName = "Personal Access Token")]
        [Parameter(Position = 0, ParameterSetName = "Prompt for credential")]
        [Alias("Collection")]
        [SupportsWildcards]
        public object Organization { get; set; }

        /// <summary>
        /// Returns the organization specified in the last call to 
        /// Connect-TfsOrganization (i.e. the "current" organization)
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Get current")]
        public SwitchParameter Current { get; set; }
    }
}