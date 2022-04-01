namespace TfsCmdlets.Cmdlets.Artifact
{
    /// <summary>
    /// Gets information from one or more artifact feeds.
    /// </summary>
    [TfsCmdlet(CmdletScope.Project, OutputType = typeof(WebApiPackage))]
    partial class GetArtifact
    {
        /// <summary>
        /// Specifies the package (artifact) name. Wildcards are supported. 
        /// When omitted, returns all packages in the specified feed.
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty]
        [Alias("Package")]
        public object Artifact { get; set; } = "*";

        /// <summary>
        /// Specifies the feed name. 
        /// </summary>
        [Parameter(ValueFromPipeline = true, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public object Feed { get; set; }

        /// <summary>
        /// Includes deletes packages in the result. 
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeDeleted { get; set; }

        /// <summary>
        /// Includes the package description in the results. 
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeDescription { get; set; }

        /// <summary>
        /// Includes prerelease packages in the results. Applies only to Nuget packages.
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludePrerelease { get; set; }

        /// <summary>
        /// Includes delisted packages in the results. Applies only to Nuget packages.
        /// </summary>
        [Parameter()]
        public SwitchParameter IncludeDelisted { get; set; }

        /// <summary>
        /// Returns only packages of the specified protocol type.
        /// </summary>
        [Parameter()]
        public string ProtocolType { get; set; }
    }
}