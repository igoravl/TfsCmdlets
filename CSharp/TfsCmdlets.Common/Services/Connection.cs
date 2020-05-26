using System;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.Identity;
using Microsoft.VisualStudio.Services.WebApi;

namespace TfsCmdlets.Services
{
    public partial class Connection: PSObject
    {
        public Connection(object obj) : base(obj) { }

        internal Uri Uri => InnerConnection.Uri;

        internal T GetClient<T>() where T : VssHttpClientBase => InnerConnection.GetClient<T>();

        internal void Connect() => DoConnect();

        partial void DoConnect();

    }
}
