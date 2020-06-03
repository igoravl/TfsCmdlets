using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.Team.TeamAdmin
{
    [Cmdlet(VerbsCommon.Get, "TfsTeamAdmin")]
    [OutputType(typeof(Microsoft.VisualStudio.Services.Identity.Identity))]
    public class GetTeamAdmin: BaseCmdlet
    {
/*
        # Specifies the board name(s). Wildcards accepted
        [Parameter(Position=0)]
        [SupportsWildcards()]
        public object Identity { get; set; } = "*";

        [Parameter(ValueFromPipeline=true)]
        public object Team { get; set; }

        [Parameter()]
        public object Project { get; set; }

        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
    {
        if(Team is Microsoft.TeamFoundation.Core.WebApi.WebApiTeam)
        {
            Project = Team.ProjectId
        }

        t = Get-TfsTeam -Team Team -Project Project -Collection Collection -IncludeMembers

        tpc = Get-TfsTeamProjectCollection -Collection Collection; if (! tpc || (tpc.Count != 1)) {throw new Exception($"Invalid or non-existent team project collection {Collection}."})

        this.Log($"Returning team admins from team "{{t}.Name}"");

        foreach(member in t.Members)
        {
            if(! member.IsTeamAdmin)
            {
            continue
            }

            i = Get-TfsIdentity -Identity member.Identity.Id -Collection Collection

            if ((i.DisplayName -like Identity) || (i.Properties["Account"] -like Identity))
            {
                Write-Output i | `
                    Add-Member -Name TeamId -MemberType NoteProperty -Value t.Id -PassThru | `
                    Add-Member -Name ProjectId -MemberType NoteProperty -Value t.ProjectId -PassThru
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
