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

        // TODO: internal VssConnection ConfigurationServer => IsHosted ? InnerConnection : InnerConnection.ParentConnection;
        internal VssConnection ConfigurationServer => InnerConnection;

        internal Identity AuthorizedIdentity => InnerConnection.AuthorizedIdentity;

        internal Guid ServerId => InnerConnection.ServerId;

        internal virtual T GetService<T>() where T : IVssClientService => InnerConnection.GetService<T>();

        internal virtual string DisplayName
        {
            get
            {
                if(InnerConnection == null) return null;

                if(InnerConnection.Uri.Segments.Length > 1 && 
                    !InnerConnection.Uri.Segments[InnerConnection.Uri.Segments.Length-1].Equals("tfs", StringComparison.OrdinalIgnoreCase))
                {
                    return InnerConnection.Uri.Segments[InnerConnection.Uri.Segments.Length-1];
                }
                else if(InnerConnection.Uri.Host.EndsWith(".visualstudio.com"))
                {
                    return InnerConnection.Uri.Host.Substring(InnerConnection.Uri.Host.IndexOf('.'));
                }

                return InnerConnection.Uri.AbsoluteUri;
            }
        }

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