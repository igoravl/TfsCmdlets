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
        Child = 2,

        /// <summary>Related</summary>
        Related = 4,

        /// <summary>Predecessor</summary>
        Predecessor = 8,

        /// <summary>Successor</summary>
        Successor = 16,

        /// <summary>Duplicate</summary>
        Duplicate = 32,

        /// <summary>Duplicate Of</summary>
        DuplicateOf = 64,

        /// <summary>Tests</summary>
        Tests = 128,

        /// <summary>Tested By</summary>
        TestedBy = 256, 
    }

}
