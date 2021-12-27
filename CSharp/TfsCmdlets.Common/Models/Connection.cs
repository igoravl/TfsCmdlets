using System.Management.Automation;
using System.Reflection;
using Microsoft.VisualStudio.Services.Location;
using Microsoft.VisualStudio.Services.WebApi;

#if NETCOREAPP3_1_OR_GREATER
using AdoConnection = Microsoft.VisualStudio.Services.WebApi.VssConnection;
using Microsoft.VisualStudio.Services.WebApi.Location;
#else
using Microsoft.TeamFoundation.Client;
using AdoConnection = Microsoft.TeamFoundation.Client.TfsConnection;
#endif

namespace TfsCmdlets.Models
{
    public sealed class Connection : ModelBase<AdoConnection>, ITfsServiceProvider
    {
        /// <summary>Converts Connection to AdoConnection</summary>
        public static implicit operator AdoConnection(Connection c) => c.InnerObject;

        /// <summary>Converts AdoConnection to Connection</summary>
        public static implicit operator Connection(AdoConnection c) => new Connection(c);

        public Connection(AdoConnection obj) : base(obj) { }

        private AdoConnection _parentConnection;

        public AdoConnection ConfigurationServer => _parentConnection ??= GetParentConnection();

        public Uri Uri => InnerObject.Uri;

        object ITfsServiceProvider.GetService(Type serviceType)
            => CallGenericMethod(serviceType, "GetService");

        object ITfsServiceProvider.GetClient(Type clientType)
            => CallGenericMethod(clientType, "GetClient");

#if NETCOREAPP3_1_OR_GREATER
        public Guid ServerId => InnerObject.ServerId;
        public string CurrentUserUniqueName => (string)InnerObject.AuthorizedIdentity.Properties["Account"];
#else
        public Guid ServerId => InnerObject.InstanceId;
        public string CurrentUserUniqueName => InnerObject.AuthorizedIdentity.UniqueName;
#endif

        public string CurrentUserDisplayName => InnerObject.AuthorizedIdentity.DisplayName;


        public string DisplayName
        {
            get
            {
                if (InnerObject == null) return null;

                var segmentCount = InnerObject.Uri.Segments.Length;

                if (segmentCount > 1 &&
                    !InnerObject.Uri.Segments[segmentCount - 1].Equals("tfs", StringComparison.OrdinalIgnoreCase))
                {
                    return InnerObject.Uri.Segments[segmentCount - 1];
                }
                else if (InnerObject.Uri.Host.EndsWith(".visualstudio.com"))
                {
                    return InnerObject.Uri.Host.Substring(InnerObject.Uri.Host.IndexOf('.'));
                }

                return InnerObject.Uri.AbsoluteUri;
            }
        }

        public void Connect()
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

        public bool IsHosted => (
            InnerObject.Uri.Host.Equals("dev.azure.com", StringComparison.OrdinalIgnoreCase) ||
            InnerObject.Uri.Host.EndsWith(".visualstudio.com", StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// Returns a client object given its type.
        /// </summary>
        public object GetClientFromType(Type type)
        {
#if NETCOREAPP3_1_OR_GREATER
            return InnerObject.GetClient(type);
#else
            return CallGenericMethod(type, "GetClient");
#endif
        }

        private AdoConnection GetParentConnection()
        {
#if NETCOREAPP3_1_OR_GREATER
            var uri = new Uri(InnerObject.GetService<ILocationService>()
                .GetLocationData(Guid.Empty)
                .LocationForCurrentConnection("LocationService2", LocationServiceConstants.ApplicationIdentifier));
            var srv = new AdoConnection(uri, InnerObject.Credentials, InnerObject.Settings);
#else
            if (!(InnerObject is TfsTeamProjectCollection tpc)) return InnerObject;
            var srv = tpc.ConfigurationServer ?? new TfsConfigurationServer(tpc.Uri, tpc.ClientCredentials);
#endif
            return srv;
        }

        public object CallGenericMethod(Type T, string methodName, object[] args = null)
        {
            var m = InnerObject.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(m => m.Name.Equals(methodName));

            object result;

            try
            {
                result = m?.MakeGenericMethod(T).Invoke(InnerObject, null);
            }
            catch (Exception ex)
            {
                if(ex.InnerException != null) throw ex.InnerException;

                throw;
            }

            return result;
        }

#if NET471_OR_GREATER
        private Microsoft.VisualStudio.Services.Common.SubjectDescriptor GetSubjectDescriptor(Microsoft.TeamFoundation.Framework.Client.TeamFoundationIdentity identity, AdoConnection connection)
        {
            var client = connection.GetClient<Microsoft.VisualStudio.Services.Identity.Client.IdentityHttpClient>();
            var apiIdentity = client.ReadIdentityAsync(identity.TeamFoundationId, Microsoft.VisualStudio.Services.Identity.QueryMembership.None).SyncResult();

            return apiIdentity.SubjectDescriptor;
        }
#endif        
    }
}

