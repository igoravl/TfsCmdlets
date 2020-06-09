using System;
using System.Management.Automation;
using TfsGlobalList = TfsCmdlets.Cmdlets.GlobalList.GlobalList;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    partial class RemoveGlobalList
    {
        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            var list = GetInstanceOf<TfsGlobalList>();

            if(list == null) 
            {
                throw new ArgumentException($"Invalid or non-existent global list '{GlobalList}'");
            }

            var tpc = GetCollection();

            if(!ShouldProcess($"Team Project Collection [{tpc.DisplayName}]", $"Delete global list [{list.Name}]")) return;

            Remove(list.Name);
        }
    }
}