#if NETCOREAPP3_1_OR_GREATER
using System;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.Identity;
using Microsoft.VisualStudio.Services.Location;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Location;

namespace TfsCmdlets.Models
{
    public partial class Connection
    {
        /// <summary>Converts Connection to VssConnection</summary>
        public static implicit operator VssConnection(Connection c) => c.InnerConnection;

        /// <summary>Converts VssConnection to Connection</summary>
        // public static implicit operator Connection(VssConnection c) => new Connection(c);

        internal VssConnection InnerConnection => this.BaseObject as VssConnection;
        private VssConnection _ParentConnection;

        // TODO: internal VssConnection ConfigurationServer => IsHosted ? InnerConnection : InnerConnection.ParentConnection;
        internal VssConnection ConfigurationServer
        {
            get
            {
                try
                {
                    return _ParentConnection??= GetParentConnection();
                }
                catch
                {
                    return InnerConnection;
                }
            }
        }

        internal Models.Identity AuthorizedIdentity => new Models.Identity(InnerConnection.AuthorizedIdentity);

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

        /// <summary>
        /// Returns a client object given its type.
        /// </summary>
        public object GetClientFromType(Type type) => InnerConnection.GetClient(type);

        private VssConnection GetParentConnection()
        {
            var uri = new Uri(InnerConnection.GetService<ILocationService>()
                .GetLocationData(Guid.Empty)
                .LocationForCurrentConnection("LocationService2", LocationServiceConstants.ApplicationIdentifier));

            return new VssConnection(uri, InnerConnection.Credentials, InnerConnection.Settings);
        }
    }
}
#endif