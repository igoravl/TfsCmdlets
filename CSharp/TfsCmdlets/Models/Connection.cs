using System;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Microsoft.VisualStudio.Services.Location;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Location;
using TfsCmdlets.Services;

#if NETCOREAPP3_1_OR_GREATER
using AdoConnection = Microsoft.VisualStudio.Services.WebApi.VssConnection;
#else
using Microsoft.TeamFoundation.Client;
using AdoConnection = Microsoft.TeamFoundation.Client.TfsConnection;
#endif

namespace TfsCmdlets.Models
{
    public sealed class Connection : PSObject, ITfsServiceProvider
    {
        /// <summary>Converts Connection to AdoConnection</summary>
        public static implicit operator AdoConnection(Connection c) => c.InnerConnection;

        /// <summary>Converts AdoConnection to Connection</summary>
        public static implicit operator Connection(AdoConnection c) => new Connection(c);

        internal Connection(object obj) : base(obj) { }

        internal AdoConnection InnerConnection => this.BaseObject as AdoConnection;

        private AdoConnection _parentConnection;

        internal AdoConnection ConfigurationServer => _parentConnection ??= GetParentConnection();

        internal Models.Identity AuthorizedIdentity => new Models.Identity(InnerConnection.AuthorizedIdentity);

        internal Uri Uri => InnerConnection.Uri;

        object ITfsServiceProvider.GetService(Type serviceType)
            => CallGenericMethod(serviceType, "GetService");

        object ITfsServiceProvider.GetClient(Type clientType)
            => CallGenericMethod(clientType, "GetClient");

#if NETCOREAPP3_1_OR_GREATER
        internal Guid ServerId => InnerConnection.ServerId;
#else
        internal Guid ServerId => InnerConnection.InstanceId;
#endif


        internal string DisplayName
        {
            get
            {
                if (InnerConnection == null) return null;

                var segmentCount = InnerConnection.Uri.Segments.Length;

                if (segmentCount > 1 &&
                    !InnerConnection.Uri.Segments[segmentCount - 1].Equals("tfs", StringComparison.OrdinalIgnoreCase))
                {
                    return InnerConnection.Uri.Segments[segmentCount - 1];
                }
                else if (InnerConnection.Uri.Host.EndsWith(".visualstudio.com"))
                {
                    return InnerConnection.Uri.Host.Substring(InnerConnection.Uri.Host.IndexOf('.'));
                }

                return InnerConnection.Uri.AbsoluteUri;
            }
        }

        internal void Connect()
        {
#if NETCOREAPP3_1_OR_GREATER
            if (BaseObject is AdoConnection vss)
            {
                vss.ConnectAsync().SyncResult();
            }
#else
            if (BaseObject is TfsTeamProjectCollection tpc)
            {
                tpc.EnsureAuthenticated();
            }
            else if (BaseObject is TfsConfigurationServer srv)
            {
                srv.EnsureAuthenticated();
            }
#endif
        }

        internal bool IsHosted => (
            InnerConnection.Uri.Host.Equals("dev.azure.com", StringComparison.OrdinalIgnoreCase) ||
            InnerConnection.Uri.Host.EndsWith(".visualstudio.com", StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// Returns a client object given its type.
        /// </summary>
        public object GetClientFromType(Type type)
        {
#if NETCOREAPP3_1_OR_GREATER
            return InnerConnection.GetClient(type);
#else
            return CallGenericMethod(type, "GetClient");
#endif
        }

        private AdoConnection GetParentConnection()
        {
#if NETCOREAPP3_1_OR_GREATER
            var uri = new Uri(InnerConnection.GetService<ILocationService>()
                .GetLocationData(Guid.Empty)
                .LocationForCurrentConnection("LocationService2", LocationServiceConstants.ApplicationIdentifier));
            var srv = new AdoConnection(uri, InnerConnection.Credentials, InnerConnection.Settings);
#else
            if (!(InnerConnection is TfsTeamProjectCollection tpc)) return InnerConnection;
            var srv = tpc.ConfigurationServer ?? new TfsConfigurationServer(tpc.Uri, tpc.ClientCredentials);
#endif
            return srv;
        }

        internal object CallGenericMethod(Type T, string methodName, object[] args = null)
        {
            var m = InnerConnection.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.Name.Equals(methodName));

            object result;

            try
            {
                result = m?.MakeGenericMethod(T).Invoke(InnerConnection, null);
            }
            catch (Exception ex)
            {
                if(ex.InnerException != null) throw ex.InnerException;

                throw;
            }

            return result;
        }
    }
}

