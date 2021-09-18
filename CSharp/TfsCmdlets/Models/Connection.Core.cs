//using System;
//using System.Management.Automation;
//using Microsoft.VisualStudio.Services.Location;
//using Microsoft.VisualStudio.Services.WebApi;
//using Microsoft.VisualStudio.Services.WebApi.Location;

//#if NETCOREAPP3_1_OR_GREATER
//using AdoConnection = Microsoft.VisualStudio.Services.WebApi.VssConnection;
//#else
//using AdoConnection = Microsoft.TeamFoundation.Client.TfsConnection;
//#endif

//namespace TfsCmdlets.Models
//{
//    public class Connection: PSObject
//    {
//        /// <summary>Converts Connection to AdoConnection</summary>
//        public static implicit operator AdoConnection(Connection c) => c.InnerConnection;

//        /// <summary>Converts AdoConnection to Connection</summary>
//        public static implicit operator Connection(AdoConnection c) => new Connection(c);

//        private Connection(object obj) : base(obj) { }

//        internal AdoConnection InnerConnection => this.BaseObject as AdoConnection;

//        private AdoConnection _parentConnection;

//        internal AdoConnection ConfigurationServer => _parentConnection ??= GetParentConnection();

//        internal Models.Identity AuthorizedIdentity => new Models.Identity(InnerConnection.AuthorizedIdentity);

//        internal virtual Uri Uri => InnerConnection.Uri;

//        internal virtual T GetClient<T>() where T : VssHttpClientBase => InnerConnection.GetClient<T>();

//        internal virtual void Connect() => DoConnect();



//#if NETCOREAPP3_1_OR_GREATER
//        internal Guid ServerId => InnerConnection.ServerId;

//        internal T GetService<T>() where T : IVssClientService => InnerConnection.GetService<T>();
//#else
//        internal Guid ServerId => InnerConnection.InstanceId;

//        internal virtual T GetService<T>() => InnerConnection.GetService<T>();
//#endif

//        internal object GetService(Type serviceType) => InnerConnection.GetService(serviceType);

//        internal virtual string DisplayName
//        {
//            get
//            {
//                if(InnerConnection == null) return null;

//                if(InnerConnection.Uri.Segments.Length > 1 && 
//                    !InnerConnection.Uri.Segments[^1].Equals("tfs", StringComparison.OrdinalIgnoreCase))
//                {
//                    return InnerConnection.Uri.Segments[^1];
//                }
//                else if(InnerConnection.Uri.Host.EndsWith(".visualstudio.com"))
//                {
//                    return InnerConnection.Uri.Host.Substring(InnerConnection.Uri.Host.IndexOf('.'));
//                }

//                return InnerConnection.Uri.AbsoluteUri;
//            }
//        }

//        partial void DoConnect()
//        {
//#if NETCOREAPP3_1_OR_GREATER
//            if (BaseObject is AdoConnection vss)
//            {
//                vss.ConnectAsync().SyncResult();
//            }
//#else
//            if (BaseObject is TfsTeamProjectCollection tpc)
//            {
//                tpc.EnsureAuthenticated();
//            }
//            else if (BaseObject is TfsConfigurationServer srv)
//            {
//                srv.EnsureAuthenticated();
//            }
//#endif
//        }

//        internal bool IsHosted => (
//            InnerConnection.Uri.Host.Equals("dev.azure.com", StringComparison.OrdinalIgnoreCase) ||
//            InnerConnection.Uri.Host.EndsWith(".visualstudio.com", StringComparison.OrdinalIgnoreCase));

//        /// <summary>
//        /// Returns a client object given its type.
//        /// </summary>
//        public object GetClientFromType(Type type)
//        {
//#if NETCOREAPP3_1_OR_GREATER
//            return InnerConnection.GetClient(type);
//#else
//            var m = InnerConnection.GetType()
//                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
//                .Where(m => m.Name.Equals("GetClient")).FirstOrDefault();

//            return m?
//                .MakeGenericMethod(type)
//                .Invoke(InnerConnection, null);
//#endif
//        }

//        private AdoConnection GetParentConnection()
//        {
//#if NETCOREAPP3_1_OR_GREATER
//            var uri = new Uri(InnerConnection.GetService<ILocationService>()
//                .GetLocationData(Guid.Empty)
//                .LocationForCurrentConnection("LocationService2", LocationServiceConstants.ApplicationIdentifier));
//            var srv = new AdoConnection(uri, InnerConnection.Credentials, InnerConnection.Settings);
//#else
//            if (!(InnerConnection is TfsTeamProjectCollection tpc)) return InnerConnection;
//            var srv = tpc.ConfigurationServer ?? new TfsConfigurationServer(tpc.Uri, tpc.ClientCredentials);
//#endif
//            return srv;
//        }
//    }
//}
