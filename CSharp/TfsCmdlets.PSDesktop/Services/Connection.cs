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

        internal TfsConnection ConfigurationServer =>
            (InnerConnection as TfsTeamProjectCollection)?.ConfigurationServer ??
            InnerConnection as TfsConfigurationServer;

        internal TeamFoundationIdentity AuthorizedIdentity => InnerConnection.AuthorizedIdentity;

        internal Guid ServerId => InnerConnection.InstanceId;

        public object GetClientFromType(Type type)
        {
            return InnerConnection.GetType()
                .GetMethod("GetClient")
                .MakeGenericMethod(type)
                .Invoke(InnerConnection, null);
        }

        partial void DoConnect()
        {
            if (BaseObject is TfsConnection tfs) tfs.EnsureAuthenticated();
        }
    }
}