using System.Management.Automation;

namespace TfsCmdlets.Cmdlets.WorkItem.ClassificationNode
{
    [Cmdlet(VerbsDiagnostic.Test, "TfsClassificationNode")]
    public class TestClassificationNode: BaseCmdlet
    {
/*
        [Parameter(ValueFromPipeline=true, ValueFromPipelineByPropertyName=true)]
        [Alias("Area")]
        [Alias("Iteration")]
        [Alias("Path")]
        [SupportsWildcards()]
        public object Node { get; set; }

        [Parameter()]
        [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup]
        StructureGroup,

        [Parameter()]
        public object Project { get; set; }

        [Parameter()]
        public object Collection { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
    {
        if(! (PSBoundParameters.ContainsKey("StructureGroup"))){if (MyInvocation.InvocationName -like "*Area"){StructureGroup = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.Areas}elseif (MyInvocation.InvocationName -like "*Iteration"){StructureGroup = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.Iterations}else{throw new Exception("Invalid or missing StructureGroup argument"}};PSBoundParameters["StructureGroup"] = StructureGroup)
        
        try
        {
            WriteObject((Get-TfsClassificationNode @PSBoundParameters).Count -gt 0); return;
        }
        catch
        {
            this.Log($"Error testing node "{Node}": {_.Exception}");
            
            WriteObject(false); return;
        }
    }
}

Set-Alias -Name Test-TfsArea -Value Test-TfsClassificationNode
Set-Alias -Name Test-TfsIteration -Value Test-TfsClassificationNode
*/
        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord() => throw new System.NotImplementedException();
    }
}
