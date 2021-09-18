//#if NET471_OR_GREATER
//using System;
//using System.Linq;
//using System.Reflection;
//using Microsoft.TeamFoundation.Client;

//namespace TfsCmdlets.Models
//{
//    partial class Connection
//    {
//        internal Microsoft.TeamFoundation.Client.TfsConnection InnerConnection => BaseObject as Microsoft.TeamFoundation.Client.TfsConnection;

//        internal object GetService(Type serviceType) => InnerConnection.GetService(serviceType);

//        internal Microsoft.TeamFoundation.Client.TfsConnection ConfigurationServer
//        {
//            get
//            {

//            }
//        }

//        internal Models.Identity AuthorizedIdentity => new Models.Identity(InnerConnection.AuthorizedIdentity);



//        internal virtual string DisplayName
//        {
//            get
//            {
//                if (InnerConnection == null) return null;

//                if (InnerConnection.Uri.Segments.Length > 1 &&
//                    !InnerConnection.Uri.Segments[InnerConnection.Uri.Segments.Length - 1].Equals("tfs", StringComparison.OrdinalIgnoreCase))
//                {
//                    return InnerConnection.Uri.Segments[InnerConnection.Uri.Segments.Length - 1];
//                }
//                else if (InnerConnection.Uri.Host.EndsWith(".visualstudio.com"))
//                {
//                    return InnerConnection.Uri.Host.Substring(InnerConnection.Uri.Host.IndexOf('.'));
//                }

//                return InnerConnection.Uri.AbsoluteUri;
//            }
//        }

//        /// <summary>
//        /// Gets a client of the given type
//        /// </summary>
//        public object GetClientFromType(Type type)
//        {
//            var m = InnerConnection.GetType()
//                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
//                .Where(m => m.Name.Equals("GetClient")).FirstOrDefault();

//            return m?
//                .MakeGenericMethod(type)
//                .Invoke(InnerConnection, null);
//        }

//        internal bool IsHosted => InnerConnection.IsHostedServer;

//        partial void DoConnect()
//        {
//            if (BaseObject is TfsTeamProjectCollection tpc)
//            {
//                tpc.EnsureAuthenticated();
//            }
//            else if (BaseObject is TfsConfigurationServer srv)
//            {
//                srv.EnsureAuthenticated();
//            }
//        }
//    }
//}
//#endif