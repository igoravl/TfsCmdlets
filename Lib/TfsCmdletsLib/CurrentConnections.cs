using System;
using System.Collections.Generic;
using System.Text;

namespace TfsCmdlets
{
    public static class CurrentConnections
    {
        public static object Organization { get; set; }

        public static object Server { get; set; }

        public static object Collection { get; set; }

        public static object Project { get; set; }

        public static object Team { get; set; }

        public static void Reset()
        {
            Organization = null;
            Server = null;
            Collection = null;
            Project = null;
            Team = null;
        }
    }
}
