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
        }

        public void Set(Connection server)
        {
            Reset();

            Server = server;
            // TODO: Mru.Server.Set(Server.Uri.ToString());
        }

        public void Set(Connection server, Models.Connection collection)
        {
            Set(server);

            Collection = collection;
            // TODO: Mru.Collection.Set(Collection.Uri.ToString());
        }

        public void Set(Connection server, Models.Connection collection, WebApiTeamProject project)
        {
            Set(server, collection);
            Project = project;
        }

        public void Set(Connection server, Models.Connection collection, WebApiTeamProject project, WebApiTeam team)
        {
            Set(server, collection, project);
            Team = team;
        }
    }
}
