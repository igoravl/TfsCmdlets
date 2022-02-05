using System.Collections.Generic;
using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.GlobalList
{
    /// <summary>
    /// Changes the contents of a Global List.
    /// </summary>
    [TfsCmdlet(CmdletScope.Collection, DesktopOnly = true, SupportsShouldProcess = true)]
    partial class SetGlobalList 
    {
        /// <summary>
        /// Specifies the name of the global list to be changed.
        /// </summary>
        /// <value></value>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Name")]
        public string GlobalList { get; set; }

        /// <summary>
        /// Specifies a list of items to be added to the global list.
        /// </summary>
        [Parameter]
        public IEnumerable<string> Add { get; set; }

        /// <summary>
        /// Specifies a list of items to be removed from the global list.
        /// </summary>
        [Parameter]
        public IEnumerable<string> Remove { get; set; }

        /// <summary>
        /// Creates a new list if the specified one does not exist.
        /// </summary>
        [Parameter]
        public SwitchParameter Force { get; set; }
    }

    // TODO

    //partial class GlobalListDataService
    //{
    //    protected override Models.GlobalList DoSetItem()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}