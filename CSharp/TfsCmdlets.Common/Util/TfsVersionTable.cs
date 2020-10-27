using System;
using System.Collections.Generic;
using System.Linq;

namespace TfsCmdlets.Util
{
    internal static class TfsVersionTable
    {
        internal static int GetYear(int majorVersion)
        {
            return _tfsMajorVersionTable[majorVersion];
        }

        internal static int GetMajorVersion(int year)
        {
            return _tfsMajorVersionTable.Where(kvp => kvp.Value == year).Select(kvp => kvp.Key).First();
        }

        internal static ServerVersion GetServerVersion(Version version)
        {
            if (!_tfsVersions.ContainsKey(version))
            {
                return new ServerVersion
                {
                    Version = version,
                    FriendlyVersion = $"{(version.Major >= 17 ? "Azure DevOps" : "Team Foundation")} Server {GetYear(version.Major)}",
                    IsHosted = false,
                    LongVersion = $"{version} (TFS {TfsVersionTable.GetYear(version.Major)}))"
                };
            }

            return _tfsVersions[version];
        }

        internal static bool IsYear(int year)
        {
            return _tfsMajorVersionTable.ContainsValue(year);
        }

        internal static bool IsMajorVersion(int majorVersion)
        {
            return _tfsMajorVersionTable.ContainsKey(majorVersion);
        }

        private static readonly Dictionary<int, int> _tfsMajorVersionTable = new Dictionary<int, int>
        {
            [8] = 2005,
            [9] = 2008,
            [10] = 2010,
            [11] = 2011,
            [12] = 2013,
            [14] = 2015,
            [15] = 2017,
            [16] = 2018,
            [17] = 2019,
            [18] = 2020
        };

        private static readonly Dictionary<Version, ServerVersion> _tfsVersions = new Dictionary<Version, ServerVersion>
        {
            // TFS 2005

            [new Version("8.0.50727.147")] = new ServerVersion()
            {
                Version = new Version("8.0.50727.147"),
                LongVersion = "8.0.50727.147 (TFS 2005 RTM)",
                FriendlyVersion = "Team Foundation Server 2005 (RTM)",
                Update = 0
            },
            [new Version("8.0.50727.762")] = new ServerVersion()
            {
                Version = new Version("8.0.50727.762"),
                LongVersion = "8.0.50727.762 (TFS 2005 SP1)",
                FriendlyVersion = "Team Foundation Server 2005 Service Pack 1",
                Update = 1
            },

            // TFS 2008

            [new Version("9.0.21022.8")] = new ServerVersion()
            {
                Version = new Version("9.0.21022.8"),
                LongVersion = "9.0.21022.8 (TFS 2008 RTM)",
                FriendlyVersion = "Team Foundation Server 2008 (RTM)",
                Update = 0
            },
            [new Version("9.0.30729.1")] = new ServerVersion()
            {
                Version = new Version("9.0.30729.1"),
                LongVersion = "9.0.30729.1 (TFS 2008 SP1)",
                FriendlyVersion = "Team Foundation Server 2008 Service Pack 1",
                Update = 1
            },

            // TFS 2010

            [new Version("10.0.30319.1")] = new ServerVersion()
            {
                Version = new Version("10.0.30319.1"),
                LongVersion = "10.0.30319.1 (TFS 2010 RTM)",
                FriendlyVersion = "Team Foundation Server 2010 (RTM)",
                Update = 0
            },
            [new Version("10.0.40219.1")] = new ServerVersion()
            {
                Version = new Version("10.0.40219.1"),
                LongVersion = "10.0.40219.1 (TFS 2010 SP1)",
                FriendlyVersion = "Team Foundation Server 2010 Service Pack 1",
                Update = 1
            },
            [new Version("10.0.40219.371")] = new ServerVersion()
            {
                Version = new Version("10.0.40219.371"),
                LongVersion = "10.0.40219.371 (TFS 2010 SP1 CU2)",
                FriendlyVersion = "Team Foundation Server 2010 Service Pack 1 Cumulative Update 2",
                Update = 1.2M
            },

            // TFS 2012

            [new Version("11.0.50727.1")] = new ServerVersion()
            {
                Version = new Version("11.0.50727.1"),
                LongVersion = "11.0.50727.1 (TFS 2012 RTM)",
                FriendlyVersion = "Team Foundation Server 2012 (RTM)",
                Update = 0
            },
            [new Version("11.0.51106.1")] = new ServerVersion()
            {
                Version = new Version("11.0.51106.1"),
                LongVersion = "11.0.51106.1 (TFS 2012.1)",
                FriendlyVersion = "Team Foundation Server 2012 Update 1",
                Update = 1
            },
            [new Version("11.0.60123.100")] = new ServerVersion()
            {
                Version = new Version("11.0.60123.100"),
                LongVersion = "11.0.60123.100 (TFS 2012.1.1)",
                FriendlyVersion = "Team Foundation Server 2012 Update 1 Cumulative Update 1",
                Update = 1.1M
            },
            [new Version("11.0.60315.1")] = new ServerVersion()
            {
                Version = new Version("11.0.60315.1"),
                LongVersion = "11.0.60315.1 (TFS 2012.2)",
                FriendlyVersion = "Team Foundation Server 2012 Update 2",
                Update = 2
            },
            [new Version("11.0.60610.1")] = new ServerVersion()
            {
                Version = new Version("11.0.60610.1"),
                LongVersion = "11.0.60610.1 (TFS 2012.3)",
                FriendlyVersion = "Team Foundation Server 2012 Update 3",
                Update = 3
            },
            [new Version("11.0.61030.0")] = new ServerVersion()
            {
                Version = new Version("11.0.61030.0"),
                LongVersion = "11.0.61030.0 (TFS 2012.4)",
                FriendlyVersion = "Team Foundation Server 2012 Update 4",
                Update = 4
            },

            // TFS 2013

            [new Version("12.0.20827.3")] = new ServerVersion()
            {
                Version = new Version("12.0.20827.3"),
                LongVersion = "12.0.20827.3 (TFS 2013 RC)",
                FriendlyVersion = "Team Foundation Server 2013 Release Candidate",
                Update = 0.9M
            },
            [new Version("12.0.21005.1")] = new ServerVersion()
            {
                Version = new Version("12.0.21005.1"),
                LongVersion = "12.0.21005.1 (TFS 2013 RTM)",
                FriendlyVersion = "Team Foundation Server 2013 (RTM)",
                Update = 0
            },
            [new Version("12.0.30324.0")] = new ServerVersion()
            {
                Version = new Version("12.0.30324.0"),
                LongVersion = "12.0.30324.0 (TFS 2013.2)",
                FriendlyVersion = "Team Foundation Server 2013 Update 2",
                Update = 2
            },
            [new Version("12.0.30626.0")] = new ServerVersion()
            {
                Version = new Version("12.0.30626.0"),
                LongVersion = "12.0.30626.0 (TFS 2013.3 RC)",
                FriendlyVersion = "Team Foundation Server 2013 Update 3 Release Candidate",
                Update = 2.9M
            },
            [new Version("12.0.30723.0")] = new ServerVersion()
            {
                Version = new Version("12.0.30723.0"),
                LongVersion = "12.0.30723.0 (TFS 2013.3)",
                FriendlyVersion = "Team Foundation Server 2013 Update 3",
                Update = 3
            },
            [new Version("12.0.31010.0")] = new ServerVersion()
            {
                Version = new Version("12.0.31010.0"),
                LongVersion = "12.0.31010.0 (TFS 2013.4 RC)",
                FriendlyVersion = "Team Foundation Server 2013 Update 4 Release Candidate",
                Update = 3.9M
            },
            [new Version("12.0.31101.0")] = new ServerVersion()
            {
                Version = new Version("12.0.31101.0"),
                LongVersion = "12.0.31101.0 (TFS 2013.4)",
                FriendlyVersion = "Team Foundation Server 2013 Update 4",
                Update = 4
            },
            [new Version("12.0.40629.0")] = new ServerVersion()
            {
                Version = new Version("12.0.40629.0"),
                LongVersion = "12.0.40629.0 (TFS 2013.5)",
                FriendlyVersion = "Team Foundation Server 2013 Update 5",
                Update = 5
            },

            // TFS 2015

            // TFS 2017

            // TFS 2018

            [new Version("16.121.26818.0")] = new ServerVersion()
            {
                Version = new Version("16.121.26818.0"),
                LongVersion = "16.121.26818.0 (TFS 2018 RC1)",
                FriendlyVersion = "Team Foundation Server 2018 Release Candidate 1",
                Update = 0.91M
            },
            [new Version("16.122.26918.3")] = new ServerVersion()
            {
                Version = new Version("16.122.26918.3"),
                LongVersion = "16.122.26918.3 (TFS 2018 RC2)",
                FriendlyVersion = "Team Foundation Server 2018 Release Candidate 2",
                Update = 0.92M
            },
            [new Version("16.122.27102.1")] = new ServerVersion()
            {
                Version = new Version("16.122.27102.1"),
                LongVersion = "16.122.27102.1 (TFS 2018 RTW)",
                FriendlyVersion = "Team Foundation Server 2018 (RTW)",
                Update = 0
            },

            // TFS (Azure DevOps Server) 2019

            // TFS (Azure DevOps Server) 2020
        };
    }

    /// <summary>
    /// Represents the version of a Team Foundation / Azure DevOps Server installation, and/or 
    /// the currently deployed version of Azure DevOps in an Azure DevOps Services organization
    /// </summary>
    public class ServerVersion
    {
        /// <summary>
        /// Gets the "four-part" version of TFS / Azure DevOps
        /// </summary>
        public Version Version { get; set; }

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
        public int YearVersion => TfsVersionTable.GetYear(Version.Major);
    }
}