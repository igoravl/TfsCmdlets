//HintName: TfsCmdlets.Cmdlets.RestApi.InvokeRestApiController.g.cs
using System.Management.Automation;
using System.Net.Http;
using TfsCmdlets.Util;
namespace TfsCmdlets.Cmdlets.RestApi
{
    internal partial class InvokeRestApiController: ControllerBase
    {
        // Path
        protected bool Has_Path { get; set; }
        protected string Path { get; set; }
        // Method
        protected bool Has_Method { get; set; }
        protected string Method { get; set; }
        // Body
        protected bool Has_Body { get; set; }
        protected string Body { get; set; }
        // RequestContentType
        protected bool Has_RequestContentType { get; set; }
        protected string RequestContentType { get; set; }
        // ResponseContentType
        protected bool Has_ResponseContentType { get; set; }
        protected string ResponseContentType { get; set; }
        // AdditionalHeaders
        protected bool Has_AdditionalHeaders { get; set; }
        protected System.Collections.Hashtable AdditionalHeaders { get; set; }
        // QueryParameters
        protected bool Has_QueryParameters { get; set; }
        protected System.Collections.Hashtable QueryParameters { get; set; }
        // ApiVersion
        protected bool Has_ApiVersion { get; set; }
        protected string ApiVersion { get; set; }
        // UseHost
        protected bool Has_UseHost { get; set; }
        protected string UseHost { get; set; }
        // NoAutoUnwrap
        protected bool Has_NoAutoUnwrap { get; set; }
        protected bool NoAutoUnwrap { get; set; }
        // Raw
        protected bool Has_Raw { get; set; }
        protected bool Raw { get; set; }
        // Destination
        protected bool Has_Destination { get; set; }
        protected string Destination { get; set; }
        // AsTask
        protected bool Has_AsTask { get; set; }
        protected bool AsTask { get; set; }
        // Team
        protected bool Has_Team => Parameters.HasParameter("Team");
        protected WebApiTeam Team => Data.GetTeam();
        // Project
        protected bool Has_Project => Parameters.HasParameter("Project");
        protected WebApiTeamProject Project => Data.GetProject();
        // Collection
        protected bool Has_Collection => Parameters.HasParameter("Collection");
        protected Models.Connection Collection => Data.GetCollection();
        // Server
        protected bool Has_Server => Parameters.HasParameter("Server");
        protected Models.Connection Server => Data.GetServer();
        // ParameterSetName
        protected bool Has_ParameterSetName { get; set; }
        protected string ParameterSetName { get; set; }
        protected override void CacheParameters()
        {
            // Path
            Has_Path = Parameters.HasParameter("Path");
            Path = Parameters.Get<string>("Path");
            // Method
            Has_Method = Parameters.HasParameter("Method");
            Method = Parameters.Get<string>("Method", "GET");
            // Body
            Has_Body = Parameters.HasParameter("Body");
            Body = Parameters.Get<string>("Body");
            // RequestContentType
            Has_RequestContentType = Parameters.HasParameter("RequestContentType");
            RequestContentType = Parameters.Get<string>("RequestContentType", "application/json");
            // ResponseContentType
            Has_ResponseContentType = Parameters.HasParameter("ResponseContentType");
            ResponseContentType = Parameters.Get<string>("ResponseContentType", "application/json");
            // AdditionalHeaders
            Has_AdditionalHeaders = Parameters.HasParameter("AdditionalHeaders");
            AdditionalHeaders = Parameters.Get<System.Collections.Hashtable>("AdditionalHeaders");
            // QueryParameters
            Has_QueryParameters = Parameters.HasParameter("QueryParameters");
            QueryParameters = Parameters.Get<System.Collections.Hashtable>("QueryParameters");
            // ApiVersion
            Has_ApiVersion = Parameters.HasParameter("ApiVersion");
            ApiVersion = Parameters.Get<string>("ApiVersion", "4.1");
            // UseHost
            Has_UseHost = Parameters.HasParameter("UseHost");
            UseHost = Parameters.Get<string>("UseHost");
            // NoAutoUnwrap
            Has_NoAutoUnwrap = Parameters.HasParameter("NoAutoUnwrap");
            NoAutoUnwrap = Parameters.Get<bool>("NoAutoUnwrap");
            // Raw
            Has_Raw = Parameters.HasParameter("Raw");
            Raw = Parameters.Get<bool>("Raw");
            // Destination
            Has_Destination = Parameters.HasParameter("Destination");
            Destination = Parameters.Get<string>("Destination");
            // AsTask
            Has_AsTask = Parameters.HasParameter("AsTask");
            AsTask = Parameters.Get<bool>("AsTask");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public InvokeRestApiController(IRestApiService restApiService, IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
            RestApiService = restApiService;
        }
    }
}