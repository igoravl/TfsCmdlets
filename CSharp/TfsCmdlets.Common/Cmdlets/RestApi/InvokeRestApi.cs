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
using TfsCmdlets.Services;
using TfsCmdlets.Util;

namespace TfsCmdlets.Cmdlets.RestApi
{
    [Cmdlet(VerbsLifecycle.Invoke, "RestApi", DefaultParameterSetName = "URL call")]
    public class InvokeRestApi : BaseCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "URL call")]
        [ValidateNotNullOrEmpty]
        public string Path { get; set; }

        [Parameter(ParameterSetName = "URL call")]
        public string Method { get; set; } = "GET";

        [Parameter(ParameterSetName = "URL call")]
        [Alias("Content")]
        public string Body { get; set; }

        [Parameter(ParameterSetName = "URL call")]
        public string RequestContentType { get; set; } = "application/json";

        [Parameter(ParameterSetName = "URL call")]
        public string ResponseContentType { get; set; } = "application/json";

        [Parameter(ParameterSetName = "URL call")]
        public Dictionary<string, string> AdditionalHeaders { get; set; }

        [Parameter(ParameterSetName = "URL call")]
        public Dictionary<string, string> QueryParameters { get; set; }

        [Parameter(ParameterSetName = "URL call")]
        public string ApiVersion { get; set; } = "4.1";

        [Parameter(ParameterSetName = "URL call")]
        public object Team { get; set; }

        [Parameter(ParameterSetName = "URL call")]
        public object Project { get; set; }

        [Parameter(ParameterSetName = "URL call")]
        public string UseHost { get; set; }

        [Parameter] public object Collection { get; set; }

        [Parameter] public SwitchParameter Raw { get; set; }

        [Parameter] public SwitchParameter AsTask { get; set; }

        /// <summary>
        /// Performs execution of the command
        /// </summary>
        protected override void ProcessRecord()
        {
            if (Path.IsLike("*{project}*"))
            {
                //TODO: Team / TP

                //if (Team.Project)
                //{
                //    Project = Team.Project
                //};

                //tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw new Exception($"Invalid or non-existent team project {Project}."}; tpc = tp.Store.TeamProjectCollection)
                //Path = Path.Replace("{project}", tp.Guid)

                //this.Log($"Replace token {{project}} in URL with [{tp.Guid}]");
            }

            if (Path.IsLike("*{team}*"))
            {
                //TODO: Team
                //t = Get-TfsTeam -Team Team -Project Project -Collection Collection; if (t.Count != 1) {throw new Exception($"Invalid or non-existent team "{Team}"."}; if(t.ProjectName) {Project = t.ProjectName}; tp = this.GetProject();; if (! tp || (tp.Count != 1)) {throw "Invalid or non-existent team project Project."}; tpc = tp.Store.TeamProjectCollection)
                //Path = Path.Replace("{team}", t.Id)

                //this.Log($"Replace token {team} in URL with "{{t}.Id}"");
            }

            this.Log($"Calling API '{Path}', version 'ApiVersion', via {Method}");

            var collection = this.GetCollection();
            var client = this.GetService<IRestApiService>();
            var task = client.InvokeAsync(collection, Path, Method, Body,
                RequestContentType, ResponseContentType,
                AdditionalHeaders, QueryParameters, ApiVersion);

            this.Log($"Actual URL called: {client.Uri}");

            if (AsTask)
            {
                WriteObject(task);
                return;
            }

            var result = task.GetResult("Unknown error when calling REST API");
            var responseBody = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var responseType = result.Content.Headers.ContentType.MediaType;

            if (Raw)
            {
                var o = new PSObject(result);

                o.AddNoteProperty("ResponseBody", responseBody);

                if (responseType.Equals("application/json"))
                    o.AddNoteProperty("ResponseObject", PSJsonConverter.Deserialize(responseBody));

                WriteObject(o);
                return;
            }

            WriteObject(responseType.Equals("application/json")
                ? PSJsonConverter.Deserialize(responseBody)
                : responseBody);
        }
    }
}