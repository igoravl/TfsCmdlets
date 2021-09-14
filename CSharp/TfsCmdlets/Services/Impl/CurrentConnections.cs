using System;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Models;
using TfsCmdlets.Services;

namespace TfsCmdlets
{
    [Exports(typeof(ICurrentConnections), Singleton = true)]
    public class CurrentConnections: ICurrentConnections
    {
        public ConfigurationServer Server {get;set;}

        public Models.TeamProjectCollection Collection {get;set;}

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

        public void Set(ConfigurationServer server)
        {
            Reset();

            Server = server;
            // TODO: Mru.Server.Set(Server.Uri.ToString());
        }

        public void Set(ConfigurationServer server, Models.TeamProjectCollection collection)
        {
            Set(server);

            Collection = collection;
            // TODO: Mru.Collection.Set(Collection.Uri.ToString());
        }

        public void Set(ConfigurationServer server, Models.TeamProjectCollection collection, TeamProject project)
        {
            Set(server, collection);
            Project = project;
        }

        public void Set(ConfigurationServer server, Models.TeamProjectCollection collection, TeamProject project, WebApiTeam team)
        {
            Set(server, collection, project);
            Team = team;
        }
    }
}
