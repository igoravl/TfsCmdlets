using System;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Models;

namespace TfsCmdlets.Services.Impl
{
    [Exports(typeof(ICurrentConnections), Singleton = true)]
    public class CurrentConnections: ICurrentConnections
    {
        public ServerConnection Server {get;set;}

        public Models.TpcConnection Collection {get;set;}

        public TeamProject Project {get;set;}

        public WebApiTeam Team {get;set;}

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

        public void Set(ServerConnection server)
        {
            Reset();

            Server = server;
            // TODO: Mru.Server.Set(Server.Uri.ToString());
        }

        public void Set(ServerConnection server, Models.TpcConnection collection)
        {
            Set(server);

            Collection = collection;
            // TODO: Mru.Collection.Set(Collection.Uri.ToString());
        }

        public void Set(ServerConnection server, Models.TpcConnection collection, TeamProject project)
        {
            Set(server, collection);
            Project = project;
        }

        public void Set(ServerConnection server, Models.TpcConnection collection, TeamProject project, WebApiTeam team)
        {
            Set(server, collection, project);
            Team = team;
        }
    }
}
