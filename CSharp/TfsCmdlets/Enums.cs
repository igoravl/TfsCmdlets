using System;

namespace TfsCmdlets
{
    /// <summary>
    /// Indicates a TFS component
    /// </summary>
    public enum TfsComponent
    {
        /// <summary>
        /// The root folder of a TFS installation
        /// </summary>
        BaseInstallation,

        /// <summary>
        /// The "ApplicationTier" folder of a TFS installation
        /// </summary>
        ApplicationTier,

        /// <summary>
        /// The "SharePointExtensions" folder of a TFS installation
        /// </summary>
        SharePointExtensions,

        /// <summary>
        /// The "TeamBuild" folder of a TFS installation
        /// </summary>
        TeamBuild,

        /// <summary>
        /// The "Tools" folder of a TFS installation
        /// </summary>
        Tools,

        /// <summary>
        /// The "VersionControlProxy" folder of a TFS installation
        /// </summary>
        VersionControlProxy
    }

    /// <summary>
    /// Client scope for Invoke-RestApi
    /// </summary>
    public enum ClientScope
    {
        /// <summary>
        /// Server-level scope
        /// </summary>
        Server,

        /// <summary>
        /// Collection-level scope
        /// </summary>
        Collection
    }

    /// <summary>
    /// Board card rule
    /// </summary>
    [Flags]
    public enum CardRuleType
    {
        /// <summary>
        /// Card color rule type
        /// </summary>
        CardColor = 1,

        /// <summary>
        /// Tag color rule type
        /// </summary>
        TagColor = 2,

        /// <summary>
        /// All card rules (card and tag color)
        /// </summary>
        All = 3
    }

    /// <summary>
    /// TFS Registry scope
    /// </summary>
    public enum RegistryScope
    {
        /// <summary>
        /// User
        /// </summary>
        User,

        /// <summary>
        /// Collection
        /// </summary>
        Collection,

        /// <summary>
        /// Server
        /// </summary>
        Server
    }

    /// <summary>
    /// Work Item Link Type
    /// </summary>
    [Flags]
    public enum WorkItemLinkType
    {
        /// <summary>All</summary>
        All = 0,

        /// <summary>Parent</summary>
        Parent = 1,

        /// <summary>Child</summary>
        Child = 1 << 1,

        /// <summary>Related</summary>
        Related = 1 << 2,

        /// <summary>Predecessor</summary>
        Predecessor = 1 << 3,

        /// <summary>Successor</summary>
        Successor = 1 << 4,

        /// <summary>Duplicate</summary>
        Duplicate = 1 << 5,

        /// <summary>Duplicate Of</summary>
        DuplicateOf = 1 << 6,

        /// <summary>Tests</summary>
        Tests = 1 << 7,

        /// <summary>Tested By</summary>
        TestedBy = 1 << 8,

        /// <summary>Test Case</summary>
        TestCase = 1 << 9,

        /// <summary>Shared Steps</summary>
        SharedSteps = 1 << 10,

        /// <summary>Shared Step</summary>
        References = 1 << 11,

        /// <summary>Shared Step</summary>
        ReferencedBy = 1 << 12,

        /// <summary>Produces For</summary>
        ProducesFor = 1 << 13,

        /// <summary>Consumes From</summary>
        ConsumesFrom = 1 << 14,

        /// <summary>Remote Related</summary>
        RemoteRelated = 1 << 15,

        /// <summary>Attached File</summary>
        AttachedFile = 1 << 29,

        /// <summary>Hyperlink</summary>
        Hyperlink = 1 << 30,

        /// <summary>Artifact link</summary>
        ArtifactLink = 1 << 31,
    }

}
