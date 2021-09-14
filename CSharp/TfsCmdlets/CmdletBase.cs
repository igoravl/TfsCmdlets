using System;
using System.Management.Automation;
using System.Reflection;
using TfsCmdlets.Util;
using TfsCmdlets.Services;
using System.Collections.Generic;

namespace TfsCmdlets.Cmdlets
{
    /// <summary>
    /// Abstract class from which all TfsCmdlets cmdlets derive
    /// </summary>
    public abstract class CmdletBase<T> : BasicCmdlet where T : class
    {
        [Inject] protected IController<T> Controller { get; set; }

        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command.
        /// </summary>
        protected override void DoProcessRecord()
        {
            var result = Controller.InvokeVerb(Verb);

            if (result != null && ReturnsValue)
            {
                WriteObject(result, true);
            }
        }
    }
}