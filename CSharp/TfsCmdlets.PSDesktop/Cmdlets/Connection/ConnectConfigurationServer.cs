using System.Management.Automation;
using Microsoft.TeamFoundation.Client;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Extensions;

namespace TfsCmdlets.Cmdlets.Connection
{
    /// <summary>
    ///  Connects to a configuration server.
    /// </summary>
    [OutputType(typeof(TfsConfigurationServer))]
    partial class ConnectConfigurationServer: BaseCmdlet
    {
        partial void DoProcessRecord()
        {
            var srv = this.GetServer();
            srv.Connect();

            CurrentConnections.Set(srv);

            this.Log($"Connected to {srv.Uri}, ID {srv.ServerId}, as {srv.AuthorizedIdentity.DisplayName}");

            if (Passthru)
            {
                WriteObject(srv);
            }
        }
	}
}
