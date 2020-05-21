/*
.SYNOPSIS
    Short description
.DESCRIPTION
    Long description
.EXAMPLE
    PS C:> <example usage>
    Explanation of what the example does
.INPUTS
    Inputs (if any)
.OUTPUTS
    Output (if any)
.NOTES
    General notes
*/

using System.Collections.Generic;
using System.Management.Automation;
using TfsCmdlets.Extensions;
using TfsCmdlets.HttpClient;

namespace TfsCmdlets.Cmdlets.RestApi
{
    [Cmdlet(VerbsLifecycle.Invoke, "RestApi", DefaultParameterSetName = "URL call")]
    public class InvokeRestApi: PSCmdlet
    {

        [Parameter(Position=0, Mandatory=true, ParameterSetName="Library call")]
        [Alias("Name", "API")]
        public string Operation { get; set; }

        [Parameter(ParameterSetName="Library call")]
        [Alias("Client", "Type")]
        public string ClientType { get; set; }

        [Parameter(ParameterSetName="Library call")]
        public object[] ArgumentList { get; set; }

        [Parameter(Mandatory=true, Position=0, ParameterSetName="URL call")]
        public string Path { get; set; }

        [Parameter(ParameterSetName = "URL call")]
        public string Method { get; set; } = "GET";

        [Parameter(ParameterSetName="URL call")]
        [Alias("Content")]
        public string Body { get; set; }

        [Parameter(ParameterSetName = "URL call")]
        public string RequestContentType { get; set; } = "application/json";

        [Parameter(ParameterSetName = "URL call")]
        public string ResponseContentType { get; set; } = "application/json";

        [Parameter(ParameterSetName="URL call")]
        public Dictionary<string,string> AdditionalHeaders { get; set; }

        [Parameter(ParameterSetName="URL call")]
        public Dictionary<string, string> QueryParameters { get; set; }

        [Parameter(ParameterSetName = "URL call")]
        public string ApiVersion { get; set; } = "4.1";

        [Parameter(ParameterSetName="URL call")]
        public object Team { get; set; }

        [Parameter(ParameterSetName="URL call")]
        public object Project { get; set; }

        [Parameter(ParameterSetName="URL call")]
        public string UseHost { get; set; }

        [Parameter()]
        public object Collection { get; set; }

        [Parameter()]
        public SwitchParameter Raw { get; set; }

        [Parameter()]
        public SwitchParameter AsTask { get; set; }

    protected override void EndProcessing()
    {
        var tpc = this.GetCollection();

        if(ParameterSetName == "Library call")
        {
            this.Log("Using library call method");

            client = Get-TfsRestClient ClientType -Collection tpc
            task = client.Operation.Invoke(ArgumentList)
        }
        else
        {
            _Log "Using URL call method"

            Path = Path.TrimStart("/")
            
            if(UseHost)
            {
                if(UseHost.IndexOf(".") == -1)
                {
                    _Log $"Converting service prefix {UseHost} to UseHost.dev.azure.com"
                    UseHost += ".dev.azure.com"
                }

                _Log $"Using service host {UseHost}"
                [GenericHttpClient]::UseHost(UseHost)
            }

            client = Get-TfsRestClient "TfsCmdlets.GenericHttpClient" -Collection tpc

            if(Path -like "*{project}*")
            {
                if(Team.Project) {Project = Team.Project}; tp = Get-TfsTeamProject -Project Project -Collection Collection; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)
                Path = Path.Replace("{project}", tp.Guid)

                _Log $"Replace token {project} in URL with "{{tp}.Guid}""
            }

            if(Path -like "*{team}*")
            {
                t = Get-TfsTeam -Team Team -Project Project -Collection Collection; if (t.Count != 1) {throw new Exception($"Invalid or non-existent team "{Team}"."}; if(t.ProjectName) {Project = t.ProjectName}; tp = Get-TfsTeamProject -Project Project -Collection Collection; if (! tp || (tp.Count != 1)) {throw "Invalid or non-existent team project Project."}; tpc = tp.Store.TeamProjectCollection)
                Path = Path.Replace("{team}", t.Id)

                _Log $"Replace token {team} in URL with "{{t}.Id}""
            }

            _Log $"Calling API "{Path}", version "ApiVersion", via Method"
            
            task = client.InvokeAsync(Method, Path, Body, RequestContentType, ResponseContentType, AdditionalHeaders, QueryParameters, ApiVersion)

            _Log $"URI called: {{client}.Uri}"
        }

        if (AsTask)
        {
            WriteObject(task); return;
        }

        result = task.Result; if(task.IsFaulted) { _throw new Exception(Message task.Exception.InnerExceptions })

        if(ParameterSetName == "URL call")
        {
            response = result.Content.ReadAsStringAsync().GetAwaiter().GetResult()

            if(Raw.IsPresent)
            {
                Add-Member -InputObject result -Name "ResponseString" -MemberType NoteProperty -Value response

                switch(result.Content.Headers.ContentType)
                {
                    "application/json" {
                        obj = (response | ConvertFrom-Json)
                        Add-Member -InputObject result -Name "ResponseObject" -MemberType NoteProperty -Value obj
                    }
                }
            }
            else
            {
                switch(result.Content.Headers.ContentType)
                {
                    "application/json" {
                        response = (response | ConvertFrom-Json)
                    }
                }
                result = response
            }
        }
        
        WriteObject(result); return;
    }
}
*/
}
}
