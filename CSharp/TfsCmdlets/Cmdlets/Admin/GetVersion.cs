using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Admin
{
    [Cmdlet(VerbsCommon.Get, "Version")]
    [OutputType(typeof(PSCustomObject))]
    public class GetVersion : PSCmdlet
    {
        [Parameter(ValueFromPipeline = true)]
        public object Collection { get; set; }
        /*
        protected override void ProcessRecord()
        {
            var tpc = this.GetCollection();

            if (tpc.IsHostedServer)
            {
                _Log "Collection is hosted (Azure DevOps Services)"
    
            html = (Invoke - TfsRestApi - Path "/" - Collection tpc)

            if (!(html - match ""serviceVersion":"(.+?) \((.+?)\)""))
            {
                    _Log "Response does not contain "serviceVersion" information"

                throw new Exception("Azure DevOps Services version not found in response.")
            }

                version = [version]($"{{Matches}[1] -replace "[a - zA - Z]", ""}.0")
                longVersion = $"{{Matches}[1]} ($(Matches[2]))"
                sprint = version.Minor
                friendlyVersion = $"Azure DevOps Services, Sprint {sprint} ({Matches[2]})"
    
        }
            else
            {
                _Log "Collection is not hosted (Azure DevOps Server / TFS)"
                throw new Exception("Unsupported server version")
            }

            WriteObject([PSCustomObject][Ordered] @{); return;
                Version = version
                LongVersion = longVersion
                FriendlyVersion = friendlyVersion
                IsHosted = tpc.IsHostedServer
                Sprint = sprint
                Update = update
            }
        }
    }
*/
}
}
