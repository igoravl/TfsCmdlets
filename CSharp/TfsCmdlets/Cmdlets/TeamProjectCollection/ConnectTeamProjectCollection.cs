using System.Management.Automation;
using Microsoft.VisualStudio.Services.ClientNotification;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.TeamFoundation;
using TfsCmdlets.Models;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    /// <summary>
    /// Connects to a TFS team project collection or Azure DevOps organization. 
    /// </summary>
    /// <remarks>
    /// The Connect-TfsTeamProjectCollection cmdlet connects to a TFS Team Project Collection or 
    /// Azure DevOps organization.
    /// 
    /// That connection can be later reused by other TfsCmdlets commands until it's closed 
    /// by a call to Disconnect-TfsTeamProjectCollection.
    /// </remarks>
    /// <notes>
    /// Most cmdlets in the TfsCmdlets module require a Collection object to be provided via their 
    /// -Collection argument in order to access a TFS instance. Those cmdlets will use the connection 
    /// opened by Connect-TfsTeamProjectCollection as their "default connection".
    /// 
    /// In other words, TFS cmdlets (e.g. New-TfsWorkItem) that have a -Collection argument will use the connection 
    /// provided by Connect-TfsTeamProjectCollection by default.
    /// </notes>
    /// <example>
    ///   <code>Connect-TfsTeamProjectCollection -Collection http://tfs:8080/tfs/DefaultCollection</code>
    ///   <para>Connects to a collection called "DefaultCollection" in a TF server called "tfs" 
    ///         using the cached credentials of the logged-on user</para>
    /// </example>
    /// <example>
    ///   <code>Connect-TfsTeamProjectCollection -Collection http://tfs:8080/tfs/DefaultCollection -Interactive</code>
    ///   <para>Connects to a collection called "DefaultCollection" in a Team Foundation server called 
    ///         "tfs", firstly prompting the user for credentials (it ignores the cached credentials for 
    ///         the currently logged-in user). It's equivalent to the command: `Connect-TfsTeamProjectCollection 
    ///         -Collection http://tfs:8080/tfs/DefaultCollection -Credential (Get-TfsCredential -Interactive)`
    ///   </para>
    /// </example>
    [TfsCmdlet(CmdletScope.Server, DefaultParameterSetName = "Prompt for credential", OutputType = typeof(VssConnection))]
    partial class ConnectTeamProjectCollection
    {
        /// <summary>
        ///  Specifies the URL to the Team Project Collection or Azure DevOps Organization to connect to, 
        ///  a TfsTeamProjectCollection object (Windows PowerShell only), or a VssConnection object. 
        ///  You can also connect to an Azure DevOps Services organizations by simply providing its name 
        ///  instead of the full URL. 
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [Alias("Organization")]
        [ValidateNotNull]
        public object Collection { get; set; }
    }

    [CmdletController(typeof(Connection))]
    partial class ConnectTeamProjectCollectionController
    {
        [Import]
        private ICurrentConnections CurrentConnections { get; }

        protected override IEnumerable Run()
        {
            var tpc = Data.GetCollection(new { Collection = Collection ?? Parameters.Get<object>("Organization") });

            try
            {
                tpc.Connect();
            }
            catch (TeamFoundationServerUnauthorizedException ex)
            {
                if (Has_PersonalAccessToken)
                {
                    throw new TeamFoundationServerUnauthorizedException(
                        $"You are not authorized to access {tpc.Uri}. Your personal access token may have expired.", ex);
                }
                else
                {
                    throw;
                }
            }

            if (tpc.CurrentUserUniqueName.Equals("Anonymous"))
            {
                var connectionType = Collection == null ? "organization" : "team project collection";
                throw new NotAuthorizedException($"You are not authorized to access {connectionType} [{tpc.Uri}]. Check your credentials and try again.");
            }

            var srv = tpc.ConfigurationServer;
            CurrentConnections.Set(srv, tpc);

            Logger.Log($"Connected to {tpc.Uri}, ID {tpc.ServerId}, as '{tpc.CurrentUserDisplayName}'");

            yield return tpc;
        }
    }
}