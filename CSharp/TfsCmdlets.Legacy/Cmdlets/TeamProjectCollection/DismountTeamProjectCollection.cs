using System;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    /// <summary>
    /// Detaches a team project collection database from a Team Foundation Server installation.
    /// </summary>
    /// <remarks>
    /// Before you move a collection, you must first detach it from the deployment of TFS on which 
    /// it is running. It's very important that you do not skip this step. When you detach a collection, 
    /// all jobs and services are stopped, and then the collection database is stopped. In addition, 
    /// the detach process copies over the collection-specific data from the configuration database 
    /// and saves it as part of the team project collection database. This configuration data is what 
    /// allows the collection database to be attached to a different deployment of TFS. If that data is 
    /// not present, you cannot attach the collection to any deployment of TFS except the one from which 
    /// it originated. If detachment succeeds, this cmdlets returns the original database connection string. 
    /// It is required to re-attach the collection to TFS.
    /// </remarks>
    /// <example>
    ///   <code>Dismount-TfsTeamProjectCollection -Collection http://vsalm:8080/tfs/DefaultCollection -Reason "Collection DefaultCollecton is down for maintenance"</code>
    ///   <para>Detaches the project collection specified by the URL provided in the Collection argument, defining a Maintenance Message to be shown to users when they try to connect to that collection while it is detached</para>
    /// </example>
    /// <related>https://www.visualstudio.com/en-us/docs/setup-admin/tfs/admin/move-project-collection#1-detach-the-collection</related>
    /// <notes>
    /// Detaching a collection prevents users from accessing any projects in that collection.
    /// </notes>
    [TfsCmdlet(CmdletScope.Server, DesktopOnly = true, SupportsShouldProcess = true, OutputType = typeof(string))]
    partial class DismountTeamProjectCollection
    {
        /// <summary>
        /// Specifies the collection to detach.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public object Collection { get; set; }

        /// <summary>
        /// Specifies a Servicing Message (optional), to provide a message for users who might try 
        /// to connect to projects in this collection while it is offline.
        /// </summary>
        [Parameter]
        public string Reason { get; set; }

        /// <summary>
        /// Specifies the maximum period of time this cmdlet should wait for the detach procedure 
        /// to complete. By default, it waits indefinitely until the collection servicing completes.
        /// </summary>
        [Parameter]
        public TimeSpan Timeout { get; set; } = TimeSpan.MaxValue;
    }
}