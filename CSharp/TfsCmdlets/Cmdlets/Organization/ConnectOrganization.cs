// using Microsoft.VisualStudio.Services.WebApi;

// namespace TfsCmdlets.Cmdlets.Organization
// {
//     /// <summary>
//     /// Connects to an Azure DevOps organization. 
//     /// </summary>
//     /// <remarks>
//     /// The Connect-TfsOrganization cmdlet connects to an Azure DevOps organization.
//     /// 
//     /// That connection can be later reused by other TfsCmdlets commands until it's closed 
//     /// by a call to Disconnect-TfsOrganization or Disconnect-TfsTeamProjectCollection.
//     /// </remarks>
//     /// <notes>
//     /// Most cmdlets in the TfsCmdlets module require a Collection object to be provided via their 
//     /// -Collection argument in order to access a TFS instance. Those cmdlets can use the connection 
//     /// opened by Connect-TfsOrganization as their "default connection".
//     /// 
//     /// In other words, TFS cmdlets (e.g. New-TfsWorkItem) that have a -Collection argument will use the connection 
//     /// provided by Connect-TfsOrganization by default.
//     /// </notes>
//     /// <example>
//     ///   <code>Connect-TfsOrganization -Organization fabrikamfiber</code>
//     ///   <para>Connects to an organization called "fabrikamfiber" (https://dev.azure.com/fabrikamfiber)
//     ///         using the cached credentials of the logged-on user</para>
//     /// </example>
//     /// <example>
//     ///   <code>Connect-TfsOrganization -Organization fabrikamfiber -Interactive</code>
//     ///   <para>Connects to an organization called "fabrikamfiber" (https://dev.azure.com/fabrikamfiber),
//     ///         firstly prompting the user for credentials (it ignores the cached credentials for 
//     ///         the currently logged-in user).
//     ///   </para>
//     /// </example>
//     [TfsCmdlet(CmdletScope.Server, DefaultParameterSetName = "Prompt for credential", OutputType = typeof(VssConnection))]
//     partial class ConnectOrganization
//     {
//         /// <summary>
//         ///  Specifies the name (or URL) of the Azure DevOps Services organization to connect to.
//         /// </summary>
//         [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
//         [ValidateNotNull]
//         public object Organization { get; set; }
//     }
// }