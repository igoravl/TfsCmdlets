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

    public sealed class ConfigurationServer : Connection
    {
#if NETCOREAPP3_1_OR_GREATER
        public static implicit operator ConfigurationServer(VssConnection c) => new ConfigurationServer(c);
#else
        public static implicit operator ConfigurationServer(Microsoft.TeamFoundation.Client.TfsConnection c) => new ConfigurationServer(c);
#endif

        public ConfigurationServer(object obj) : base(obj)
        {
        }
    }

    public sealed class TeamProjectCollection : Connection
    {
#if NETCOREAPP3_1_OR_GREATER
        public static implicit operator TeamProjectCollection(VssConnection c) => new TeamProjectCollection(c);
#else
        public static implicit operator TeamProjectCollection(Microsoft.TeamFoundation.Client.TfsConnection c) => new TeamProjectCollection(c);
#endif
        public TeamProjectCollection(object obj) : base(obj)
        {
        }
    }
}
