using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Identity.Group
{
    [Cmdlet(VerbsCommon.Get, "TfsGroupMember")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Identity.Identity))]
    public class GetGroupMember: BaseCmdlet
    {
/*
        # Specifies the board name(s). Wildcards accepted
        [Parameter(Position=0)]
        [SupportsWildcards()]
        public object Identity { get; set; } = "*";

        [Parameter(ValueFromPipeline=true)]
        public object Group { get; set; }

        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
    {
        tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})

        this.Log($"Returning members from group "{Group}"");

        gi = Get-TfsIdentity -Identity Group -Collection tpc -QueryMembership

        if(! gi)
        {
            throw new Exception($"Invalid or non-existent group "{Group}"")
        }

        foreach(mid in gi.MemberIds)
        {
            i = Get-TfsIdentity -Identity mid -Collection Collection

            if ((i.DisplayName -like Identity) || (i.Properties["Account"] -like Identity))
            {
                Write-Output i
            }
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
