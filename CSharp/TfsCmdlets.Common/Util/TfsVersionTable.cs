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

    public class ServerVersion
    {
        public Version Version { get; set; }
        public string LongVersion { get; set; }
        public string FriendlyVersion { get; set; }
        public bool IsHosted { get; set; }
        public string Sprint { get; set; }
        public string Update { get; set; }

    }
}