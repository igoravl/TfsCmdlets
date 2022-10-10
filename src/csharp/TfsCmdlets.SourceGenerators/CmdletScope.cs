using System;

namespace TfsCmdlets.SourceGenerators
{
    [Flags]
        public enum CmdletScope
        {
            None = 0,
            Server = 1,
            Collection = 2,
            Project = 3,
            Team = 4
        }

}