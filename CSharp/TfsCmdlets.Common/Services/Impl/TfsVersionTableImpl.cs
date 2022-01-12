using TfsCmdlets.Models;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(ITfsVersionTable)), Shared]
    public class TfsVersionTableImpl : ITfsVersionTable
    {
        public int GetYear(int majorVersion)
        {
            return _tfsMajorVersionTable[majorVersion];
        }

        public int GetMajorVersion(int year)
        {
            return _tfsMajorVersionTable.Where(kvp => kvp.Value == year).Select(kvp => kvp.Key).First();
        }

        public ServerVersion GetServerVersion(Version version)
        {
            var versionString = version.ToString();
            int year = GetYear(version.Major);

            return versionString switch
            {
                // TFS 2005

                "8.0.50727.147" => new ServerVersion(versionString, "TFS 2005 RTM", year)
                {
                    FriendlyVersion = "Team Foundation Server 2005 (RTM)",
                    Update = 0
                },
                "8.0.50727.762" => new ServerVersion(versionString, "TFS 2005 SP1", year)
                {
                    FriendlyVersion = "Team Foundation Server 2005 Service Pack 1",
                    Update = 1
                },

                // TFS 2008

                "9.0.21022.8" => new ServerVersion(versionString, "TFS 2008 RTM", year)
                {
                    FriendlyVersion = "Team Foundation Server 2008 (RTM)",
                    Update = 0
                },
                "9.0.30729.1" => new ServerVersion(versionString, "TFS 2008 SP1", year)
                {
                    FriendlyVersion = "Team Foundation Server 2008 Service Pack 1",
                    Update = 1
                },

                // TFS 2010

                "10.0.30319.1" => new ServerVersion(versionString, "TFS 2010 RTM", year)
                {
                    FriendlyVersion = "Team Foundation Server 2010 (RTM)",
                    Update = 0
                },
                "10.0.40219.1" => new ServerVersion(versionString, "TFS 2010 SP1", year)
                {
                    FriendlyVersion = "Team Foundation Server 2010 Service Pack 1",
                    Update = 1
                },
                "10.0.40219.371" => new ServerVersion(versionString, "TFS 2010 SP1 CU2", year)
                {
                    FriendlyVersion = "Team Foundation Server 2010 Service Pack 1 Cumulative Update 2",
                    Update = 1.2M
                },

                // TFS 2012

                "11.0.50727.1" => new ServerVersion(versionString, "TFS 2012 RTM", year)
                {
                    FriendlyVersion = "Team Foundation Server 2012 (RTM)",
                    Update = 0
                },
                "11.0.51106.1" => new ServerVersion(versionString, "TFS 2012.1", year)
                {
                    FriendlyVersion = "Team Foundation Server 2012 Update 1",
                    Update = 1
                },
                "11.0.60123.100" => new ServerVersion(versionString, "TFS 2012.1.1", year)
                {
                    FriendlyVersion = "Team Foundation Server 2012 Update 1 Cumulative Update 1",
                    Update = 1.1M
                },
                "11.0.60315.1" => new ServerVersion(versionString, "TFS 2012.2", year)
                {
                    FriendlyVersion = "Team Foundation Server 2012 Update 2",
                    Update = 2
                },
                "11.0.60610.1" => new ServerVersion(versionString, "TFS 2012.3", year)
                {
                    FriendlyVersion = "Team Foundation Server 2012 Update 3",
                    Update = 3
                },
                "11.0.61030.0" => new ServerVersion(versionString, "TFS 2012.4", year)
                {
                    FriendlyVersion = "Team Foundation Server 2012 Update 4",
                    Update = 4
                },

                // TFS 2013

                "12.0.20827.3" => new ServerVersion(versionString, "TFS 2013 RC", year)
                {
                    FriendlyVersion = "Team Foundation Server 2013 Release Candidate",
                    Update = 0.9M
                },
                "12.0.21005.1" => new ServerVersion(versionString, "TFS 2013 RTM", year)
                {
                    FriendlyVersion = "Team Foundation Server 2013 (RTM)",
                    Update = 0
                },
                "12.0.30324.0" => new ServerVersion(versionString, "TFS 2013.2", year)
                {
                    FriendlyVersion = "Team Foundation Server 2013 Update 2",
                    Update = 2
                },
                "12.0.30626.0" => new ServerVersion(versionString, "TFS 2013.3 RC", year)
                {
                    FriendlyVersion = "Team Foundation Server 2013 Update 3 Release Candidate",
                    Update = 2.9M
                },
                "12.0.30723.0" => new ServerVersion(versionString, "TFS 2013.3", year)
                {
                    FriendlyVersion = "Team Foundation Server 2013 Update 3",
                    Update = 3
                },
                "12.0.31010.0" => new ServerVersion(versionString, "TFS 2013.4 RC", year)
                {
                    FriendlyVersion = "Team Foundation Server 2013 Update 4 Release Candidate",
                    Update = 3.9M
                },
                "12.0.31101.0" => new ServerVersion(versionString, "TFS 2013.4", year)
                {
                    FriendlyVersion = "Team Foundation Server 2013 Update 4",
                    Update = 4
                },
                "12.0.40629.0" => new ServerVersion(versionString, "TFS 2013.5", year)
                {
                    FriendlyVersion = "Team Foundation Server 2013 Update 5",
                    Update = 5
                },

                // TFS 2015

                // TFS 2017

                // TFS 2018

                "16.121.26818.0" => new ServerVersion(versionString, "TFS 2018 RC1", year)
                {
                    FriendlyVersion = "Team Foundation Server 2018 Release Candidate 1",
                    Update = 0.91M
                },
                "16.122.26918.3" => new ServerVersion(versionString, "TFS 2018 RC2", year)
                {
                    FriendlyVersion = "Team Foundation Server 2018 Release Candidate 2",
                    Update = 0.92M
                },
                "16.122.27102.1" => new ServerVersion(versionString, "TFS 2018 RTW", year)
                {
                    FriendlyVersion = "Team Foundation Server 2018 (RTW)",
                    Update = 0
                },
                "16.131.28601.4" => new ServerVersion(versionString, "TFS 2018.3.2", year)
                {
                    FriendlyVersion = "Team Foundation Server 2018 Update 3.2",
                    Update = 3.2M
                },

                // TFS (Azure DevOps Server) 2019

                // TFS (Azure DevOps Server) 2020

                // Unknown

                _ => new ServerVersion(versionString, $"TFS {year}", year)
                {
                    FriendlyVersion = $"{(version.Major >= 17 ? "Azure DevOps" : "Team Foundation")} Server {year}",
                }
            };
        }

        public bool IsYear(int year)
        {
            return _tfsMajorVersionTable.ContainsValue(year);
        }

        public bool IsMajorVersion(int majorVersion)
        {
            return _tfsMajorVersionTable.ContainsKey(majorVersion);
        }

        private readonly Dictionary<int, int> _tfsMajorVersionTable = new Dictionary<int, int>
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
            [18] = 2020,
            [19] = 2022,
        };
    }
}