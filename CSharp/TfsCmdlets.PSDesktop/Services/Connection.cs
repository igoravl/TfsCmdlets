using System;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;

namespace TfsCmdlets.Services
{
    public partial class Connection
    {
        public static implicit operator TfsConnection(Connection c) => c.InnerConnection;
        public static implicit operator Connection(TfsConnection c) => new Connection(c);

        internal TfsConnection InnerConnection => BaseObject as TfsConnection;

        internal TfsConnection ConfigurationServer
        {
            get
            {
                if (InnerConnection is TfsTeamProjectCollection tpc)
                {
                    var srv = tpc.ConfigurationServer;

                    if(srv == null)
                    {
                        srv = new TfsConfigurationServer(tpc.Uri, tpc.ClientCredentials);
                    }

                    return srv;
                }

                return InnerConnection;
            }
        }

        internal TeamFoundationIdentity AuthorizedIdentity => InnerConnection.AuthorizedIdentity;

        internal Guid ServerId => InnerConnection.InstanceId;

        public object GetClientFromType(Type type)
        {
            return InnerConnection.GetType()
                .GetMethod("GetClient")
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
            else if(BaseObject is TfsConfigurationServer srv)
            {
                srv.EnsureAuthenticated();
            }
        }
    }
}