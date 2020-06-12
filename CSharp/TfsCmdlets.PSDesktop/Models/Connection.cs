using System;
using System.Linq;
using System.Reflection;
using Microsoft.TeamFoundation.Client;

namespace TfsCmdlets.Models
{
    partial class Connection
    {
        /// <summary>
        /// Converts a Connection object to a TfsConnection-derived object
        /// </summary>
        public static implicit operator TfsConnection(Connection c) => c?.InnerConnection;

        /// <summary>
        /// Converts a TfsConnection-derived object to a Connection object
        /// </summary>
        public static implicit operator Connection(TfsConnection c) => new Connection(c);

        internal TfsConnection InnerConnection => BaseObject as TfsConnection;

        internal TfsConnection ConfigurationServer
        {
            get
            {
                if (InnerConnection is TfsTeamProjectCollection tpc)
                {
                    var srv = tpc.ConfigurationServer;

                    if (srv == null)
                    {
                        srv = new TfsConfigurationServer(tpc.Uri, tpc.ClientCredentials);
                    }

                    return srv;
                }

                return InnerConnection;
            }
        }

        internal Models.Identity AuthorizedIdentity => new Models.Identity(InnerConnection.AuthorizedIdentity);

        internal Guid ServerId => InnerConnection.InstanceId;

        internal virtual T GetService<T>() => InnerConnection.GetService<T>();

        internal virtual string DisplayName
        {
            get
            {
                if (InnerConnection == null) return null;

                if (InnerConnection.Uri.Segments.Length > 1 &&
                    !InnerConnection.Uri.Segments[InnerConnection.Uri.Segments.Length - 1].Equals("tfs", StringComparison.OrdinalIgnoreCase))
                {
                    return InnerConnection.Uri.Segments[InnerConnection.Uri.Segments.Length - 1];
                }
                else if (InnerConnection.Uri.Host.EndsWith(".visualstudio.com"))
                {
                    return InnerConnection.Uri.Host.Substring(InnerConnection.Uri.Host.IndexOf('.'));
                }

                return InnerConnection.Uri.AbsoluteUri;
            }
        }

        /// <summary>
        /// Gets a client of the given type
        /// </summary>
        public object GetClientFromType(Type type)
        {
            var m = InnerConnection.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.Name.Equals("GetClient")).FirstOrDefault();

            return m?
                .MakeGenericMethod(type)
                .Invoke(InnerConnection, null);
        }

        internal bool IsHosted => InnerConnection.IsHostedServer;

        partial void DoConnect()
        {
            if (BaseObject is TfsTeamProjectCollection tpc)
            {
                tpc.EnsureAuthenticated();
            }
            else if (BaseObject is TfsConfigurationServer srv)
            {
                srv.EnsureAuthenticated();
            }
        }
    }
}