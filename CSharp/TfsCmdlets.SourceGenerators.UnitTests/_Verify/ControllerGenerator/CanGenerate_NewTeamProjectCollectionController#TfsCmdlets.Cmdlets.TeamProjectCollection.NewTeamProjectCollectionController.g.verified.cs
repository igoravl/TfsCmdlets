//HintName: TfsCmdlets.Cmdlets.TeamProjectCollection.NewTeamProjectCollectionController.g.cs
using System.Management.Automation;
using System;
using System.Threading;
using Microsoft.TeamFoundation.Framework.Common;
using TfsCmdlets.Models;
namespace TfsCmdlets.Cmdlets.TeamProjectCollection
{
    internal partial class NewTeamProjectCollectionController: ControllerBase
    {
        // Collection
        protected bool Has_Collection { get; set; }
        protected object Collection { get; set; }
        // Description
        protected bool Has_Description { get; set; }
        protected string Description { get; set; }
        // DatabaseServer
        protected bool Has_DatabaseServer { get; set; }
        protected string DatabaseServer { get; set; }
        // DatabaseName
        protected bool Has_DatabaseName { get; set; }
        protected string DatabaseName { get; set; }
        // ConnectionString
        protected bool Has_ConnectionString { get; set; }
        protected string ConnectionString { get; set; }
        // Default
        protected bool Has_Default { get; set; }
        protected bool Default { get; set; }
        // UseExistingDatabase
        protected bool Has_UseExistingDatabase { get; set; }
        protected bool UseExistingDatabase { get; set; }
        // InitialState
        protected bool Has_InitialState { get; set; }
        protected string InitialState { get; set; }
        // PollingInterval
        protected bool Has_PollingInterval { get; set; }
        protected int PollingInterval { get; set; }
        // Timeout
        protected bool Has_Timeout { get; set; }
        protected System.TimeSpan Timeout { get; set; }
        // Passthru
        protected bool Has_Passthru { get; set; }
        protected bool Passthru { get; set; }
        // Server
        protected bool Has_Server => Parameters.HasParameter("Server");
        protected Models.Connection Server => Data.GetServer();
        // ParameterSetName
        protected bool Has_ParameterSetName { get; set; }
        protected string ParameterSetName { get; set; }
        // Items
        protected IEnumerable<TfsCmdlets.Models.Connection> Items => Collection switch {
            TfsCmdlets.Models.Connection item => new[] { item },
            IEnumerable<TfsCmdlets.Models.Connection> items => items,
            _ => Data.GetItems<TfsCmdlets.Models.Connection>()
        };
        // DataType
        public override Type DataType => typeof(TfsCmdlets.Models.Connection);
        protected override void CacheParameters()
        {
            // Collection
            Has_Collection = Parameters.HasParameter("Collection");
            Collection = Parameters.Get<object>("Collection");
            // Description
            Has_Description = Parameters.HasParameter("Description");
            Description = Parameters.Get<string>("Description");
            // DatabaseServer
            Has_DatabaseServer = Parameters.HasParameter("DatabaseServer");
            DatabaseServer = Parameters.Get<string>("DatabaseServer");
            // DatabaseName
            Has_DatabaseName = Parameters.HasParameter("DatabaseName");
            DatabaseName = Parameters.Get<string>("DatabaseName");
            // ConnectionString
            Has_ConnectionString = Parameters.HasParameter("ConnectionString");
            ConnectionString = Parameters.Get<string>("ConnectionString");
            // Default
            Has_Default = Parameters.HasParameter("Default");
            Default = Parameters.Get<bool>("Default");
            // UseExistingDatabase
            Has_UseExistingDatabase = Parameters.HasParameter("UseExistingDatabase");
            UseExistingDatabase = Parameters.Get<bool>("UseExistingDatabase");
            // InitialState
            Has_InitialState = Parameters.HasParameter("InitialState");
            InitialState = Parameters.Get<string>("InitialState", "Started");
            // PollingInterval
            Has_PollingInterval = Parameters.HasParameter("PollingInterval");
            PollingInterval = Parameters.Get<int>("PollingInterval", 5);
            // Timeout
            Has_Timeout = Parameters.HasParameter("Timeout");
            Timeout = Parameters.Get<System.TimeSpan>("Timeout", TimeSpan.MaxValue);
            // Passthru
            Has_Passthru = Parameters.HasParameter("Passthru");
            Passthru = Parameters.Get<bool>("Passthru");
            // ParameterSetName
            Has_ParameterSetName = Parameters.HasParameter("ParameterSetName");
            ParameterSetName = Parameters.Get<string>("ParameterSetName");
        }
        [ImportingConstructor]
        public NewTeamProjectCollectionController(IPowerShellService powerShell, IDataManager data, IParameterManager parameters, ILogger logger)
            : base(powerShell, data, parameters, logger)
        {
        }
    }
}