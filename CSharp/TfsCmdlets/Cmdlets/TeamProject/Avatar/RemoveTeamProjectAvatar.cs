namespace TfsCmdlets.Cmdlets.TeamProject.Avatar
{
    /// <summary>
    /// Removes the team project avatar, resetting it to the default.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, SupportsShouldProcess = true)]
    partial class RemoveTeamProjectAvatar
    {
        /// <summary>
        /// HELP_PARAM_PROJECT
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public object Project { get; set; }
    }
}