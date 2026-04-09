using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services.Impl
{
    [Export(typeof(ICurrentConnections)), Shared]
    public class CurrentConnectionsImpl: ICurrentConnections
    {
        public Connection Server {get;set;}

        public Models.Connection Collection {get;set;}

        public WebApiTeamProject Project {get;set;}

        public Models.Team Team {get;set;}

        public AzureCredential AzureCredential {get;set;}

        public T Get<T>(string name)
        {
            return (T) Get(name);
        }

        public object Get(string name)
        {
            return name.ToLowerInvariant() switch
            {
                "server" => (object) Server,
                "collection" => Collection,
                "project" => Project,
                "team" => Team,
                _ => throw new ArgumentException(nameof(name))
            };
        }

        public void Reset()
        {
            Server = null;
            Collection = null;
            Project = null;
            Team = null;
            AzureCredential = null;

            SyncEnvironmentVariables();
        }

        public void Set(Connection server)
        {
            Server = server;
            Collection = null;
            Project = null;
            Team = null;
            AzureCredential = null;
            // TODO: Mru.Server.Set(Server.Uri.ToString());

            SyncEnvironmentVariables();
        }

        public void Set(Connection server, Models.Connection collection)
        {
            Server = server;
            Collection = collection;
            Project = null;
            Team = null;
            AzureCredential = null;
            // TODO: Mru.Collection.Set(Collection.Uri.ToString());

            SyncEnvironmentVariables();
        }

        public void Set(Connection server, Models.Connection collection, WebApiTeamProject project)
        {
            Server = server;
            Collection = collection;
            Project = project;
            Team = null;
            AzureCredential = null;

            SyncEnvironmentVariables();
        }

        public void Set(Connection server, Models.Connection collection, WebApiTeamProject project, WebApiTeam team)
        {
            Server = server;
            Collection = collection;
            Project = project;
            Team = team;
            AzureCredential = null;

            SyncEnvironmentVariables();
        }

        /// <summary>
        /// Synchronizes the current connection state to environment variables (TFSCMDLETS_*),
        /// enabling integration with Oh-My-Posh, Starship, and other prompt customization tools.
        /// </summary>
        private void SyncEnvironmentVariables()
        {
            static void SetVar(string name, string value)
                => Environment.SetEnvironmentVariable(name, value, EnvironmentVariableTarget.Process);

            if (Collection is { } col)
            {
                SetVar("TFSCMDLETS_CONNECTED", "true");
                SetVar("TFSCMDLETS_ORG", col.DisplayName?.TrimEnd('/'));
                SetVar("TFSCMDLETS_ORG_URL", col.Uri?.ToString());
                SetVar("TFSCMDLETS_USER", col.CurrentUserUniqueName);
                SetVar("TFSCMDLETS_USER_DISPLAY", col.CurrentUserDisplayName);
                SetVar("TFSCMDLETS_IS_HOSTED", col.IsHosted ? "true" : "false");
            }
            else
            {
                SetVar("TFSCMDLETS_CONNECTED", null);
                SetVar("TFSCMDLETS_ORG", null);
                SetVar("TFSCMDLETS_ORG_URL", null);
                SetVar("TFSCMDLETS_USER", null);
                SetVar("TFSCMDLETS_USER_DISPLAY", null);
                SetVar("TFSCMDLETS_IS_HOSTED", null);
            }

            SetVar("TFSCMDLETS_PROJECT", Project?.Name);
            SetVar("TFSCMDLETS_TEAM", Team?.Name);
        }

        /// <summary>
        /// Clears all TFSCMDLETS_* environment variables. Called during module cleanup.
        /// </summary>
        internal static void ClearEnvironmentVariables()
        {
            foreach (var name in new[]
            {
                "TFSCMDLETS_CONNECTED", "TFSCMDLETS_ORG", "TFSCMDLETS_ORG_URL",
                "TFSCMDLETS_USER", "TFSCMDLETS_USER_DISPLAY", "TFSCMDLETS_IS_HOSTED",
                "TFSCMDLETS_PROJECT", "TFSCMDLETS_TEAM"
            })
            {
                Environment.SetEnvironmentVariable(name, null, EnvironmentVariableTarget.Process);
            }
        }
    }
}
