using System;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Services
{
    /// <summary>
    /// Encapsulates the platform-specific connection object
    /// </summary>
    public partial class Connection: PSObject
    {
        internal Connection(object obj) : base(obj) { }

        internal virtual Uri Uri => InnerConnection.Uri;

        internal virtual T GetClient<T>() where T : VssHttpClientBase => InnerConnection.GetClient<T>();

        internal virtual void Connect() => DoConnect();

        partial void DoConnect();

    }
}
