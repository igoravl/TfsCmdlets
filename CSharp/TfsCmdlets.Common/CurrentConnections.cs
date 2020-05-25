using System;
using Microsoft.VisualStudio.Services.WebApi;
using TfsCmdlets.Util;

namespace TfsCmdlets
{
    internal static class CurrentConnections
    {
        public static VssConnection Server { get; private set; }

        public static VssConnection Collection { get; private set; }

        public static object Project { get; private set; }

        public static object Team { get; private set; }

        public static object Get(string name)
        {
            return name.ToLowerInvariant() switch
            {
                "server" => Server,
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

        internal static void Set(VssConnection server)
        {
            Reset();

            Server = server;
            Mru.Server.Set(Server.Uri.ToString());
        }

        internal static void Set(VssConnection server, VssConnection collection)
        {
            Set(server);

            Collection = collection;
            Mru.Collection.Set(Collection.Uri.ToString());
        }

        internal static void Set(VssConnection server, VssConnection collection, object project)
        {
            Set(server, collection);
            Project = project;
        }

        internal static void Set(VssConnection server, VssConnection collection, object project, object team)
        {
            Set(server, collection, project);
            Team = team;
        }
    }
}
