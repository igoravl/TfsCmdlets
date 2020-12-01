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
}
