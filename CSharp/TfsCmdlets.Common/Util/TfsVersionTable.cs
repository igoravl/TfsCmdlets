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

        internal static ServerVersion GetServerVersion(int majorVersion)
        {
            throw new NotImplementedException();
        }

        internal static bool TryGetServerVersion(int majorVersion, out ServerVersion version)
        {
            // TODO
            version = null;
            return false;
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
        /// Gets the number of the sprint currently deployed in an Azure DevOps Services organization
        /// </summary>
        public string Sprint { get; set; }

        /// <summary>
        /// Gets the version number of the Update installed on a server
        /// </summary>
        public string Update { get; set; }

    }
}