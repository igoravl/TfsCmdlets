/*
.SYNOPSIS
    Gets one or more Team Project Collection addresses registered in the current computer.

.PARAMETER Name
    Specifies the name of a registered collection. When omitted, all registered collections are returned. Wildcards are permitted.

.INPUTS
    System.String
*/

using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    [Cmdlet(VerbsCommon.Get, "RegisteredTeamProjectCollection")]
    //[OutputType(typeof(Microsoft.TeamFoundation.Client.RegisteredProjectCollection[]))]
    public class GetRegisteredTeamProjectCollection: PSCmdlet
    {
/*
        [Parameter(Position=0, ValueFromPipeline=true)]
        [Alias("Name")]
        [SupportsWildcards()]
        public string Collection { get; set; } = "*"

    protected override void ProcessRecord()
    {
        registeredCollections = [Microsoft.TeamFoundation.Client.RegisteredTfsConnections]::GetProjectCollections() 
        
        foreach(tpc in registeredCollections)
        {
            tpcName = ([Uri]tpc.Uri).Segments[-1]

            if(tpcName -like Collection)
            {
                Write-Output tpc
            }
        }
    }
}
*/
}
}
