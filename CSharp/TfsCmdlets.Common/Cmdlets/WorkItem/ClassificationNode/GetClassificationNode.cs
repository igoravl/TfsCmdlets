using System.Management.Automation;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TfsCmdlets.Cmdlets.WorkItem.ClassificationNode
{
    [Cmdlet(VerbsCommon.Get, "ClassificationNode")]
    [OutputType(typeof(WorkItemClassificationNode))]
    public class GetClassificationNode: BaseCmdlet
    {
/*
        [Parameter()]
        [SupportsWildcards()]
        [Alias("Area")]
        [Alias("Iteration")]
        [Alias("Path")]
        public object Node { get; set; } = "\**",

        [Parameter()]
        [Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup]
        StructureGroup,

        [Parameter(ValueFromPipeline=true)]
        public object Project { get; set; }

        [Parameter()]
        public object Collection { get; set; }

    protected override void ProcessRecord()
    {
        if (Node is Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode) { this.Log("Input item is of type Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemClassificationNode; returning input item immediately, without further processing."; WriteObject(Node }); return;);

        if(! (PSBoundParameters.ContainsKey("StructureGroup"))){if (MyInvocation.InvocationName -like "*Area"){StructureGroup = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.Areas}elseif (MyInvocation.InvocationName -like "*Iteration"){StructureGroup = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.TreeStructureGroup.Iterations}else{throw new Exception("Invalid or missing StructureGroup argument"}};PSBoundParameters["StructureGroup"] = StructureGroup)
        
        tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)

        var client = tpc.GetClient<Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient>();

        if(_IsWildcard Node)
        {
            depth = 2
            pattern = _NormalizeNodePath -Project tp.Name -Scope StructureGroup -Path Node -IncludeScope -IncludeTeamProject -IncludeLeadingBackslash
            Node = "/"

            this.Log($"Preparing to recursively search for pattern "{pattern}"");
        }
        else
        {
            Node = _NormalizeNodePath -Project tp.Name -Scope StructureGroup -Path Node -IncludeLeadingBackslash
            depth = 0

            this.Log($"Getting {StructureGroup} under path "Node"");
        }

        task = client.GetClassificationNodeAsync(tp.Name,StructureGroup,Node,depth); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error retrieving {StructureGroup} from path "Node"" task.Exception.InnerExceptions })

        if(! (pattern))
        {
            WriteObject(result); return;
        }

        _GetNodeRecursively -Pattern pattern -Node result -StructureGroup StructureGroup -Project tp.Name -Client client
    }
}

Function _GetNodeRecursively(Pattern, Node, StructureGroup, Project, Client, Depth = 2)
{
    _FixNodePath -Node Node -StructureGroup StructureGroup -Project Project

    this.Log($"Searching for pattern "{Pattern}" under {Node.Path}");

    if(Node.HasChildren && (Node.Children.Count == 0))
    {
        this.Log($"Fetching child nodes for node "{{Node}.Path}"");

        task = client.GetClassificationNodeAsync(Project,StructureGroup,Node.RelativePath, Depth); result = task.Result; if(task.IsFaulted) { _throw new Exception( $"Error retrieving {StructureGroup} from path "{Node.RelativePath}"" task.Exception.InnerExceptions })
        Node = result
    }

    if(Node.Path -like Pattern)
    {
        this.Log($"{{Node}.Path} matches pattern Pattern. Returning node.");
        Write-Output Node
    }

    foreach(c in Node.Children)
    {
        _GetNodeRecursively -Pattern Pattern -Node c -StructureGroup StructureGroup -Project Project -Client Client -Depth Depth
    }
}

Function _FixNodePath(Node, StructureGroup, Project)
{
    if (Node.Path)
    {
        Write-Verbose "Not fixing path"
        # Nothing to fix
        return
    }

    StructureGroup = StructureGroup.ToString()

    # Older versions of the REST API don"t populate the Path property. So, let"s do it ourselves

    decodedUrl = System.Web.HttpUtility.UrlDecode(Node.Url)
    path = decodedUrl.Substring(decodedUrl.IndexOf($"/{StructureGroup}")+1).Replace(StructureGroup, "").Replace("/", "\")
    Node.Path = $"\{Project}\{StructureGroup.TrimEnd("s"})path"

    Write-Verbose $"Fixed path: {{Node}.Path}"
}

Set-Alias -Name Get-TfsArea -Value Get-TfsClassificationNode
Set-Alias -Name Get-TfsIteration -Value Get-TfsClassificationNode
*/
}
}
