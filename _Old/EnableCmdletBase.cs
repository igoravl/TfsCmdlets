using System;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Models;
using TfsCmdlets.Util;
using WebApiTeamProject = Microsoft.TeamFoundation.Core.WebApi.TeamProject;
using Microsoft.VisualStudio.Services.WebApi;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TfsCmdlets.Services;

namespace TfsCmdlets.Cmdlets
{
    /// <summary>
    /// Abstract class from which and TfsCmdlets cmdlets derive
    /// </summary>
    public abstract class EnableCmdletBase<T> : CmdletBase
        where T : class
    {
        /// <summary>
        /// HELP_PARAM_COLLECTION
        /// </summary>
        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// HELP_PARAM_PASSTHRU
        /// </summary>
        [Parameter()]
        public SwitchParameter Passthru { get; set; }

        /// <summary>
        /// Performs execution of the command.
        /// </summary>
        protected override void DoProcessRecord()
        {
            var result = EnableItem<T>();

            if (Passthru && result != null)
            {
                WriteObject(result);
            }
        }
    }
}