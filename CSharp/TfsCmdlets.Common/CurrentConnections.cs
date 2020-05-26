using System;
using Microsoft.TeamFoundation.Core.WebApi;
using TfsCmdlets.Services;

namespace TfsCmdlets
{
    internal static class CurrentConnections
    {
        public static Connection Server { get; private set; }

        public static Connection Collection { get; private set; }

        public static TeamProject Project { get; private set; }

        public static WebApiTeam Team { get; private set; }

        public static object Get(string name)
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

        internal static void Reset()
        {
            Server = null;
            Collection = null;
            Project = null;
            Team = null;
        }

        internal static void Set(Connection server)
        {
            Reset();

            Server = server;
            // TODO: Mru.Server.Set(Server.Uri.ToString());
        }

        internal static void Set(Connection server, Connection collection)
        {
            Set(server);

            Collection = collection;
            // TODO: Mru.Collection.Set(Collection.Uri.ToString());
        }

        internal static void Set(Connection server, Connection collection, TeamProject project)
        {
            Set(server, collection);
            Project = project;
        }

        internal static void Set(Connection server, Connection collection, TeamProject project, WebApiTeam team)
        {
            Set(server, collection, project);
            Team = team;
        }
    }
}
