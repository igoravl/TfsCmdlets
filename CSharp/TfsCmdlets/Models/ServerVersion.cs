using System;
using TfsCmdlets.Util;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Represents the version of a Team Foundation / Azure DevOps Server installation, and/or 
    /// the currently deployed version of Azure DevOps in an Azure DevOps Services organization
    /// </summary>
    public class ServerVersion
    {
        public ServerVersion(string version)
        : this(Version.Parse(version))
        {
        }

        public ServerVersion(string version, string longVersionMoniker, int year)
        : this(Version.Parse(version))
        {
            LongVersion = $"{version} ({longVersionMoniker})";
            YearVersion = year;
        }

        public ServerVersion(Version version)
        {
            Version = version;
        }

        /// <summary>
        /// Gets the "four-part" version of TFS / Azure DevOps
        /// </summary>
        public System.Version Version { get; set; }

        /// <summary>
        /// Gets the "long" version of TFS / Azure DevOps
        /// </summary>
        public string LongVersion { get; set; }

        /// <summary>
        /// Gets the "friendly" version of TFS / Azure DevOps
        /// </summary>
        public string FriendlyVersion { get; set; }

        /// <summary>
        /// Indicates whether it's a "hosted" (Azure DevOps Services) deployment or not 
        /// (TFS/Azure DevOps Server)
        /// </summary>
        public bool IsHosted { get; set; }

        /// <summary>
        /// Gets the version number of the Update installed on a server, or number of the sprint 
        /// currently deployed in an Azure DevOps Services organization
        /// </summary>
        public decimal Update { get; set; }

        /// <summary>
        /// Gets the version of the server as its corresponding year (e.g. 2019 for version 17.*)
        /// </summary>
        public int YearVersion { get; set; }
    }
}