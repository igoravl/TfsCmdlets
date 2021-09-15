using System;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Models
{
    /// <summary>
    /// Encapsulates the platform-specific connection object
    /// </summary>
    public abstract partial class Connection : PSObject
    {
        internal Connection(object obj) : base(obj) { }

        internal virtual Uri Uri => InnerConnection.Uri;

        internal virtual T GetClient<T>() where T : VssHttpClientBase => InnerConnection.GetClient<T>();

        internal virtual void Connect() => DoConnect();

        partial void DoConnect();
    }

    public sealed class ServerConnection : Connection
    {
#if NETCOREAPP3_1_OR_GREATER
        public static implicit operator ServerConnection(VssConnection c) => new ServerConnection(c);
#else
        public static implicit operator ServerConnection(Microsoft.TeamFoundation.Client.TfsConnection c) => new ServerConnection(c);
#endif

        public ServerConnection(object obj) : base(obj)
        {
        }
    }

    public sealed class TpcConnection : Connection
    {
#if NETCOREAPP3_1_OR_GREATER
        public static implicit operator TpcConnection(VssConnection c) => new TpcConnection(c);
#else
        public static implicit operator TpcConnection(Microsoft.TeamFoundation.Client.TfsConnection c) => new TpcConnection(c);
#endif
        public TpcConnection(object obj) : base(obj)
        {
        }
    }
}
