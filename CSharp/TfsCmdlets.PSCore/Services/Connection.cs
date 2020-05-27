using System;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.Identity;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Services
{
    public partial class Connection
    {
        public static implicit operator VssConnection(Connection c) => c.InnerConnection;
        public static implicit operator Connection(VssConnection c) => new Connection(c);

        internal VssConnection InnerConnection => this.BaseObject as VssConnection;

        internal VssConnection ConfigurationServer => IsHosted ? InnerConnection : InnerConnection.ParentConnection;

        internal Identity AuthorizedIdentity => InnerConnection.AuthorizedIdentity;

        internal Guid ServerId => InnerConnection.ServerId;

        partial void DoConnect()
        {
            if (BaseObject is VssConnection vss)
            {
                vss.ConnectAsync().SyncResult();
            }
        }

        internal bool IsHosted => (
            InnerConnection.Uri.Host.Equals("dev.azure.com", StringComparison.OrdinalIgnoreCase) ||
            InnerConnection.Uri.Host.EndsWith(".visualstudio.com", StringComparison.OrdinalIgnoreCase));

        public object GetClientFromType(Type type) => InnerConnection.GetClient(type);
    }
}